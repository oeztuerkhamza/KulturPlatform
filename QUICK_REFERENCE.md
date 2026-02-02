# ?? IMAGE HANDLING - QUICK REFERENCE

## ? Quick Start

### 1. Upload Image from Frontend

**Request:**
```http
POST /api/images/upload
Authorization: Bearer {token}
Content-Type: application/json

{
  "base64Data": "data:image/jpeg;base64,/9j/4AAQSkZJRg...",
  "fileName": "profile.jpg",
  "containerName": "team-members",
  "maxWidth": 512,
  "maxHeight": 512,
  "quality": 90
}
```

**Response:**
```json
{
  "success": true,
  "imageUrl": "https://localhost:7189/uploads/team-members/xxx-profile.jpg",
  "message": "Image uploaded successfully"
}
```

---

## ?? Command Handler Pattern

### Update Entity with Image

```csharp
public class UpdateEntityCommandHandler
{
    private readonly IRepository _repository;
    private readonly ImageService _imageService;  // ? Inject this
    private readonly IUnitOfWork _unitOfWork;

    public async Task<Guid> Handle(UpdateEntityCommand request)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        
        // Handle image upload
        HybridImage? newImage = null;
        if (!string.IsNullOrEmpty(request.ImageBase64))
        {
            newImage = await _imageService.ProcessAndUploadImageAsync(
                request.ImageBase64,
                request.ImageFileName ?? "default.jpg",
                "container-name",  // ? Category folder
                maxWidth: 1920,
                maxHeight: 1080,
                quality: 85);
        }
        
        // Update entity
        entity.Update(..., image: newImage);
        
        await _unitOfWork.SaveChangesAsync();
        return entity.Id;
    }
}
```

---

## ?? Container Naming Convention

| Entity | Container Name | Image Size | Quality |
|--------|---------------|------------|---------|
| **FocusArea** | `focus-areas` | 512x512 | 90 |
| **TeamMember** | `team-members` | 800x800 | 85 |
| **Activity** | `activities` | 1920x1080 | 85 |
| **Hero Section** | `hero-sections` | 1920x1080 | 90 |
| **Gallery** | `gallery` | 1920x1080 | 80 |
| **Thumbnails** | `thumbnails` | 300x200 | 75 |

---

## ?? Image Service Methods

### 1. Process & Upload
```csharp
var image = await _imageService.ProcessAndUploadImageAsync(
    base64Data,
    fileName,
    containerName,
    maxWidth: 1920,
    maxHeight: 1080,
    quality: 85);
// Returns: HybridImage with URL
```

### 2. Update Image (with old cleanup)
```csharp
var newImage = await _imageService.UpdateImageAsync(
    currentImage: entity.CurrentImage,
    newBase64Data: request.NewImageBase64,
    newFileName: request.NewFileName,
    containerName: "container-name",
    maxWidth: 1920,
    maxHeight: 1080,
    quality: 85);
// Old image automatically deleted!
```

### 3. Delete Image
```csharp
await _imageService.DeleteImageAsync(entity.Image);
```

### 4. Create Thumbnail
```csharp
var thumbnail = await _imageService.CreateAndUploadThumbnailAsync(
    sourceImage: entity.Image,
    containerName: "thumbnails",
    width: 300,
    height: 200);
```

---

## ?? Configuration

### appsettings.json (Development)
```json
{
  "FileStorage": {
    "Provider": "Local",
    "BaseUrl": "https://localhost:7189"
  },
  "ImageProcessing": {
    "MaxFileSizeBytes": 5242880,
    "DefaultMaxWidth": 1920,
    "DefaultMaxHeight": 1080,
    "DefaultQuality": 85
  }
}
```

### appsettings.Production.json
```json
{
  "FileStorage": {
    "Provider": "Azure"
  },
  "AzureBlobStorage": {
    "ConnectionString": "DefaultEndpointsProtocol=https;AccountName=xxx;AccountKey=xxx;EndpointSuffix=core.windows.net",
    "ContainerPrefix": "kulturplatform"
  }
}
```

---

## ? Validation Rules

### Max File Size
- Default: 5MB
- Configurable in appsettings.json
- Validation happens before processing

### Allowed MIME Types
- `image/jpeg`
- `image/jpg`
- `image/png`
- `image/webp`

### Image Dimensions
- Max Width: 1920px (default)
- Max Height: 1080px (default)
- Auto-resized if larger

---

## ?? Best Practices

### ? DO

```csharp
// ? Use ImageService for all uploads
var image = await _imageService.ProcessAndUploadImageAsync(...);

// ? Delete old images when updating
await _imageService.UpdateImageAsync(oldImage, newBase64, ...);

// ? Use specific container names
await _imageService.ProcessAndUploadImageAsync(..., "team-members");

// ? Store URLs in database, not base64
entity.ImageUrl = image.ImageUrl;
entity.ImageData = null;  // Don't store in DB!

// ? Validate before processing
await _imageProcessingService.ValidateImageAsync(base64Data);
```

### ? DON'T

```csharp
// ? Don't store base64 in database
entity.ImageData = ImageData.Create(base64, ...);  // NO!

// ? Don't skip image compression
var url = await _fileStorage.UploadImageFromBase64Async(rawBase64, ...);  // Missing compression!

// ? Don't forget to delete old images
entity.Image = newImage;  // Old image still in storage!

// ? Don't allow unlimited sizes
// Always set maxWidth, maxHeight, quality

// ? Don't use generic container names
await _imageService.ProcessAndUploadImageAsync(..., "images");  // Too generic!
```

