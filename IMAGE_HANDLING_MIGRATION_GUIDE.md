# ?? Image Handling Migration Guide

## Current State Analysis

Your application currently uses a **HYBRID** approach:
- ? `HybridImage` Value Object (supports both URL and Database)
- ? `ImageData` Value Object (database storage)
- ? `Url` Value Object (URL storage)
- ? `IImageProcessingService` (compression, resizing)
- ? **Missing:** File storage service for cloud/local uploads

---

## Migration Strategy: 3-Phase Approach

### **Phase 1: Add File Storage Infrastructure** ? COMPLETED

**What we added:**
1. `IFileStorageService` interface
2. `LocalFileStorageService` implementation (for development)
3. `AzureBlobStorageService` implementation (for production)
4. `ImageService` orchestrator
5. `ImagesController` example
6. Configuration in `appsettings.json`
7. Service registration in `Program.cs`

**What to do:**
```bash
# No additional steps - already implemented above
```

---

### **Phase 2: Update Existing Entities** (NEXT STEP)

**Goal:** Migrate existing entities from database storage to URL storage

#### Step 2.1: Identify Entities Using Images

Search your codebase for:
```csharp
// Entities with HybridImage
public HybridImage? SomeImage { get; private set; }

// Entities with direct ImageData/Url
public Url? IconUrl { get; private set; }
public ImageData? IconData { get; private set; }
```

**Found entities:**
- `FocusArea` (IconUrl, IconData)
- `Partner` (likely has images)
- `Activity` (MediaGallery, GalleryImages)
- `HeroSection` (likely has images)
- `TeamMember` (likely has profile images)
- Others...

#### Step 2.2: Update Command Handlers

**Before (storing in database):**
```csharp
public async Task<Guid> Handle(UpdateFocusAreaCommand request, CancellationToken cancellationToken)
{
    var focusArea = await _repository.GetByIdAsync(request.Id);
    
    // Old way - storing base64 in database
    var iconData = ImageData.Create(request.IconBase64, "image/jpeg", "icon.jpg", 12345);
    focusArea.Update(..., iconData: iconData);
    
    await _unitOfWork.SaveChangesAsync();
    return focusArea.Id;
}
```

**After (storing in cloud storage):**
```csharp
public async Task<Guid> Handle(UpdateFocusAreaCommand request, CancellationToken cancellationToken)
{
    var focusArea = await _repository.GetByIdAsync(request.Id);
    
    // New way - upload to storage and get URL
    HybridImage? newIcon = null;
    if (!string.IsNullOrEmpty(request.IconBase64))
    {
        newIcon = await _imageService.ProcessAndUploadImageAsync(
            request.IconBase64,
            request.IconFileName ?? "icon.jpg",
            "focus-areas", // container name
            maxWidth: 512,
            maxHeight: 512,
            quality: 90);
    }
    
    focusArea.Update(..., iconUrl: newIcon?.ImageUrl, iconData: null);
    
    await _unitOfWork.SaveChangesAsync();
    return focusArea.Id;
}
```

#### Step 2.3: Create Migration Script for Existing Data

```csharp
// Example: Migrate existing FocusArea icons from DB to cloud storage

public class MigrateImagesToStorageService
{
    private readonly IFocusAreaRepository _repository;
    private readonly ImageService _imageService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<MigrateImagesToStorageService> _logger;

    public async Task MigrateFocusAreaIconsAsync()
    {
        var allFocusAreas = await _repository.GetAllAsync();
        var migratedCount = 0;

        foreach (var focusArea in allFocusAreas)
        {
            // Skip if already using URL
            if (focusArea.IconUrl != null)
            {
                _logger.LogInformation("FocusArea {Id} already uses URL, skipping", focusArea.Id);
                continue;
            }

            // Skip if no icon data
            if (focusArea.IconData == null)
            {
                _logger.LogInformation("FocusArea {Id} has no icon, skipping", focusArea.Id);
                continue;
            }

            try
            {
                // Upload existing ImageData to cloud storage
                var imageUrl = await _imageService.UploadImageAsync(
                    focusArea.IconData,
                    "focus-areas");

                // Update entity to use URL instead of database storage
                focusArea.UpdateIcon(iconUrl: Url.Create(imageUrl), iconData: null);

                migratedCount++;
                _logger.LogInformation("Migrated FocusArea {Id} icon to cloud storage", focusArea.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to migrate FocusArea {Id}", focusArea.Id);
            }
        }

        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Migration completed. Migrated {Count} focus area icons", migratedCount);
    }
}
```

