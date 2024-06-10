namespace RenoCare.Core.Features.Authentication.Contracts.Models
{
    /// <summary>
    /// Represents a request to set password for first time using OTP code.
    /// </summary>
    public class OtpPasswordSetRequest
    {
        /// <summary>
        /// Represents the user email.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Represents the user first password.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Represents the OTP code.
        /// </summary>
        public string OTP { get; set; }
    }
}
