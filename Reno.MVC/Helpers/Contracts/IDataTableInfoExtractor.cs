using Microsoft.AspNetCore.Http;
using Reno.MVC.Helpers.Models;

namespace Reno.MVC.Helpers.Contracts
{
    /// <summary>
    /// Extracts DataTable plugin request information
    /// </summary>
    public interface IDataTableInfoExtractor
    {
        /// <summary>
        /// Extracts DataTable information from current request
        /// </summary>
        /// <param name="request">Current request</param>
        /// <returns>DataTale plugin information</returns>
        public DataTableInfo ExtractDataTableInformation(HttpRequest request);
    }
}
