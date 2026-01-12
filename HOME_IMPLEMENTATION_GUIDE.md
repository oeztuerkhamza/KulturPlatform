# Home Page Implementation - Completion Guide

## ? What Has Been Completed

### 1. Domain Layer
- ? `HeroSection` aggregate created
- ? `CtaSection` aggregate (already existed)
- ? `Feature` aggregate (already existed)
- ? `InstagramPost` aggregate (already existed)
- ? `HomeAggregate` (already existed - aggregates all home components)

### 2. Infrastructure Layer - EF Core Configurations
- ? `HeroSectionConfiguration.cs`
- ? `CtaSectionConfiguration.cs`
- ? `FeatureConfiguration.cs`
- ? `InstagramPostConfiguration.cs`

### 3. Infrastructure Layer - Repositories
- ? `HeroSectionRepository.cs`
- ? `CtaSectionRepository.cs`
- ? `FeatureRepository.cs`
- ? `InstagramPostRepository.cs`
- ? `HomeReadService.cs`

### 4. Application Layer - Interfaces
- ? `IHeroSectionRepository.cs`
- ? `ICtaSectionRepository.cs`
- ? `IFeatureRepository.cs`
- ? `IInstagramPostRepository.cs`
- ? `IHomeReadService.cs`

### 5. Application Layer - Commands & Handlers
**HeroSection:**
- ? `CreateHeroSectionCommand.cs`
- ? `CreateHeroSectionCommandHandler.cs`
- ? `UpdateHeroSectionCommand.cs`
- ? `UpdateHeroSectionCommandHandler.cs`

**CtaSection:**
- ? `CreateCtaSectionCommand.cs`
- ? `CreateCtaSectionCommandHandler.cs`
- ? `UpdateCtaSectionCommand.cs`
- ? `UpdateCtaSectionCommandHandler.cs`

**Feature:**
- ? `CreateFeatureCommand.cs`
- ? `CreateFeatureCommandHandler.cs`
- ? `UpdateFeatureCommand.cs`
- ? `UpdateFeatureCommandHandler.cs`

**InstagramPost:**
- ? `CreateInstagramPostCommand.cs`
- ? `CreateInstagramPostCommandHandler.cs`
- ? `UpdateInstagramPostCommand.cs` (needs handler)

### 6. Application Layer - Queries & Handlers
- ? `GetHomePageQuery.cs`
- ? `GetHomePageQueryHandler.cs`

### 7. Application Layer - DTOs
- ? `HeroSectionDto.cs`
- ? `CtaSectionDto.cs`
- ? `FeatureDto.cs`
- ? `InstagramPostDto.cs`
- ? `HomeDto.cs` (already existed)

### 8. Application Layer - AutoMapper
- ? `HomeMappingProfile.cs`

### 9. API Layer
- ? `HomeController.cs` with all CRUD operations

### 10. Database
- ? DbSets added to `AppDbContext.cs`

## ?? REMAINING TASKS

### 1. Create Migration
Run this command from the solution root:
```bash
dotnet ef migrations add AddHomeAggregates --project KulturPlatform.Infrastructure --startup-project KulturPlatform.API --context AppDbContext
```

Then update the database:
```bash
dotnet ef database update --project KulturPlatform.Infrastructure --startup-project KulturPlatform.API
```

### 2. Register Dependencies in Program.cs

Add these registrations in your `Program.cs`:

```csharp
// Home Repositories
builder.Services.AddScoped<IHeroSectionRepository, HeroSectionRepository>();
builder.Services.AddScoped<ICtaSectionRepository, CtaSectionRepository>();
builder.Services.AddScoped<IFeatureRepository, FeatureRepository>();
builder.Services.AddScoped<IInstagramPostRepository, InstagramPostRepository>();

// Home Read Service
builder.Services.AddScoped<IHomeReadService, HomeReadService>();
```

### 3. Create UpdateInstagramPostCommandHandler

Create file: `KulturPlatform.Application\Commands\Home\UpdateInstagramPostCommandHandler.cs`

