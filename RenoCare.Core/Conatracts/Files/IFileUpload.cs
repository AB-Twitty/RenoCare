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
        /// <param name="dir">The directory to upload the file</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains the file path.
        /// </returns>
        public Task<string> UploadFileAsync(IFormFile file, FileDir dir);


        /// <summary>
        /// Upload a given file.
        /// </summary>
        /// <param name="bytes">the file to upload as byte array</param>
        /// <param name="fileName">the name of the uploaded file</param>
        /// <param name="dir">The directory to upload the file</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains the file path.
        /// </returns>
        public Task<string> UploadFileAsync(byte[] bytes, string fileName, FileDir dir);
    }

    public enum FileDir
    {
        Uploads,
        Images
    }
}
