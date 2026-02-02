# ?? Architecture Decision Record: Image Handling Strategy

**Status:** Recommended  
**Date:** 2026-01-28  
**Decision Maker:** Architecture Team  
**Context:** Clean Architecture + DDD Application

---

## Problem Statement

How should we handle image storage in a .NET Clean Architecture application following DDD principles?

---

## Decision

**Chosen Strategy:** **Hybrid Approach transitioning to Cloud Storage (URL-based)**

### Implementation:
- **Short-term (Current):** Hybrid (support both DB and URL)
- **Medium-term (3-6 months):** Migrate all images to cloud storage
- **Long-term (Production):** URL-only with Azure Blob Storage + CDN

---

## Options Considered

### Option 1: Database-Only Storage ?

**Approach:** Store images as VARBINARY(MAX) or TEXT (base64)

**Pros:**
- ? Single source of truth
- ? ACID transactions
- ? Included in database backups
- ? No external dependencies

**Cons:**
- ? **CRITICAL:** Database bloat (1 image = 2MB avg)
- ? Performance degradation on large tables
- ? Expensive backup/restore operations
- ? Not scalable beyond 100,000 images
- ? Cannot use CDN for global delivery
- ? Memory pressure on database server

**Verdict:** ? **Not recommended for production**

---

### Option 2: File System Storage ??

**Approach:** Store images in server's local file system (wwwroot/uploads)

**Pros:**
- ? Fast local access
- ? Simple implementation
- ? No cloud dependencies
- ? Good for development/testing

**Cons:**
- ? Not cloud-native
- ? Backup complexity (separate from DB)
- ? Scaling issues (need shared storage)
- ? No built-in CDN
- ? Server disk space management

**Verdict:** ?? **Good for DEVELOPMENT, not production**

---

### Option 3: Cloud Storage (Azure Blob/AWS S3) ?

**Approach:** Store images in cloud blob storage, save URLs in database

**Pros:**
- ? **Highly scalable** (unlimited storage)
- ? **Cost-effective** ($0.02/GB/month)
- ? **CDN integration** (fast global delivery)
- ? Database stays small and fast
- ? Built-in redundancy and backup
- ? Supports pre-signed URLs for security
- ? Pay-as-you-grow model

**Cons:**
- ?? External dependency (mitigated by hybrid approach)
- ?? Transaction complexity (mitigated by retry logic)
- ?? Potential link rot (mitigated by owned storage)

**Verdict:** ? **RECOMMENDED for production**

---

### Option 4: Hybrid Approach (Current) ?

**Approach:** Support both database and URL storage during transition

**Pros:**
- ? **Flexibility** during migration
- ? **Backwards compatibility**
- ? **Risk mitigation** (fallback option)
- ? Gradual migration path

**Cons:**
- ?? Code complexity (both paths)
- ?? Migration overhead
- ?? Need clear rules for which to use

**Verdict:** ? **RECOMMENDED for transition period**

---

## Decision Rationale

### Why Hybrid ? Cloud Migration?

1. **Current Reality:**
   - Some images already in database
   - Cannot break existing functionality
   - Need gradual migration

2. **Future Vision:**
   - Production needs scalability
   - Global users need CDN
   - Costs need optimization

3. **DDD Alignment:**
   - `HybridImage` value object encapsulates both strategies
   - Domain model stays clean (no leaky abstractions)
   - Infrastructure concerns properly separated

---

## DDD Implementation

### Domain Layer (Pure Business Logic)

```csharp
// Value Object - No infrastructure concerns
public sealed record HybridImage
{
    public Url? ImageUrl { get; init; }
    public ImageData? ImageData { get; init; }

    public string? GetImageSource() => 
        ImageData?.GetDataUri() ?? ImageUrl?.Value;
    
    public bool IsUrl() => ImageUrl != null;
    public bool IsDatabase() => ImageData != null;
}

// Entity - Domain rules only
public class FocusArea
{
    public HybridImage? Icon { get; private set; }

    public void UpdateIcon(HybridImage? newIcon)
    {
        // Domain rule: Icon is optional
        Icon = newIcon;
    }
}
```

### Application Layer (Use Cases)

