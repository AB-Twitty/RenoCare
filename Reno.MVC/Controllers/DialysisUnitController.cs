using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reno.MVC.Helpers.Contracts;
using Reno.MVC.Models.DialysisUnit;
using Reno.MVC.Services.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Reno.MVC.Controllers
{
    public class DialysisUnitController : Controller
    {
        private readonly IClient _client;
        private readonly IDataTableInfoExtractor _dataTableExtractor;

        public DialysisUnitController(IClient client, IDataTableInfoExtractor dataTableExtractor)
        {
            _client = client;
            _dataTableExtractor = dataTableExtractor;
        }

        [HttpGet("DialysisUnit/Newcome")]
        public IActionResult NewcomeDialysisUnitAsync(string email, string id)
        {
            return View("Newcome_Unit", new DialysisUnitIndexModel
            {
                Email = email,
                UserId = id,
                UnitSpec = new UnitSpecificationsDto(),
                Sessions = new List<SessionTimeModel>(),
            });
        }

        [HttpPost("DialysisUnit/Newcome")]
        public async Task<IActionResult> NewcomeDialysisUnitAsync(DialysisUnitIndexModel unit_model)
        {
            try
            {
                var sessions = unit_model.Sessions?.Where(x => !x.Deleted).Select(x => new SessionTimeDto
                {
                    Day = x.Day,
                    Time = DateTime.Today.Add(x.Time),
                }).ToList();


                var imgs = new List<ImageUploadDto>();

                if (unit_model.Images != null)
                {
                    foreach (var img in unit_model.Images)
                    {
                        var ms = new MemoryStream();
                        await img.CopyToAsync(ms);

                        imgs.Add(new ImageUploadDto
                        {
                            FileName = img.FileName,
                            Bytes = ms.ToArray()
                        });

                        ms.Close();
                    }
                }

                var amenities = new List<int>();
                if (unit_model.Amenities != null)
                {
                    foreach (var idx in unit_model.Amenities.Split(','))
                    {
                        int.TryParse(idx, out int i);
                        amenities.Add(i);
                    }
                }

                var viruses = new List<int>();
                if (unit_model.Viruses != null)
                {
                    foreach (var idx in unit_model.Viruses.Split(','))
                    {
                        int.TryParse(idx, out int i);
                        viruses.Add(i);
                    }
                }


                var unit_newcome = new DialysisUnitNewcome
                {
                    Email = unit_model.Email,
                    FirstName = unit_model.FirstName,
                    LastName = unit_model.LastName,
                    Phone = unit_model.Phone,
                    UnitSpecificationsDto = unit_model.UnitSpec,
                    Sessions = sessions,
                    Images = imgs,
                    ThumbnailIdx = unit_model.Thumbnail,
                    Amenities = amenities,
                    Viruses = viruses,
                };

                var result = await _client.CreateDialysisUnitNewcomeAsync(unit_newcome);

                unit_model.Sessions = unit_model.Sessions?.Where(x => !x.Deleted)
                    .OrderBy(x => x.Time).ToList() ?? new List<SessionTimeModel>();

                return View("Newcome_Unit", unit_model);
            }
            catch (ApiException<ApiResponse<string>> ex)
            {
                var errors = ex.Result.Errors;
                if (errors?.Any() ?? false)
                {
                    foreach (var error in errors)
                    {
                        var key = error.Split(" : ")[0];
                        var msg = error.Split(" : ")[1];

                        key = key.Replace("UnitSpecificationsDto", "UnitSpec");

                        if (key == "Sessions")
                        {
                            var error_msg = msg.Split('#')[0];
                            var days = msg.Split('#')[1];

                            foreach (var day in days.Split(","))
                            {
                                ModelState.AddModelError(day, error_msg);
                            }
                        }

                        ModelState.AddModelError(key, msg);
                    }
                }

                unit_model.Sessions = unit_model.Sessions?.Where(x => !x.Deleted)
                    .OrderBy(x => x.Time).ToList() ?? new List<SessionTimeModel>();

                return View("Newcome_Unit", unit_model);
            }
        }

        [HttpGet("DialysisUnit/Details/{id}")]
        [Authorize(Roles = "Admin, HealthCare")]
        public async Task<IActionResult> GetDialysisUnitDetailsAsync(int id)
        {
            var reuslt = await _client.GetDialysisUnitDetailsAsync(id);

            return View("Details", reuslt.Data);
        }

        [HttpGet("Dialysis/Units")]
        [Authorize(Roles = "Admin, HealthCare")]
        public async Task<IActionResult> GetDialysisUnitsAsync([FromQuery] int page = 1)
        {
            var medStatuses = (await _client.MedicationRequestStatusListAsync(null)).Data;

            var model = new AllMedStatusModel
            {
                MedReqStatus = medStatuses.OrderBy(x => x.Id).ToList(),
            };

            return View("List", model);
        }

        [HttpPost("Dialysis/Units")]
        [Authorize(Roles = "Admin, HealthCare")]
        public async Task<IActionResult> GetDialysisUnitsAsync()
        {
            var dataTableInfo = _dataTableExtractor.ExtractDataTableInformation(Request);

            var data = (await _client.GetDialysisUnitListAsync(dataTableInfo.PageIndex, new GetDialysisUnitsListQueryRequest
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
