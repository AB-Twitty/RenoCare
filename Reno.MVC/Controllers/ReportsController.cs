using Microsoft.AspNetCore.Mvc;
using Reno.MVC.Models.Reports;

namespace Reno.MVC.Controllers
{
    public class ReportsController : Controller
    {
        public IActionResult Index(int patientId)
        {
            var model = new ReportIndexVM
            {
                PatientId = patientId,
                PatientName = "Ahmed Ali",
                ReportNephrologist = "Dr.Khaled Abdel-Ghaffar"
            };

            return View(model);
        }
    }
}
