using FluentEmail.Core;
using RenoCare.Core.Conatracts.Mail;
using System.Threading.Tasks;

namespace RenoCare.Infrastructure.Mail
{
    /// <summary>
    /// Represents an email sender service.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        #region Fields

        private readonly IFluentEmail _fluentEmail;

        #endregion

        #region Ctor

        public EmailSender(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends an email.
        /// </summary>
        /// <param name="email">Contains the information needed to send an email.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains a value indicating whether the email has been sent or not.
        /// </returns>
        public async Task<bool> SendEmailAsync(Core.Conatracts.Mail.Email email, object values)
        {
            var response = await _fluentEmail
                .To(email.ToAddress)
                .Subject(email.Subject)
                .UsingTemplate(email.Body, values)
                .SendAsync();

            return response.Successful;
        }

        #endregion
    }
}
