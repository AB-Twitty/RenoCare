using Microsoft.AspNetCore.Mvc;
using Reno.MVC.Models.Reports;
using Reno.MVC.Services.Base;
using System;
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
        [ValidateAntiForgeryToken]
        public ActionResult Save([FromForm] ReportDto report)
        {
            return View(report);
        }
    }
}
