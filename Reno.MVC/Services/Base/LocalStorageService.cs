using Hanssens.Net;
using Microsoft.AspNetCore.Http;
using Reno.MVC.Services.Base.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Reno.MVC.Services.Base
{
    /// <summary>
    /// Responsible for local storage data exchange
    /// </summary>
    public class LocalStorageService : ILocalStorageService
    {
        #region Fields

        private readonly LocalStorage _localStorage;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Ctor

        public LocalStorageService(IHttpContextAccessor httpContextAccessor)
        {
            var config = new LocalStorageConfiguration
            {
                AutoLoad = true,
                AutoSave = true,
                Filename = "RenoCare"
            };

            _localStorage = new LocalStorage(config);
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Methods 

        /// <summary>
        /// Sets a data value for a specific key in local storage.
        /// </summary>
        /// <typeparam name="T">Type of data</typeparam>
        /// <param name="key">Local storage key</param>
        /// <param name="value">Data Value</param>
        public void SetStorageValue<T>(string key, T value)
        {
            _localStorage.Store(key, value);
            _localStorage.Persist();
        }

        /// <summary>
        /// Gets a data value for a specific key from local storage.
        /// </summary>
        /// <typeparam name="T">Type of data</typeparam>
        /// <param name="key">Local storage key</param>
        /// <returns>Data value</returns>
        public T GetStorageValue<T>(string key)
        {
            return _localStorage.Get<T>(key);
        }

        /// <summary>
        /// Checks whether a specific key exists in local storage.
        /// </summary>
        /// <param name="key">Local storage key</param>
        /// <returns>A vaalue indicating whether a specific key exists in local storage.</returns>
        public bool Exists(string key)
        {
            return _localStorage.Exists(key);
        }

        /// <summary>
        /// Clear values for the corresponding keys from loal storage.
        /// </summary>
        /// <param name="keys">A list of local storage keys</param>
        public void ClearStorage(IList<string> keys)
        {
            foreach (string key in keys)
            {
                _localStorage.Remove(key);
            }
        }


        /// <summary>
        /// Clear the token for the current user from loal storage.
        /// </summary>
        public void ClearCurrentToken()
        {
            var userId = _httpContextAccessor.HttpContext
                .User.Claims.Where(c => c.Type == "sub").FirstOrDefault()?.Value;
            if (userId != null)
                _localStorage.Remove($"{userId}_Token");
        }


        /// <summary>
        /// get the token for the current user from loal storage.
        /// </summary>
        public string GetCurrentToken()
        {
            var userId = _httpContextAccessor.HttpContext
                .User.Claims.Where(c => c.Type == "sub").FirstOrDefault()?.Value;

            if (userId != null && _localStorage.Exists($"{userId}_Token"))
                return _localStorage.Get<string>($"{userId}_Token");

            return "";
        }


        #endregion
    }
}
