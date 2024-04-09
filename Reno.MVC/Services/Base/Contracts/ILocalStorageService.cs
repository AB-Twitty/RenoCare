using System.Collections.Generic;

namespace Reno.MVC.Services.Base.Contracts
{
    /// <summary>
    /// Responsible for local storage data exchange
    /// </summary>
    public interface ILocalStorageService
    {
        /// <summary>
        /// Sets a data value for a specific key in local storage.
        /// </summary>
        /// <typeparam name="T">Type of data</typeparam>
        /// <param name="key">Local storage key</param>
        /// <param name="value">Data Value</param>
        public void SetStorageValue<T>(string key, T value);

        /// <summary>
        /// Gets a data value for a specific key from local storage.
        /// </summary>
        /// <typeparam name="T">Type of data</typeparam>
        /// <param name="key">Local storage key</param>
        /// <returns>Data value</returns>
        public T GetStorageValue<T>(string key);

        /// <summary>
        /// Checks whether a specific key exists in local storage.
        /// </summary>
        /// <param name="key">Local storage key</param>
        /// <returns>A vaalue indicating whether a specific key exists in local storage.</returns>
        public bool Exists(string key);

        /// <summary>
        /// Clear values for the corresponding keys from loal storage.
        /// </summary>
        /// <param name="keys">A list of local storage keys</param>
        public void ClearStorage(IList<string> keys);

    }
}
