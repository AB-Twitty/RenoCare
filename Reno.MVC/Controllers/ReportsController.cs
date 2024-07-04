using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reno.MVC.Helpers.Contracts;
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
        private readonly IDataTableInfoExtractor _dataTableExtractor;

        public ReportsController(IClient client, IDataTableInfoExtractor dataTableExtractor)
        {
            _client = client;
            _dataTableExtractor = dataTableExtractor;
        }


        [HttpGet("/Medication/Request/Details/{requestId}")]
        [Authorize(Roles = "Admin, HealthCare")]
        public async Task<IActionResult> MedReqDetailsAsync([FromRoute] int requestId)
        {
            var medReq = (await _client.GetMedicationRequestDetailsAsync(requestId)).Data;

            var allMedReq = (await _client.GetMedReqAllForPatientAsync(medReq.PatientId)).Data;

            var previous_reports = allMedReq.Where(x => x.Status == "Completed" && x.ReportId != null)
                .Select(x => (x.Date, x.ReportId.Value)).OrderBy(x => x.Date).ToList();

            var patient = (await _client.GetPatientMedicalInfoAsync(medReq.PatientId)).Data;

            var report = new ReportDto
            {
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                SessionDate = medReq.Date,
                Patient = patient,
                DialysisUnitName = User.Claims.Where(x => x.Type == "unit").FirstOrDefault()?.Value
            };

            var model = new ReportIndexVM { PrevReports = previous_reports, MedReq = medReq, Report = report, ViewMode = "View" };

            return View("Index", model);
        }

        [HttpGet("Reports/{id}")]
        [Authorize(Roles = "Admin, HealthCare")]
        public async Task<IActionResult> ViewReport([FromRoute] int id)
        {
            var report_response = await _client.GetReportAsync(id);

            var medReq = (await _client.GetMedicationRequestDetailsAsync(report_response.Data.MedReqId)).Data;

            var allMedReq = (await _client.GetMedReqAllForPatientAsync(medReq.PatientId)).Data;

            var previous_reports = allMedReq.Where(x => x.Status == "Completed" && x.ReportId != null)
                .Select(x => (x.Date, x.ReportId.Value)).OrderBy(x => x.Date).ToList();

            var model = new ReportIndexVM { PrevReports = previous_reports, MedReq = medReq, Report = report_response.Data, ViewMode = "View" };

            return View("Index", model);
        }

        [HttpGet("{controller}/{action}/{requestId}")]
        [Authorize(Roles = "HealthCare")]
        public async Task<IActionResult> Create(int requestId)
        {
            var medReq = (await _client.GetMedicationRequestDetailsAsync(requestId)).Data;

            var allMedReq = (await _client.GetMedReqAllForPatientAsync(medReq.PatientId)).Data;

            var previous_reports = allMedReq.Where(x => x.Status == "Completed" && x.ReportId != null)
                .Select(x => (x.Date, x.ReportId.Value)).OrderBy(x => x.Date).ToList();

            var report = new ReportDto
            {
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                SessionDate = medReq.Date,
                DialysisUnitName = User.Claims.Where(x => x.Type == "unit").FirstOrDefault()?.Value
            };

            var patientDto = (await _client.GetPatientMedicalInfoAsync(medReq.PatientId)).Data;

            report.Patient = patientDto;

            var model = new ReportIndexVM { PrevReports = previous_reports, MedReq = medReq, Report = report, ViewMode = "Create" };

            return View("Index", model);
        }

        [HttpPost("{controller}/{action}/{requestId}")]
        [Authorize(Roles = "HealthCare")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save([FromForm] ReportDto report, int requestId)
        {
            try
            {
                report.MedReqId = requestId;

                var result = await _client.CreateReportAsync(report);

                return RedirectToAction("ViewReport", new { id = result.Data.Id });
            }
            catch (ApiException<ApiResponse<ReportDto>> ex)
            {
                if (ex.Result.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
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

                    var medReq = (await _client.GetMedicationRequestDetailsAsync(requestId)).Data;
                    report.Patient = (await _client.GetPatientMedicalInfoAsync(medReq.PatientId)).Data;

                    report.CreatedDate = DateTime.Now;
                    report.LastModifiedDate = DateTime.Now;
                    report.SessionDate = medReq.Date;
                    report.DialysisUnitName = User.Claims.Where(x => x.Type == "unit").FirstOrDefault()?.Value;

                    var allMedReq = (await _client.GetMedReqAllForPatientAsync(medReq.PatientId)).Data;

                    var previous_reports = allMedReq.Where(x => x.Status == "Completed" && x.ReportId != null)
                        .Select(x => (x.Date, x.ReportId.Value)).OrderBy(x => x.Date).ToList();


                    var model = new ReportIndexVM { PrevReports = previous_reports, MedReq = medReq, Report = report, ViewMode = "Create" };

                    return View("Index", model);
                }

                throw ex;
            }
        }

        [HttpGet("Reports")]
        [Authorize(Roles = "Admin, HealthCare")]
        public IActionResult GetReportsList([FromQuery] int page = 1)
        {
            return View("List");
        }

        [HttpPost("Reports")]
        [Authorize(Roles = "Admin, HealthCare")]
        public async Task<IActionResult> GetReportsList()
        {
            var dataTableInfo = _dataTableExtractor.ExtractDataTableInformation(Request);

            var data = (await _client.GetAllReportsAsync(dataTableInfo.PageIndex, new GetReportsListQueryRequest
            {
                PageIndex = dataTableInfo.PageIndex,
                PageSize = dataTableInfo.PageSize,
                SortColumn = dataTableInfo.SortColumn,
                SortDirection = dataTableInfo.SortColumnDirection,
                SearchDict = dataTableInfo.SearchDictionary
            })).Data;

            //get total count of data in table
            int totalRecord = data.TotalCount;

            // get total count of records after search
            int filterRecord = data.FilterCount == data.TotalCount ? data.TotalCount : data.FilterCount;

            var items = data.Items.ToArray();

            var returnObj = new
            {
                draw = dataTableInfo.Draw,
                recordsTotal = totalRecord,
                recordsFiltered = filterRecord,
                data = items
            };

            return Ok(returnObj);
        }
    }
}
