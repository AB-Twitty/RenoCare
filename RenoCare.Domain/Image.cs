using RenoCare.Domain.Common;

namespace RenoCare.Domain
{
    /// <summary>
    /// Represents dialysis unit images entity
    /// </summary>
    public class Image : BaseEntity
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public int DialysisUnitId { get; set; }
        public bool IsThumbnail { get; set; }
    }
}
