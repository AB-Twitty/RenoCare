using Microsoft.AspNetCore.Http;
using Reno.MVC.Helpers.Contracts;
using Reno.MVC.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reno.MVC.Helpers
{
    /// <summary>
    /// Extracts DataTable plugin request information
    /// </summary>
    public class DataTableExtractor : IDataTableInfoExtractor
    {
        /// <summary>
        /// Extracts DataTable information from current request
        /// </summary>
        /// <param name="request">Current request</param>
        /// <returns>DataTale plugin information</returns>
        public DataTableInfo ExtractDataTableInformation(HttpRequest request)
        {
            var draw = request.Form["draw"].FirstOrDefault();
            var sortColumn = request.Form["columns[" + request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = request.Form["search[value]"].FirstOrDefault();
            int pageSize = Convert.ToInt32(request.Form["length"].FirstOrDefault() ?? "0");
            int skip = Convert.ToInt32(request.Form["start"].FirstOrDefault() ?? "0");

            var searchDict = new Dictionary<string, string>();

            int columnsCount = request.Form.Keys.Where(x => x.EndsWith("[data]")).Count();
            for (int index = 0; index < columnsCount; index++)
            {
                if (request.Form[$"columns[{index}][searchable]"].First() == "true")
                {
                    var key = request.Form[$"columns[{index}][name]"].FirstOrDefault() ?? string.Empty;
                    var value = request.Form[$"columns[{index}][search][value]"].FirstOrDefault() ?? string.Empty;

                    if (string.IsNullOrEmpty(value))
                        continue;

                    searchDict.Add(key, value);
                }
            }

            return new DataTableInfo
            {
                Draw = draw,
                SortColumn = sortColumn,
                SortColumnDirection = sortColumnDirection,
                SearchValue = searchValue,
                PageSize = pageSize,
                Skip = skip,
                ColumnsCount = columnsCount,
                SearchDictionary = searchDict
            };
        }
    }
}