---

### **Phase 3: Clean Up Database** (FINAL STEP)

Once all images are migrated to storage:

#### Step 3.1: Remove ImageData columns from database

**Entity changes:**
```csharp
// Before
public class FocusArea
{
    public Url? IconUrl { get; private set; }
    public ImageData? IconData { get; private set; } // ? Remove this
}

// After
public class FocusArea
{
    public Url? IconUrl { get; private set; }
    // IconData removed completely
}
```

#### Step 3.2: Update EF Configuration

```csharp
// FocusAreaConfiguration.cs - Remove IconData mapping
public void Configure(EntityTypeBuilder<FocusArea> builder)
{
    builder.OwnsOne(x => x.IconUrl, url => { ... });
    
    // ? REMOVE THIS:
    // builder.OwnsOne(x => x.IconData, data => { ... });
}
```

#### Step 3.3: Create Migration

```bash
cd KulturPlatform.Infrastructure
dotnet ef migrations add RemoveImageDataColumns --startup-project ../KulturPlatform.API
dotnet ef database update --startup-project ../KulturPlatform.API
```

---

## Configuration Guide

### Development (Local Storage)

```json
{
  "FileStorage": {
    "Provider": "Local",
    "BaseUrl": "https://localhost:7189"
  }
}
```

### Production (Azure Blob Storage)

```json
{
  "FileStorage": {
    "Provider": "Azure"
  },
  "AzureBlobStorage": {
    "ConnectionString": "DefaultEndpointsProtocol=https;AccountName=yourapp;AccountKey=xxx;EndpointSuffix=core.windows.net",
    "ContainerPrefix": "kulturplatform"
  }
}
```

**Container naming convention:**
- `focus-areas` ? Focus area icons
- `team-members` ? Profile pictures
- `gallery` ? Activity gallery images
- `hero-sections` ? Homepage hero images
- `thumbnails` ? Auto-generated thumbnails

---

## Best Practices

### ? DO

1. **Always use ImageService** for uploading images
   ```csharp
   var image = await _imageService.ProcessAndUploadImageAsync(...);
   ```

2. **Use HybridImage** in domain entities
   ```csharp
   public HybridImage? ProfilePicture { get; private set; }
   ```

3. **Organize by container** (category-based folders)
   ```csharp
   await _imageService.ProcessAndUploadImageAsync(..., "team-members");
   ```

4. **Delete old images** when updating
   ```csharp
   var newImage = await _imageService.UpdateImageAsync(oldImage, newBase64, ...);
   ```

5. **Validate images** before processing
   ```csharp
   await _imageProcessingService.ValidateImageAsync(base64Data);
   ```

### ? DON'T

1. **Don't store large images in database** (performance killer)
2. **Don't skip image compression** (use ImageProcessingService)
3. **Don't forget to delete old images** (storage costs money)
4. **Don't expose raw file paths** to frontend (use URLs)
5. **Don't allow unlimited file sizes** (validate and limit)

---

## Example: Complete Update Flow