```csharp
// Service - Orchestrates workflow
public class ImageService
{
    public async Task<HybridImage> ProcessAndUploadImageAsync(...)
    {
        // 1. Process (IImageProcessingService)
        // 2. Upload (IFileStorageService)
        // 3. Return HybridImage with URL
    }
}

// Command Handler - Application logic
public class UpdateFocusAreaHandler
{
    public async Task Handle(UpdateFocusAreaCommand request)
    {
        var icon = await _imageService.ProcessAndUploadImageAsync(...);
        focusArea.UpdateIcon(icon);
    }
}
```

### Infrastructure Layer (Technical Implementation)

```csharp
// Interface - Abstracts storage
public interface IFileStorageService
{
    Task<string> UploadImageAsync(...);
    Task<bool> DeleteImageAsync(string url);
}

// Implementation - Azure-specific
public class AzureBlobStorageService : IFileStorageService
{
    // Azure SDK calls here
}

// Implementation - Local files
public class LocalFileStorageService : IFileStorageService
{
    // File system calls here
}
```

---

## Dependency Rule Compliance

```
???????????????????????????????????????????
?         Domain Layer (Entities)         ? ? No dependencies
?  - HybridImage (Value Object)           ?
?  - FocusArea (Entity)                   ?
???????????????????????????????????????????
                 ?
                 ? (depends on)
???????????????????????????????????????????
?      Application Layer (Use Cases)      ? ? Depends on Domain
?  - ImageService                         ?
?  - IFileStorageService (interface)      ?
?  - UpdateFocusAreaHandler               ?
???????????????????????????????????????????
                 ?
                 ? (implements)
???????????????????????????????????????????
?   Infrastructure Layer (Implementation) ? ? Depends on Application
?  - AzureBlobStorageService              ?
?  - LocalFileStorageService              ?
?  - ImageProcessingService               ?
???????????????????????????????????????????
```

**? Dependency Rule Satisfied:**
- Domain has zero dependencies
- Application depends only on Domain
- Infrastructure implements Application interfaces

---

## Aggregate Boundaries

### FocusArea Aggregate

```csharp
public class FocusArea : IAggregateRoot
{
    public Guid Id { get; private set; }
    public Title TitleTr { get; private set; }
    public Description DescriptionTr { get; private set; }
    public HybridImage? Icon { get; private set; } // Part of aggregate

    // Icon is intrinsic to FocusArea
    // Deleting FocusArea should delete Icon
}
```

