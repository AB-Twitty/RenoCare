using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using RenoCare.Core.Base;
using System.Linq;
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

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Configure FluentValidation DisplayNameResolver
            ValidatorOptions.Global.DisplayNameResolver = (type, memberInfo, lambdaExpression) =>
            {
                if (memberInfo != null)
                {
                    // Split the member name by spaces and return the last part
                    return memberInfo.Name.Split(' ').Last();
                }
                return null;
            };

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddHttpContextAccessor();

            services.AddSignalR();
            //GlobalHost.HubPipeline.RequireAuthentication();

            return services;
        }
    }
}