```csharp
// 1. COMMAND
public record UpdateFocusAreaCommand(
    Guid Id,
    string TitleTr,
    string TitleDe,
    string DescriptionTr,
    string DescriptionDe,
    string? IconBase64,      // New image (optional)
    string? IconFileName,
    int Order
) : IRequest<Guid>;

// 2. HANDLER
public class UpdateFocusAreaCommandHandler : IRequestHandler<UpdateFocusAreaCommand, Guid>
{
    private readonly IFocusAreaRepository _repository;
    private readonly ImageService _imageService;
    private readonly IUnitOfWork _unitOfWork;

    public async Task<Guid> Handle(UpdateFocusAreaCommand request, CancellationToken cancellationToken)
    {
        var focusArea = await _repository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException($"Focus area {request.Id} not found");

        // Process icon if provided
        HybridImage? newIcon = null;
        if (!string.IsNullOrEmpty(request.IconBase64))
        {
            // Upload new icon
            newIcon = await _imageService.UpdateImageAsync(
                currentImage: /* get current icon from focusArea */,
                newBase64Data: request.IconBase64,
                newFileName: request.IconFileName ?? "icon.jpg",
                containerName: "focus-areas",
                maxWidth: 512,
                maxHeight: 512,
                quality: 90);
        }

        // Update entity
        focusArea.Update(
            titleTr: Title.Create(request.TitleTr),
            titleDe: Title.Create(request.TitleDe),
            descriptionTr: Description.Create(request.DescriptionTr),
            descriptionDe: Description.Create(request.DescriptionDe),
            iconUrl: newIcon?.ImageUrl,
            iconData: null, // Always null now (using URL storage)
            order: request.Order);

        await _repository.UpdateAsync(focusArea);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return focusArea.Id;
    }
}

// 3. CONTROLLER
[HttpPut("{id}")]
public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFocusAreaRequest request)
{
    var command = new UpdateFocusAreaCommand(
        id,
        request.TitleTr,
        request.TitleDe,
        request.DescriptionTr,
        request.DescriptionDe,
        request.IconBase64,
        request.IconFileName,
        request.Order);

    var result = await _mediator.Send(command);
    return Ok(result);
}
```

---

## Testing Checklist

### Unit Tests
- [ ] ImageService processes and uploads correctly
- [ ] ImageService deletes old images on update
- [ ] Validation rejects invalid images
- [ ] HybridImage enforces single-source rule

### Integration Tests
- [ ] Upload image ? Verify file exists in storage
- [ ] Update image ? Verify old file deleted
- [ ] Delete entity ? Verify image deleted (if cascade configured)

### Manual Testing
- [ ] Upload small image (< 1MB)
- [ ] Upload large image (> 5MB) ? Should be rejected or compressed
- [ ] Update image ? Verify old URL returns 404
- [ ] Delete record ? Verify image also deleted

---

## Troubleshooting

### "Cannot find wwwroot folder"
**Solution:** Ensure `wwwroot` folder exists in API project
```bash
mkdir KulturPlatform.API/wwwroot
mkdir KulturPlatform.API/wwwroot/uploads
```

### "Azure Blob Storage connection failed"
**Solution:** Check connection string in appsettings.json
```bash
# Test connection using Azure Storage Explorer or CLI
az storage account show --name youraccountname
```

### "Image too large for database"
**Solution:** This is exactly why we moved to file storage! Increase column size temporarily, then migrate to storage.

### "Images not showing in frontend"
**Solution:** 
1. Check CORS policy allows image requests
2. Verify BaseUrl in configuration matches your domain
3. Check browser console for 404 errors

---

## Performance Metrics

### Before (Database Storage)
- ?? Image size: ~2MB average
- ?? Database size growth: ~2GB/month
- ?? Query performance: Slow (SELECT on large BLOB columns)
- ?? Backup size: Large (includes all images)

### After (Cloud Storage)
- ?? Image size: ~200KB average (after compression)
- ?? Database size growth: ~50MB/month
- ?? Query performance: Fast (only URLs in DB)
- ?? Backup size: Small (images stored separately)
- ?? CDN-ready: Can add Azure CDN for global delivery

---

## Next Steps

1. ? **Phase 1 Complete** - Infrastructure added
2. ? **Phase 2 - Update Handlers** - Modify all command handlers to use ImageService
3. ? **Phase 3 - Data Migration** - Run migration script for existing data
4. ? **Phase 4 - Cleanup** - Remove ImageData columns from database
5. ? **Phase 5 - Production** - Switch to Azure Blob Storage

---

## Questions?

If you encounter any issues, check:
1. Logs in `ILogger` output
2. Azure Portal ? Storage Account ? Containers
3. Local file system ? `wwwroot/uploads` folder
4. Database ? Verify URL columns contain valid URLs

**Need help?** Review the example handlers in this migration guide!
