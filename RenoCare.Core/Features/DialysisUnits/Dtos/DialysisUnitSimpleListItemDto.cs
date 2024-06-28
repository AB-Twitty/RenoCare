namespace RenoCare.Core.Features.DialysisUnits.Dtos
{
    public class DialysisUnitSimpleListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ContactNumber { get; set; }
        public double Rating { get; set; }
        public int ReviewsCnt { get; set; }
        public bool IsHdSupported { get; set; }
        public double? HdPrice { get; set; }
        public bool IsHdfSupported { get; set; }
        public double? HdfPrice { get; set; }
        public string ThumbnailImage { get; set; }
    }
}
