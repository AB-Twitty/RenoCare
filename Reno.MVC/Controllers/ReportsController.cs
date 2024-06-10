using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reno.MVC.Models.Reports;
using Reno.MVC.Services.Base;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Reno.MVC.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IClient _client;

        public ReportsController(IClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> Index([FromQuery] int id)
        {
            var report_response = await _client.GetReportAsync(id);

            return View(new ReportIndexVM { Report = report_response.Data });
        }

        [HttpGet("{controller}/{action}/{patientId}/{requestId}")]
        [Authorize(Roles = "HealthCare")]
        public async Task<IActionResult> Create(int requestId, int patientId)
        {
            var report = new ReportDto
            {
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                SessionDate = DateTime.Now.AddDays(-62),
                DialysisUnitName = "healthcare unit name"
            };

            var patientDto = (await _client.GetPatientMedicalInfoAsync(patientId)).Data;

            report.Patient = patientDto;

            var model = new ReportIndexVM { Report = report, ViewMode = "Create" };

            return View("Index", model);
        }

        [HttpPost("{controller}/{action}")]
        [Authorize(Roles = "HealthCare")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save([FromForm] ReportDto report, int patientId)
        {
            try
            {
                var result = await _client.CreateReportAsync(report);

            }
            catch (ApiException<ApiResponse<ReportDto>> ex)
            {
                var errors = ex.Result.Errors;
                if (errors?.Any() ?? false)
                {
                    foreach (var error in errors)
                    {
                        var key = error.Split(" : ")[0];
                        var msg = error.Split(" : ")[1];

                        ModelState.AddModelError(key, msg);
                    }
                }
            }

            report.Patient = (await _client.GetPatientMedicalInfoAsync(patientId)).Data;
            var model = new ReportIndexVM { Report = report, ViewMode = "Create" };
            return View("Index", model);
        }
    }
}
