using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reno.MVC.Services.Base;
using System.Threading.Tasks;

namespace Reno.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClient _client;

        public HomeController(ILogger<HomeController> logger, IClient client)
        {
            _logger = logger;
            _client = client;
        }


        [Authorize(Roles = "Admin, HealthCare")]
        public async Task<IActionResult> Index()
        {
            var model = (await _client.GetDashboardAsync()).Data;

            return View(model);
        }

    }
}
