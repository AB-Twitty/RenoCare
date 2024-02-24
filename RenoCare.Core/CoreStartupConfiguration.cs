using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace RenoCare.Core
{
    /// <summary>
    /// Represents the core configuration.
    /// </summary>
    public static class CoreStartupConfiguration
    {
        /// <summary>
        /// Configure the core layer.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <returns>A collection of service descriptors.</returns>
        public static IServiceCollection ConfigureCore(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
