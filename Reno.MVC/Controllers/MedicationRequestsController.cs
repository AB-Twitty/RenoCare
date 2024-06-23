using Microsoft.AspNetCore.Mvc;
using Reno.MVC.Helpers.Contracts;
using Reno.MVC.Services.Base;
using System.Linq;
using System.Threading.Tasks;

namespace Reno.MVC.Controllers
{
    public class MedicationRequestsController : Controller
    {
        private readonly IDataTableInfoExtractor _dataTableExtractor;
        private readonly IClient _client;

        public MedicationRequestsController(IDataTableInfoExtractor dataTableExtractor, IClient client)
        {
            _dataTableExtractor = dataTableExtractor;
            _client = client;
        }

        [HttpGet]
        public IActionResult Index([FromQuery] int page = 1)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var dataTableInfo = _dataTableExtractor.ExtractDataTableInformation(Request);

            var data = (await _client.MedicationRequestsAsync(dataTableInfo.PageIndex, new GetMedicationRequestsQueryRequest
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