---

## ?? Troubleshooting

### Image Upload Fails

**Symptom:** 500 Internal Server Error

**Solutions:**
1. Check wwwroot/uploads folder exists
2. Verify folder permissions
3. Check logs for detailed error
4. Validate base64 format

### Image Not Showing

**Symptom:** 404 Not Found

**Solutions:**
1. Verify BaseUrl in configuration
2. Check CORS policy
3. Verify file exists: `GET {imageUrl}`
4. Check browser console for errors

### Database Bloat

**Symptom:** Database size growing rapidly

**Solutions:**
1. Check if still storing ImageData (should be null)
2. Run migration to cloud storage
3. Verify using URL storage not database

---

## ?? Performance Tips

### Optimize Images

```csharp
// Small images (icons, thumbnails)
await _imageService.ProcessAndUploadImageAsync(
    ...,
    maxWidth: 512,
    maxHeight: 512,
    quality: 90);  // Higher quality for small images

// Large images (hero, gallery)
await _imageService.ProcessAndUploadImageAsync(
    ...,
    maxWidth: 1920,
    maxHeight: 1080,
    quality: 80);  // Lower quality acceptable for large images
```

### Batch Upload

```csharp
// Upload multiple images in parallel
var uploadTasks = images.Select(img => 
    _imageService.ProcessAndUploadImageAsync(
        img.Base64Data,
        img.FileName,
        "gallery"));

var uploadedImages = await Task.WhenAll(uploadTasks);
```

---

## ?? Security Checklist

- [x] Validate file size (< 5MB)
- [x] Validate MIME types (only images)
- [x] Validate base64 format
- [x] Sanitize filenames
- [x] Use authentication on upload endpoints
- [x] Rate limit upload requests
- [ ] Scan for malware (TODO if needed)
- [ ] Use private containers for sensitive images

---

## ?? Monitoring

### Log Entries

```csharp
_logger.LogInformation("Image uploaded: {FileName}, Size: {Size}KB", fileName, sizeKb);
_logger.LogWarning("Image validation failed: {Reason}", reason);
_logger.LogError("Image upload failed: {Error}", ex.Message);
```

### Metrics to Track

- Upload success rate
- Average file size
- Processing time
- Storage usage
- CDN cache hit rate (Azure)

---

## ?? Examples

### Example 1: Update FocusArea Icon

```csharp
public class UpdateFocusAreaCommandHandler
{
    private readonly IFocusAreaRepository _repository;
    private readonly ImageService _imageService;
    private readonly IUnitOfWork _unitOfWork;

    public async Task<Guid> Handle(UpdateFocusAreaCommand request)
    {
        var focusArea = await _repository.GetByIdAsync(request.Id);
        
        HybridImage? newIcon = null;
        if (!string.IsNullOrEmpty(request.IconBase64))
        {
            newIcon = await _imageService.ProcessAndUploadImageAsync(
                request.IconBase64,
                request.IconFileName ?? "icon.jpg",
                "focus-areas",
                maxWidth: 512,
                maxHeight: 512,
                quality: 90);
        }
        
        focusArea.Update(
            ...,
            iconUrl: newIcon?.ImageUrl,
            iconData: null);
        
        await _unitOfWork.SaveChangesAsync();
        return focusArea.Id;
    }
}
```

### Example 2: Upload Team Member Photo

```csharp
public class CreateTeamMemberCommandHandler
{
    private readonly ITeamMemberRepository _repository;
    private readonly ImageService _imageService;
    private readonly IUnitOfWork _unitOfWork;

    public async Task<Guid> Handle(CreateTeamMemberCommand request)
    {
        HybridImage? photo = null;
        if (!string.IsNullOrEmpty(request.PhotoBase64))
        {
            photo = await _imageService.ProcessAndUploadImageAsync(
                request.PhotoBase64,
                request.PhotoFileName ?? "profile.jpg",
                "team-members",
                maxWidth: 800,
                maxHeight: 800,
                quality: 85);
        }
        
        var teamMember = TeamMember.Create(
            ...,
            photo);
        
        await _repository.AddAsync(teamMember);
        await _unitOfWork.SaveChangesAsync();
        
        return teamMember.Id;
    }
}
```

### Example 3: Bulk Upload Gallery Images

```csharp
public class UploadGalleryImagesCommandHandler
{
    private readonly IActivityRepository _repository;
    private readonly ImageService _imageService;
    private readonly IUnitOfWork _unitOfWork;

    public async Task Handle(UploadGalleryImagesCommand request)
    {
        var activity = await _repository.GetByIdAsync(request.ActivityId);
        
        // Upload all images in parallel
        var uploadTasks = request.Images.Select(img =>
            _imageService.ProcessAndUploadImageAsync(
                img.Base64Data,
                img.FileName,
                "gallery",
                maxWidth: 1920,
                maxHeight: 1080,
                quality: 80));
        
        var uploadedImages = await Task.WhenAll(uploadTasks);
        
        // Add to activity
        foreach (var image in uploadedImages)
        {
            activity.AddGalleryImage(image);
        }
        
        await _unitOfWork.SaveChangesAsync();
    }
}
```

---

## ?? Related Documentation

- [Migration Guide](IMAGE_HANDLING_MIGRATION_GUIDE.md)
- [Architecture Decision Record](ADR_IMAGE_HANDLING_STRATEGY.md)
- [Implementation Summary](IMPLEMENTATION_SUMMARY.md)

---

**Last Updated:** 2026-01-28  
**Version:** 1.0  
**Status:** ? Production Ready
