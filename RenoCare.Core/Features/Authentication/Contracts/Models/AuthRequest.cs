namespace RenoCare.Core.Features.Authentication.Contracts.Models
{
    /// <summary>
    /// Represents properties used to authenticate a user request.
    /// </summary>
    public class AuthRequest
    {
        /// <summary>
        /// Represents the user email.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Represents the user password.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Represents a value indicating whether the authentication is for current use only or not.
        /// </summary>
        public bool RememberMe { get; set; }
    }
}
