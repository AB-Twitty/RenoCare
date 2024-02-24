using RenoCare.Domain.Identity;
using System.Threading.Tasks;

namespace RenoCare.Infrastructure.Authentication.Contracts
{
    /// <summary>
    /// Represents Jwt token provider service.
    /// </summary>
    public interface ITokenProvider
    {
        /// <summary>
        /// Generate Jwt token for the specified user.
        /// </summary>
        /// <param name="user">Current user to be authenticated.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task contains the jwt access token.
        /// </returns>
        public Task<string> GenerateTokenAsync(AppUser user);
    }
}
