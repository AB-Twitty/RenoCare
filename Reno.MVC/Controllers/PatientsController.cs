using Microsoft.AspNetCore.Mvc;
using Reno.MVC.Services.Base;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Reno.MVC.Controllers
{
	public class PatientsController : Controller
	{
		private readonly IClient _client;

		public PatientsController(IClient client)
		{
			_client = client;
		}

		[HttpGet]
		public async Task<IActionResult> Index([FromQuery] int page = 1)
		{
			var list = await _client.PatientsAsync(page, new GetPatientListQueryRequest
			{
				PageIndex = page,
				PageSize = 20
			});

			return View(list);
		}

		[HttpPost]
		public async Task<IActionResult> Index()
		{
			int totalRecord = 0;
			int filterRecord = 0;
			var draw = Request.Form["draw"].FirstOrDefault();
			var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
			var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
			var searchValue = Request.Form["search[value]"].FirstOrDefault();
			int pageSize = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");
			int skip = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");

			int page = skip / pageSize + 1;
			var data = (await _client.PatientsAsync(page, new GetPatientListQueryRequest
			{
				PageIndex = page,
				PageSize = pageSize
			})).Data;

			//get total count of data in table
			totalRecord = data.TotalCount;
			// search data when search value found
			if (!string.IsNullOrEmpty(searchValue))
			{
				data.Items = data.Items.Where(x => x.FirstName.ToLower().Contains(searchValue.ToLower()) || x.LastName.ToLower().Contains(searchValue.ToLower()));
			}
			// get total count of records after search
			filterRecord = data.Items.Count();
			//sort data
			//if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection)) data.Items = data.Items.OrderBy(sortColumn + " " + sortColumnDirection);
			//pagination

			var items = data.Items.Select(x => new
			{
				PatientName = x.FirstName + " " + x.LastName,
				ReportsSameUnit = x.ReportsCount,
				ReportsOverral = x.ReportsCount,
				x.Gender,
				x.Age,
				x.Diabetes,
				x.Hypertension,
				Smoking = "Non-Smoker"
			}).ToArray();


			var returnObj = new
			{
				draw = draw,
				recordsTotal = totalRecord,
				recordsFiltered = totalRecord,
				data = items
			};

			return Ok(returnObj);
		}
	}

}

