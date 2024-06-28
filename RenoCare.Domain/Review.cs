using RenoCare.Domain.Common;
using System;

namespace RenoCare.Domain
{
    public class Review : BaseEntity
    {
        public int PatientId { get; set; }
        public int DialysisUnitId { get; set; }
        public string Comment { get; set; }
        public double Rating { get; set; }
        public DateTime CreationDate { get; set; }



        public virtual Patient Patient { get; set; }
        public virtual DialysisUnit DialysisUnit { get; set; }
    }
}
