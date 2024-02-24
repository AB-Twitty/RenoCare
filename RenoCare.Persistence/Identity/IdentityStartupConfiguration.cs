using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RenoCare.Domain.Identity;

namespace RenoCare.Persistence.Identity
{
    /// <summary>
    /// Represents the identity persestence configuration.
    /// </summary>
    internal static class IdentityStartupConfiguration
    {
        /// <summary>
        /// Configure the identity persistence.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <param name="configuration">Represents a set of key/value application configuration settings.</param>
        /// <returns>A collection of service descriptors.</returns>
        public static IServiceCollection ConfigureIdentityPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnectionString")));

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
