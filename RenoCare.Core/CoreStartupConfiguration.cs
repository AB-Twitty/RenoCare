using FluentValidation;
using IronPdf;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using RenoCare.Core.Base;
using System;
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

            services.AddSignalR(o =>
            {
                o.KeepAliveInterval = TimeSpan.FromMinutes(5);
                o.ClientTimeoutInterval = TimeSpan.FromMinutes(10);
                o.HandshakeTimeout = TimeSpan.FromMinutes(5);
            });
            //GlobalHost.HubPipeline.RequireAuthentication();

            License.LicenseKey = "IRONSUITE.MOCANEVA2.GMAIL.COM.18342-3040EC1DB1-KGKPW-MPN32LH3MXQH-AAW5EVEMLJ6K-SRGNB2LBUSS3-XVXODVH2QBJF-JJBEOTKBJMB7-YNNDIIC4LHQF-HIQGIH-THV3MD5J2B6NEA-DEPLOYMENT.TRIAL-IIW6NX.TRIAL.EXPIRES.25.JUL.2024";

            services.AddHostedService<MedReqCheckBackgroundService>();
            services.AddHostedService<Notify24HourBeforeBooking>();


            return services;
        }
    }
}
