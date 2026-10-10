using KulturPlatform.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KulturPlatform.API.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove all DbContext-related service descriptors
            var descriptorsToRemove = services
                .Where(d => d.ServiceType.IsGenericType &&
                           (d.ServiceType.GetGenericTypeDefinition() == typeof(DbContextOptions<>) ||
                            d.ServiceType == typeof(DbContextOptions)) ||
                           d.ServiceType == typeof(AppDbContext) ||
                           // EF Core 9 keeps the SQL Server setup in this descriptor too.
                           d.ServiceType == typeof(Microsoft.EntityFrameworkCore.Infrastructure.IDbContextOptionsConfiguration<AppDbContext>))
                .ToList();

            foreach (var descriptor in descriptorsToRemove)
            {
                services.Remove(descriptor);
            }

            // Add InMemory database for testing
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDatabase");
            });
        });

        builder.UseEnvironment("Testing");

        // appsettings.json ships an empty key; without one every request fails
        // while the JWT bearer handler builds its signing key.
        builder.UseSetting("JwtSettings:SecretKey", "integration-tests-only-signing-key-0123456789");
    }
}
