using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RenoCare.Core.Features.Authentication.Contracts;
using RenoCare.Infrastructure.Authentication.Contracts;
using System;
using System.Text;

namespace RenoCare.Infrastructure.Authentication
{
    /// <summary>
    /// Represents the authentication infrastructure configuration.
    /// </summary>
    internal static class AuthenticationStartupConfiguration
    {
        /// <summary>
        /// Configure the authentication infrastructure.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <returns>A collection of service descriptors.</returns>
        public static IServiceCollection ConfigureAuthenticationInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ITokenProvider, TokenProvider>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            });

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtOptions =>
            {
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Key)),
                    ValidateIssuerSigningKey = true,
                    ValidAudience = JwtSettings.Audience,
                    ValidateAudience = true,
                    ValidIssuer = JwtSettings.Issuer,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                jwtOptions.Events = new JwtBearerEvents();
                jwtOptions.Events.OnTokenValidated = async (context) =>
                {
                    //cache the logged user for further need.
                };
            });

            return services;
        }
    }
}