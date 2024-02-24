namespace RenoCare.Core.Features.Authentication.Contracts.Models
{
    /// <summary>
    /// Represents the result of an authentication process.
    /// </summary>
    public class AuthResponse
    {
        /// <summary>
        /// Represents the user Id.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Represents the first name of the user.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Represents the last name of the user.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Represents the JWT token for subsequent requests.
        /// </summary>
        public string AccessToken { get; set; }
    }
}
