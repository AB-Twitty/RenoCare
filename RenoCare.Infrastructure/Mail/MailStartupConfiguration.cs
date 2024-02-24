using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RenoCare.Core.Conatracts.Mail;
using System.Net;
using System.Net.Mail;

namespace RenoCare.Infrastructure.Mail
{
    /// <summary>
    /// Represents the mail infrastructure configuration.
    /// </summary>
    internal static class MailStartupConfiguration
    {
        /// <summary>
        /// Configure the identity persistence.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <param name="configuration">Represents a set of key/value application configuration settings.</param>
        /// <returns>A collection of service descriptors.</returns>
        public static IServiceCollection ConfigureMailInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddFluentEmail(configuration["EmailSettings:SmtpCredintials:username"])
                .AddSmtpSender(new SmtpClient()
                {
                    Host = configuration["EmailSettings:SmtpHost"],
                    Port = int.Parse(configuration["EmailSettings:SmtpPort"]),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential()
                    {
                        UserName = configuration["EmailSettings:SmtpCredintials:Username"],
                        Password = configuration["EmailSettings:SmtpCredintials:Password"]
                    },
                    EnableSsl = true
                }).AddRazorRenderer();

            return services;
        }

    }
}
