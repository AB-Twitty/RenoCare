using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RenoCare.Core.Conatracts.Files;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Infrastructure.Files
{
    /// <summary>
    /// Service to upload files.
    /// </summary>
    public class FileUpload : IFileUpload
    {
        #region Fields

        private readonly IWebHostEnvironment _webEnv;

        private const string API_KEY = "AIzaSyCAAYd94_zU1i1lMcfQHL3_069_hymoD-o";
        private const string BUCKET_NAME = "renocarefiles.appspot.com";
        private const string AUTH_EMAIL = "renocare@dotnet.com";
        private const string AUTH_PASS = "123@reno";

        #endregion

        #region Ctor

        public FileUpload(IWebHostEnvironment webEnv)
        {
            _webEnv = webEnv;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Upload a given file.
        /// </summary>
        /// <param name="file">the file as byte array</param>
        /// <param name="dir">the directory to upload the file</param>
        /// <returns>
        /// A task that represents the asynchronous operation,
        /// the task result contains the file path.
        /// </returns>
        public async Task<string> UploadFileAsync(IFormFile file, FileDir dir)
        {
            if (file.Length > 0)
            {
                string uploadFolder = Path.Combine(_webEnv.WebRootPath, dir.ToString());
                Directory.CreateDirectory(uploadFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                string file_path = Path.Combine(uploadFolder, uniqueFileName);

                var ms = new FileStream(file_path, FileMode.Create);
                await file.CopyToAsync(ms);
                ms.Close();

                ms = new FileStream(file_path, FileMode.Open);

                var auth = new FirebaseAuthProvider(new FirebaseConfig(API_KEY));
                var a = await auth.SignInWithEmailAndPasswordAsync(AUTH_EMAIL, AUTH_PASS);

                // you can use CancellationTokenSource to cancel the upload midway
                var cancellation = new CancellationTokenSource();

                var task = new FirebaseStorage(
                    BUCKET_NAME,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
                    })
                    .Child(dir.ToString())
                    .Child(uniqueFileName)
                    .PutAsync(ms, cancellation.Token);

                task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

                return await task;
            }
            else
            {
                throw new Exception("No file is provided.");
            }
        }

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
        public async Task<string> UploadFileAsync(byte[] bytes, string fileName, FileDir dir)
        {
            try
            {
                string uploadFolder = Path.Combine(_webEnv.WebRootPath, dir.ToString());
                Directory.CreateDirectory(uploadFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "-" + fileName;
                string file_path = Path.Combine(uploadFolder, uniqueFileName);
                await File.WriteAllBytesAsync(file_path, bytes);

                var ms = new FileStream(file_path, FileMode.Open);

                var auth = new FirebaseAuthProvider(new FirebaseConfig(API_KEY));
                var a = await auth.SignInWithEmailAndPasswordAsync(AUTH_EMAIL, AUTH_PASS);

                // you can use CancellationTokenSource to cancel the upload midway
                var cancellation = new CancellationTokenSource();

                var task = new FirebaseStorage(
                    BUCKET_NAME,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
                    })
                    .Child(dir.ToString())
                    .Child(uniqueFileName)
                    .PutAsync(ms, cancellation.Token);

                task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

                return await task;
            }
            catch (Exception ex)
            {
                throw new Exception("No file is provided.");
            }
        }

        #endregion
    }
}
