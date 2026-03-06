using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;

namespace KulturPlatform.API.Configuration
{
    public static class ScalarConfiguration
    {
        public static IServiceCollection AddScalarServices(this IServiceCollection services)
        {
            services.AddOpenApi("v1", options =>
            {
                options.AddDocumentTransformer((document, context, cancellationToken) =>
                {
                    document.Info.Title = "KulturPlatform API";
                    document.Info.Description =
                        "Kultur Platform API with JWT Authentication.\n\n" +
                        "**Login:** POST /api/auth/login\n\n" +
                        "**Credentials:** email: admin@kpf.de, password: Admin123!";

                    // SECURITY SCHEME EKLE
                    document.Components ??= new OpenApiComponents();
                    document.Components.SecuritySchemes ??= new Dictionary<string, OpenApiSecurityScheme>();

                    document.Components.SecuritySchemes.Add("bearerAuth", new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Name = "Authorization",
                        Description = "JWT Bearer Token"
                    });

                    return Task.CompletedTask;
                });
            });


            return services;
        }
    }
}
