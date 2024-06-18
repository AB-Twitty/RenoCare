using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace RenoCare.Core.Conatracts.Files
{
    /// <summary>
    /// Service to upload files.
    /// </summary>
    public interface IFileUpload
    {
        /// <summary>
        /// Upload a given file.
        /// </summary>
        /// <param name="file">the file to upload</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains the file path.
        /// </returns>
        public Task<string> UploadFileAsync(IFormFile file);
    }
}