**Design Decision:**
- ? Icon is part of FocusArea aggregate
- ? Icon deletion handled by application service
- ? No separate Icon entity (it's a value object)

---

## Transaction Management

### Challenge: Distributed Transaction

When updating an entity with a new image:
1. Upload new image to blob storage
2. Update database with new URL
3. Delete old image from blob storage

**Problem:** These are 2 separate transactions (DB + Blob Storage)

### Solution: Compensating Transactions

```csharp
public async Task<HybridImage> UpdateImageAsync(
    HybridImage? currentImage,
    string newBase64Data,
    string containerName)
{
    HybridImage newImage;
    
    try
    {
        // Step 1: Upload new image FIRST
        newImage = await ProcessAndUploadImageAsync(newBase64Data, containerName);
        
        // Step 2: Database update (in calling handler)
        // ... database transaction ...
        
        // Step 3: Delete old image LAST (after DB commit)
        if (currentImage?.IsUrl() == true)
        {
            await DeleteImageAsync(currentImage);
        }
    }
    catch (Exception)
    {
        // Rollback: Delete newly uploaded image
        if (newImage?.IsUrl() == true)
        {
            await DeleteImageAsync(newImage);
        }
        throw;
    }
    
    return newImage;
}
```

**Strategy:**
- ? Upload before DB update (can retry safely)
- ? Delete after DB commit (old image still accessible if failure)
- ? Compensating transaction on failure

---

## Security Considerations

### 1. File Upload Validation

```csharp
// IImageProcessingService
public async Task<bool> ValidateImageAsync(string base64Data, int maxSizeBytes)
{
    // ? Check file size
    // ? Validate MIME type
    // ? Validate base64 format
    // ? Detect malicious content
}
```

### 2. Access Control

```csharp
// Public images: Use public blob containers
var containerClient = await GetOrCreateContainerAsync("focus-areas");
await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

// Private images: Use pre-signed URLs
var sasUrl = await _fileStorage.GetPresignedUrlAsync(imageUrl, expiresInMinutes: 60);
```

### 3. Rate Limiting

```csharp
[HttpPost("upload")]
[RateLimit(MaxRequests = 10, WindowMinutes = 60)] // Custom attribute
public async Task<IActionResult> UploadImage(...)
```

---

## Performance Optimization

### 1. Image Compression

```csharp
// Always compress before upload
var processedImage = await _imageProcessing.ProcessImageAsync(
    base64Data,
    fileName,
    maxWidth: 1920,
    maxHeight: 1080,
    quality: 85); // 85% quality = good balance
```

**Result:**
- Original: 2.5MB
- Compressed: 200KB
- Size reduction: 92%

### 2. CDN Integration

```json
{
  "AzureBlobStorage": {
    "ConnectionString": "...",
    "CdnEndpoint": "https://yourcdn.azureedge.net"
  }
}
```

**Benefits:**
- ? Global edge caching
- ? Reduced latency
- ? Bandwidth savings

### 3. Lazy Loading

```csharp
// Don't load images in list queries
public class FocusAreaListDto
{
    public Guid Id { get; set; }
    public string TitleTr { get; set; }
    // ? Don't include: public string IconBase64 { get; set; }
    // ? Include: public string? IconUrl { get; set; }
}
```

---

## Cost Analysis

### Database Storage (Current)

| Metric | Value |
|--------|-------|
| Storage cost | $0.15/GB/month (Azure SQL) |
| Backup cost | $0.20/GB/month |
| Average image size | 2MB |
| 10,000 images | 20GB = $7/month storage + $4/month backup = **$11/month** |

### Cloud Storage (Recommended)

| Metric | Value |
|--------|-------|
| Storage cost | $0.02/GB/month (Azure Blob) |
| Bandwidth cost | $0.05/GB (first 100GB free) |
| Average image size | 200KB (after compression) |
| 10,000 images | 2GB = $0.04/month storage + $0/month bandwidth = **$0.04/month** |

**Savings:** $10.96/month (99.6% reduction)

**Scaling:**
- 1 million images in DB: $1,100/month
- 1 million images in Blob: $4/month

---

## Migration Timeline

### Week 1-2: Infrastructure Setup ? DONE
- [x] Create IFileStorageService
- [x] Implement LocalFileStorageService
- [x] Implement AzureBlobStorageService
- [x] Create ImageService orchestrator
- [x] Update Program.cs registration

### Week 3-4: Update Handlers
- [ ] Identify all entities with images
- [ ] Update command handlers to use ImageService
- [ ] Test each handler individually

### Week 5-6: Data Migration
- [ ] Create migration scripts
- [ ] Run migration on staging environment
- [ ] Validate all images accessible
- [ ] Run migration on production

### Week 7: Cleanup
- [ ] Remove ImageData columns
- [ ] Create migration to drop columns
- [ ] Monitor for any issues

### Week 8: Production Deployment
- [ ] Switch to Azure Blob Storage
- [ ] Configure CDN
- [ ] Monitor performance metrics

---

## Monitoring & Alerting

### Metrics to Track

```csharp
// Log image operations
_logger.LogInformation(
    "Image uploaded: {FileName}, Size: {Size}KB, Container: {Container}",
    fileName, sizeKb, containerName);

_logger.LogWarning(
    "Image upload failed: {FileName}, Error: {Error}",
    fileName, ex.Message);
```

### Azure Monitor Alerts

1. **Storage usage > 80%** ? Upgrade tier
2. **Upload failure rate > 5%** ? Investigate
3. **CDN cache miss rate > 20%** ? Optimize caching

---

## Conclusion

**Recommended Path:**

1. ? **Current State:** Hybrid approach with `HybridImage`
2. ? **Transition:** Migrate existing images to cloud storage
3. ? **Future State:** URL-only with Azure Blob + CDN

**Benefits:**
- ?? 99% cost reduction
- ?? Better performance
- ?? Unlimited scalability
- ?? Global CDN delivery
- ?? Cleaner database

**Alignment:**
- ? Clean Architecture: Proper layer separation
- ? DDD: Value objects, aggregates respected
- ? SOLID: Single responsibility, dependency inversion
- ? Production-ready: Scalable, secure, cost-effective

---

**Decision:** Proceed with hybrid approach and plan migration to cloud storage within 3 months.

**Reviewed by:** Architecture Team  
**Approved by:** Technical Lead  
**Implementation Status:** Phase 1 Complete ?
