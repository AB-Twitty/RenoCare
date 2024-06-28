using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Reno.MVC.Helpers.Contracts;
using Reno.MVC.Models.DialysisUnit;
using Reno.MVC.Services.Base;
using System.Linq;
using System.Threading.Tasks;

namespace Reno.MVC.Controllers
{
    [Authorize(Roles = "Admin,HealthCare")]
    public class PatientsController : Controller
    {
        private readonly IClient _client;
        private readonly IDataTableInfoExtractor _dataTableExtractor;

        public PatientsController(IClient client, IDataTableInfoExtractor dataTableExtractor)
        {
            _client = client;
            _dataTableExtractor = dataTableExtractor;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int page = 1)
        {
            ViewBag.DiabetesSelectList = new SelectList((await _client.DiabetesTypesAsync(null)).Data.OrderBy(x => x.Id), "Name", "Name");

            ViewBag.HypertensionSelectList = new SelectList((await _client.HypertensionTypesAsync(null)).Data.OrderBy(x => x.Id), "Name", "Name");

            ViewBag.SmokingSelectList = new SelectList((await _client.SmokingStatusListAsync(null)).Data.OrderBy(x => x.Id), "Name", "Name");

            var medStatuses = (await _client.MedicationRequestStatusListAsync(null)).Data;

            var model = new AllMedStatusModel
            {
                MedReqStatus = medStatuses.OrderBy(x => x.Id).ToList(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var dataTableInfo = _dataTableExtractor.ExtractDataTableInformation(Request);

            var data = (await _client.PatientsAsync(dataTableInfo.PageIndex, new GetPatientListQueryRequest
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

