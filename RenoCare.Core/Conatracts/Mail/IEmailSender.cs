using System.Threading.Tasks;

namespace RenoCare.Core.Conatracts.Mail
{
    /// <summary>
    /// Represents an email sender service.
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends an email.
        /// </summary>
        /// <param name="email">Contains the information needed to send an email.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains a value indicating whether the email has been sent or not.
        /// </returns>
        public Task<bool> SendEmailAsync(Email email, object values);
    }
}
