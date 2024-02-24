using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RenoCare.Infrastructure.Authentication;
using RenoCare.Infrastructure.Mail;

namespace RenoCare.Infrastructure
{
    /// <summary>
    /// Represents the infrastructure configuration.
    /// </summary>
    public static class InfrastructureStartupConfiguration
    {
        /// <summary>
        /// Configure the infrastructure layer.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <returns>A collection of service descriptors.</returns>
        public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureAuthenticationInfrastructure()
                .ConfigureMailInfrastructure(configuration);

            return services;
        }
    }
}

