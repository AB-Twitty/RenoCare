using RenoCare.Domain.Common;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RenoCare.Domain
{
    public class Virus : BaseEntity
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<Patient> Patients { get; set; }
        [JsonIgnore]
        public virtual ICollection<DialysisUnit> DialysisUnits { get; set; }
    }
}
