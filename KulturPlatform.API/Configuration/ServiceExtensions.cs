using KulturPlatform.API.Authorization;
using KulturPlatform.Application.Interfaces.Localization;
using KulturPlatform.Domain.Commons.Constants;
using KulturPlatform.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;

namespace KulturPlatform.API.Configuration
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Configure CORS policy
        /// </summary>
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                );
            });
        }

        /// <summary>
        /// Configure Localization services
        /// </summary>
        public static void ConfigureLocalization(this IServiceCollection services)
        {
            services.AddScoped<ILocalizationService, LocalizationService>();
        }

        /// <summary>
        /// Configure Authorization policies with role-based access
        /// Best Practice: Clear hierarchy - SystemAdmin > UserAdmin > User
        /// </summary>
        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationHandler, GroupRequirementHandler>();

            services.AddAuthorization(options =>
            {
                // SystemAdmin - Full system access (Delete operations, system configuration)
                options.AddPolicy(Policies.RequireSystemAdmin, policy =>
                    policy.AddRequirements(new GroupRequirement(Roles.SystemAdmin)));

                // UserAdmin - Content management access (Create, Update operations)
                // SystemAdmin also has UserAdmin permissions
                options.AddPolicy(Policies.RequireUserAdmin, policy =>
                    policy.AddRequirements(new GroupRequirement(Roles.UserAdmin, Roles.SystemAdmin)));

                // User - Basic authenticated access (Read protected content)
                // All roles have User permissions
                options.AddPolicy(Policies.RequireUser, policy =>
                    policy.AddRequirements(new GroupRequirement(Roles.User, Roles.UserAdmin, Roles.SystemAdmin)));
            });
        }
    }
}
