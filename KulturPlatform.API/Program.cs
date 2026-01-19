using FluentValidation;
using KulturPlatform.API.Configuration;
using KulturPlatform.Application;
using KulturPlatform.Application.Commands.Auth;
using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Application.Interfaces.Activity;
using KulturPlatform.Application.Interfaces.Admin;
using KulturPlatform.Application.Interfaces.Auth;
using KulturPlatform.Application.Interfaces.ContactInfo;
using KulturPlatform.Application.Interfaces.Course;
using KulturPlatform.Application.Interfaces.Dashboard;
using KulturPlatform.Application.Interfaces.DonatePage;
using KulturPlatform.Application.Interfaces.GuelenMovement;
using KulturPlatform.Application.Interfaces.Home;
using KulturPlatform.Application.Interfaces.Imprint;
using KulturPlatform.Application.Interfaces.LocalizationResource;
using KulturPlatform.Application.Interfaces.PageContent;
using KulturPlatform.Application.Interfaces.Partner;
using KulturPlatform.Application.Interfaces.Satzung;
using KulturPlatform.Application.Interfaces.TeaEvent;
using KulturPlatform.Application.Interfaces.ValueItem;
using KulturPlatform.Application.Interfaces.VolunteerSubmission;
using KulturPlatform.Domain.Interfaces;
using KulturPlatform.Infrastructure;
using KulturPlatform.Infrastructure.ReadServices;
using KulturPlatform.Infrastructure.Repositories;
using KulturPlatform.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:3001",
                "https://localhost:3001",
                "http://localhost:5173",
                "https://localhost:5173"
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// Add services to the container.

builder.Services.AddControllers();

// Memory Cache (for LocalizationService)
builder.Services.AddMemoryCache();

// CORS
//builder.Services.ConfigureCors();

// Localization
builder.Services.ConfigureLocalization();

// JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"] ?? "YourSuperSecretKeyThatIsAtLeast32CharactersLong!";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"] ?? "KulturPlatform",
        ValidAudience = jwtSettings["Audience"] ?? "KulturPlatformApp",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        // ✅ Claim type mappings
        NameClaimType = System.Security.Claims.ClaimTypes.NameIdentifier,
        RoleClaimType = System.Security.Claims.ClaimTypes.Role
    };

    // ✅ Debug için event handlers
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            var authHeader = context.Request.Headers["Authorization"].ToString();
            logger.LogWarning("📨 OnMessageReceived - Authorization Header: [{Header}]", authHeader);

            if (!string.IsNullOrEmpty(authHeader))
            {
                var parts = authHeader.Split(' ');
                logger.LogWarning("📨 Header parts count: {Count}", parts.Length);
                if (parts.Length == 2)
                {
                    logger.LogWarning("📨 Scheme: [{Scheme}], Token length: {Length}", parts[0], parts[1]?.Length ?? 0);
                    var tokenDots = parts[1]?.Count(c => c == '.');
                    logger.LogWarning("📨 Token dots count: {Dots}", tokenDots);
                }
            }
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogError("❌ JWT Authentication failed: {Exception}", context.Exception.Message);
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            var userId = context.Principal?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var role = context.Principal?.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
            logger.LogInformation("✅ JWT Token validated - UserId: {UserId}, Role: {Role}", userId, role);
            return Task.CompletedTask;
        }
    };
});
builder.Services.AddAutoMapper(cfg => { }, typeof(ApplicationMarker).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(ApplicationMarker).Assembly);

// Authorization with role-based policies
builder.Services.ConfigureAuthorization();

// OpenAPI - will be used by Scalar
builder.Services.AddScalarServices();

// MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(KulturPlatform.Application.ApplicationMarker).Assembly);
    cfg.AddOpenBehavior(typeof(KulturPlatform.Application.Behaviors.ValidationBehavior<,>));
}); ;
// Database Context - SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);

    // ⚠️ TEMPORARY: Suppress pending model changes warning in development
    if (builder.Environment.IsDevelopment())
    {
        options.ConfigureWarnings(warnings =>
            warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
    }
});

// Token Service
builder.Services.AddScoped<ITokenService, TokenService>();

// Dashboard Service
builder.Services.AddScoped<IDashboardService, DashboardService>();

// UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Admin
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IAdminReadService, AdminReadService>(); // ✅ Added

// LocalizationResource
builder.Services.AddScoped<ILocalizationResourceRepository, LocalizationResourceRepository>();
builder.Services.AddScoped<ILocalizationResourceReadService, LocalizationResourceReadService>();

// Activity
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IActivityReadService, ActivityReadService>();

// Course
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseReadService, CourseReadService>();

// Partner
builder.Services.AddScoped<IPartnerRepository, PartnerRepository>();
builder.Services.AddScoped<IPartnerReadService, PartnerReadService>();

// TeamMember

// ValueItem
builder.Services.AddScoped<IValueItemWriteRepository, ValueItemWriteRepository>();
builder.Services.AddScoped<IValueItemReadRepository, ValueItemReadRepository>();

