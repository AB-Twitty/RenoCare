using System.Collections.Generic;

namespace Reno.MVC.Helpers.Models
{
    /// <summary>
    /// Represents dataTable plugin request information
    /// </summary>
    public class DataTableInfo
    {
        public string Draw { get; set; }
        public string SortColumn { get; set; }
        public string SortColumnDirection { get; set; }
        public string SearchValue { get; set; }
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public int PageIndex => Skip / PageSize + 1;
        public int ColumnsCount { get; set; }
        public IDictionary<string, string> SearchDictionary { get; set; }
    }
}
