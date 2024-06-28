namespace RenoCare.Core.Features.DialysisUnits.Dtos
{
    public class UnitSpecificationsDto
    {
        /// <summary>
        /// Gets or sets the dialysis unit name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a description for the dialysis unit name
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the dialysis unit
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the address of the dialysis unit
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the country of the dialysis unit
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the city of the dialysis unit
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether HD treatment is supported
        /// </summary>
        public bool IsHdSupported { get; set; }

        /// <summary>
        /// Gets or sets the price of HD treatment session
        /// </summary>
        public double? HdPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether HDF treatment is supported
        /// </summary>
        public bool IsHdfSupported { get; set; }

        /// <summary>
        /// Gets or sets the price of HDF treatment session
        /// </summary>
        public double? HdfPrice { get; set; }
    }
}
