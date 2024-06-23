using System.Collections.Generic;

namespace RenoCare.Core.Helpers.Contracts
{
    /// <summary>
    /// Represents a searchable request
    /// </summary>
    public interface ISearchable
    {
        public IDictionary<string, string> SearchDict { get; set; }
    }
}
