using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Domain;
using RenoCare.Domain.Identity;
using RenoCare.Infrastructure.Authentication.Contracts;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RenoCare.Infrastructure.Authentication
{
    /// <summary>
    /// Represents Jwt token provider service.
    /// </summary>
    public class TokenProvider : ITokenProvider
    {
        #region Fields

        private readonly IRepository<DialysisUnit> _dialysisUnitRepo;
        private readonly UserManager<AppUser> _userManager;

        #endregion

        #region Ctor

        public TokenProvider(UserManager<AppUser> userManager, IRepository<DialysisUnit> dialysisUnitRepo)
        {
            _userManager = userManager;
            _dialysisUnitRepo = dialysisUnitRepo;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Generate Jwt token for the specified user.
        /// </summary>
        /// <param name="user">Current user to be authenticated.</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task contains the jwt access token.
        /// </returns>
        public async Task<string> GenerateTokenAsync(AppUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);

            var rolesClaims = (await _userManager.GetRolesAsync(user))
                .Select(r => new Claim(ClaimTypes.Role, r));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }.Union(userClaims).Union(rolesClaims);

            if (rolesClaims.Where(x => x.Value == "HealthCare").Any())
            {
                var unitName = await _dialysisUnitRepo.Table
                    .Where(x => x.UserId == user.Id).Select(x => x.Name).FirstOrDefaultAsync();
                if (unitName != null)
                    claims = claims.Append(new Claim("unit", unitName));
            }


            var credentials = new SigningCredentials
            (
                key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Key)),
                algorithm: SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken
            (
                issuer: JwtSettings.Issuer,
                audience: JwtSettings.Audience,
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.Now.AddMinutes(JwtSettings.ExpiredAfterMinutes)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion
    }
}
