namespace RenoCare.Core.Conatracts.Mail
{
    /// <summary>
    /// Represents an email properties.
    /// </summary>
    public class Email
    {
        /// <summary>
        /// Represents the name of the receiver.
        /// </summary>
        public string ToName { get; set; }
        /// <summary>
        /// Represents the email address of the receiver.
        /// </summary>
        public string ToAddress { get; set; }
        /// <summary>
        /// Represents the email subject.
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Represents the email body.
        /// </summary>
        public string Body { get; set; }
    }
}
