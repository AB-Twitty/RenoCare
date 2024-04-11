using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Persistence.Identity;

namespace RenoCare.Persistence
{
    /// <summary>
    /// Represents the persistence configuration.
    /// </summary>
    public static class PersistenceStartupConfiguration
    {
        /// <summary>
        /// Configure the persistence layer.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <param name="configuration">Represents a set of key/value application configuration settings.</param>
        /// <returns>A collection of service descriptors.</returns>
        public static IServiceCollection ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureIdentityPersistence(configuration);

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));

            services.AddTransient(typeof(IRepository<>), typeof(EntityRepository<>));

            return services;
        }
    }
}
