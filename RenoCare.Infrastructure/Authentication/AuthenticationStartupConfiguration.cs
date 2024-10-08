﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RenoCare.Core.Base;
using RenoCare.Core.Features.Authentication.Contracts;
using RenoCare.Infrastructure.Authentication.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
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
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
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
                    var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                    context.HttpContext.User.AddIdentity(claimsIdentity);

                    if (context.HttpContext.User.IsInRole("HealthCare"))
                    {
                        var unitId = context.HttpContext.User.Claims.Where(x => x.Type == "uid").FirstOrDefault();
                        var unitName = context.HttpContext.User.Claims.Where(x => x.Type == "unit").FirstOrDefault();

                        if (unitId != null && unitName != null)
                        {
                            context.HttpContext.Items["unitId"] = int.Parse(unitId.Value);
                            context.HttpContext.Items["unit"] = unitName.Value;
                        }
                    }
                };

                jwtOptions.Events.OnAuthenticationFailed = async (context) =>
                {
                    var response = new ApiResponse<string>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = "Token Authentication Failure"
                    };

                    // Use this method to write to the response body after invoking the next middleware
                    context.Response.OnStarting(async () =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsJsonAsync(response); //problem when parsing from JSON
                    });
                };

                jwtOptions.Events.OnChallenge = async (context) =>
                {
                    var response = new ApiResponse<string>
                    {
                        StatusCode = HttpStatusCode.Unauthorized,
                        Message = "Unauthorized Access"
                    };

                    // Call this to skip the default logic and avoid using the default response
                    context.HandleResponse();

                    // Set the status code and content type
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Response.ContentType = "application/json";

                    // Write the apiResponse as JSON in the response body
                    await context.Response.WriteAsJsonAsync(response);
                };

            });

            services.AddSwaggerGen(config =>
            {
                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the bearer scheme. \r\n\r\n
						Enter 'Bearer' [Space] and then your token in the text input below. \r\n\r\n
						Example : Bearer 12345abcdef",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                config.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });

                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "RenoCare API"
                });

                /*config.CustomSchemaIds(type =>
                {
                    var typeName = type.Name;
                    if (type.IsGenericType)
                    {
                        var genericArgs = string.Join(", ", type.GetGenericArguments().ToList());

                        int index = typeName.IndexOf('`');
                        var typeNameWithoutGenericArity = index == -1 ? typeName : typeName.Substring(0, index);

                        return $"{typeNameWithoutGenericArity}<{genericArgs}>";
                    }
                    return typeName;
                });*/

            });

            return services;
        }
    }
}