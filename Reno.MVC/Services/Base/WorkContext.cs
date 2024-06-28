using Microsoft.AspNetCore.Http;
using Reno.MVC.Services.Base.Contracts;
using System.Linq;

namespace Reno.MVC.Services.Base
{
    public class WorkContext : IWorkContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WorkContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext
                .User.Claims.Where(c => c.Type == "sub").FirstOrDefault()?.Value;

            return userId;
        }
    }
}