// VolunteerSubmission
builder.Services.AddScoped<IVolunteerSubmissionRepository, VolunteerSubmissionRepository>();
builder.Services.AddScoped<IVolunteerSubmissionReadService, VolunteerSubmissionReadService>();

// PageContent
builder.Services.AddScoped<IPageContentRepository, PageContentRepository>();
builder.Services.AddScoped<IPageContentReadService, PageContentReadService>();

// Satzung
builder.Services.AddScoped<ISatzungRepository, SatzungRepository>();
builder.Services.AddScoped<ISatzungReadService, SatzungReadService>();

// TeaEvent
builder.Services.AddScoped<ITeaEventWriteRepository, TeaEventWriteRepository>();
builder.Services.AddScoped<ITeaEventReadRepository, TeaEventReadRepository>();

// DonatePage
builder.Services.AddScoped<IDonatePageRepository, DonatePageRepository>();
builder.Services.AddScoped<IDonatePageReadService, DonatePageReadService>();

// GuelenMovement
builder.Services.AddScoped<IGuelenMovementRepository, GuelenMovementRepository>();
builder.Services.AddScoped<IGuelenMovementReadService, GuelenMovementReadService>();

// Imprint
builder.Services.AddScoped<IImprintRepository, ImprintRepository>();
builder.Services.AddScoped<IImprintReadService, ImprintReadService>();

// ContactInfo
builder.Services.AddScoped<IContactInfoRepository, ContactInfoRepository>();
builder.Services.AddScoped<IContactInfoReadService, ContactInfoReadService>();

// ContactMessage
builder.Services.AddScoped<KulturPlatform.Application.Interfaces.ContactMessages.IContactMessageRepository, ContactMessageRepository>();
builder.Services.AddScoped<KulturPlatform.Application.Interfaces.ContactMessages.IContactMessageReadService, ContactMessageReadService>();

// About Us - New Structure
builder.Services.AddScoped<IAboutUsQuoteRepository, AboutUsQuoteRepository>();
builder.Services.AddScoped<IAboutUsWhoWeAreRepository, AboutUsWhoWeAreRepository>();
builder.Services.AddScoped<IAboutUsGoalsRepository, AboutUsGoalsRepository>();
builder.Services.AddScoped<IAboutUsVisionRepository, AboutUsVisionRepository>();
builder.Services.AddScoped<IAboutUsMissionRepository, AboutUsMissionRepository>();
builder.Services.AddScoped<IAboutUsHumanRightsRepository, AboutUsHumanRightsRepository>();
builder.Services.AddScoped<ICoreValueRepository, CoreValueRepository>();
builder.Services.AddScoped<IFocusAreaRepository, FocusAreaRepository>();
builder.Services.AddScoped<IActivityAreaRepository, ActivityAreaRepository>();
builder.Services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();
builder.Services.AddScoped<IAboutUsAggregateReadService, AboutUsAggregateReadService>();

// Home Page
builder.Services.AddScoped<IHeroSectionRepository, HeroSectionRepository>();
builder.Services.AddScoped<ICtaSectionRepository, CtaSectionRepository>();
builder.Services.AddScoped<IFeatureRepository, FeatureRepository>();
builder.Services.AddScoped<IInstagramPostRepository, InstagramPostRepository>();
builder.Services.AddScoped<IHomeReadService, HomeReadService>();


var app = builder.Build();

// Database migration
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        var db = services.GetRequiredService<AppDbContext>();

        // Apply pending migrations
        logger.LogInformation("Applying database migrations...");
        db.Database.Migrate();
        logger.LogInformation("Database migrations applied successfully.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "❌ An error occurred while migrating the database.");
        throw;
    }
}


// Configure Scalar UI
if (app.Environment.IsDevelopment())
{
    var configuration = app.Services.GetRequiredService<IConfiguration>();
    var token = configuration["Scalar:Token"] ?? "";

    // OpenAPI endpoint
    app.MapOpenApi();
    app.MapPost("/api/auth/dev-token", async (IMediator mediator) =>
    {
        try
        {
            var command = new LoginCommand("admin@kpf.de", "Admin123!");
            var result = await mediator.Send(command);

            return Results.Ok(new
            {
                token = result.Token,
            });
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new
            {
                error = ex.Message,
                hint = "Make sure the database has admin user"
            });
        }
    })
        .AllowAnonymous()
        .WithTags("Authentication")
        .WithName("GetDevToken")
        .WithSummary("🚀 Get Token (One Click!)")
        .WithDescription("**DEVELOPMENT ONLY** - Instantly get a JWT token with admin credentials. No email/password needed!");

    // Scalar API Reference
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("KulturPlatform API Documentation")
            .WithTheme(ScalarTheme.BluePlanet)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
            .WithHttpBearerAuthentication(auth =>
            {
                auth.Token = token;
            });
    });
}




// Root redirect to Scalar UI
app.MapGet("/", () => Results.Redirect("/scalar/v1"));

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