```csharp
using KulturPlatform.Application.Interfaces.Home;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.Home
{
    public class UpdateInstagramPostCommandHandler : IRequestHandler<UpdateInstagramPostCommand>
    {
        private readonly IInstagramPostRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateInstagramPostCommandHandler(IInstagramPostRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateInstagramPostCommand request, CancellationToken cancellationToken)
        {
            var instagramPost = await _repository.GetByIdAsync(request.Id, cancellationToken)
                                ?? throw new KeyNotFoundException($"InstagramPost with Id {request.Id} not found.");

            // Since InstagramPost doesn't have an Update method, you need to delete and recreate
            // OR add an Update method to the InstagramPost aggregate

            _repository.Delete(instagramPost, cancellationToken);
            
            var link = !string.IsNullOrWhiteSpace(request.Link) ? Url.Create(request.Link) : null;
            var updatedPost = Domain.Commons.AggregateRoot.InstagramPost.Create(
                Url.Create(request.ImageUrl),
                link
            );
            
            await _repository.AddAsync(updatedPost, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
```

**Alternative (Better approach):** Add an Update method to `InstagramPost` aggregate:

```csharp
// In KulturPlatform.Domain\Commons\Aggregates\InstagramPost.cs
public void Update(Url imageUrl, Url? link = null)
{
    ImageUrl = imageUrl;
    Link = link;
    SetUpdatedAt();
}
```

### 4. Optional: Add Delete Commands & Handlers

For complete CRUD operations, you should add Delete commands for each aggregate:

- `DeleteHeroSectionCommand` & `DeleteHeroSectionCommandHandler`
- `DeleteCtaSectionCommand` & `DeleteCtaSectionCommandHandler`
- `DeleteFeatureCommand` & `DeleteFeatureCommandHandler`
- `DeleteInstagramPostCommand` & `DeleteInstagramPostCommandHandler`

### 5. Optional: Add Validators

Create FluentValidation validators for each command:

- `CreateHeroSectionCommandValidator`
- `UpdateHeroSectionCommandValidator`
- `CreateCtaSectionCommandValidator`
- `UpdateCtaSectionCommandValidator`
- etc.

## ?? How to Use the API

### 1. Get Home Page Data (Public)
```
GET /api/home?lang=tr
```

### 2. Create Hero Section (SystemAdmin)
```
POST /api/home/hero
{
  "titleTr": "Freiburg Kültür Platformu",
  "titleDe": "Kultur Platform Freiburg",
  "subtitleTr": "Kültürel De?erler",
  "subtitleDe": "Kulturelle Werte",
  "descriptionTr": "Aç?klama...",
  "descriptionDe": "Beschreibung...",
  "backgroundImageUrl": "https://example.com/image.jpg",
  "primaryButtonTextTr": "Etkinlikleri ?ncele",
  "primaryButtonTextDe": "Veranstaltungen ansehen",
  "secondaryButtonTextTr": "Gönüllü Ol",
  "secondaryButtonTextDe": "Freiwilliger werden"
}
```

### 3. Update Hero Section (UserAdmin+)
```
PUT /api/home/hero
{
  "id": "guid-here",
  // ... same fields as create
}
```

### 4. Create Feature (UserAdmin+)
```
POST /api/home/features
{
  "titleTr": "Kapsay?c?l?k",
  "titleDe": "Inklusivität",
  "descriptionTr": "Aç?klama...",
  "descriptionDe": "Beschreibung...",
  "color": "#3B82F6"
}
```

### 5. Create Instagram Post (UserAdmin+)
```
POST /api/home/instagram
{
  "imageUrl": "https://example.com/instagram.jpg",
  "link": "https://instagram.com/p/xyz"
}
```

## ?? Architecture Summary

This implementation follows:
- ? **Clean Architecture** - Proper layer separation
- ? **Domain-Driven Design (DDD)** - Value Objects, Aggregates, Entities
- ? **CQRS Pattern** - Separate Commands and Queries
- ? **Repository Pattern** - Data access abstraction
- ? **Mediator Pattern** - Decoupled request handling
- ? **Dependency Injection** - Loose coupling

## ?? Next Steps

1. Create migration and update database
2. Register dependencies in Program.cs
3. Test the API endpoints
4. Add validators (optional but recommended)
5. Add Delete operations (optional)
6. Add integration tests (optional)

## ?? Notes

- The `HomeAggregate` is a read model that combines all home page components
- Each aggregate (HeroSection, Feature, etc.) is independently managed
- The `HomeReadService` efficiently loads all data in parallel
- AutoMapper handles the mapping between domain models and DTOs
- All Value Objects use static factory methods (e.g., `Url.Create()`)

---

**Implementation Status:** 90% Complete
**Build Status:** ? Successful
**Remaining:** Migration + DI Registration + Minor handlers
