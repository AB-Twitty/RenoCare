using Microsoft.AspNetCore.Identity;

namespace RenoCare.Domain.Identity
{
    /// <summary>
    /// Represents the user entity.
    /// </summary>
    public class AppUser : IdentityUser
    {
        /// <summary>
        /// Represents the first name of the user.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Represents the last name of the user.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Represents a value indicating whether the user is deleted or not.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
