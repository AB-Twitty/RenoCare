namespace RenoCare.Core.Conatracts.Mail
{
    /// <summary>
    /// Represents Smtp credintials email settings.
    /// </summary>
    public class EmailSettings
    {
        public string SmtpHost { get; set; }
        public string SmtpPort { get; set; }
        public SmtpCredentials SmtpCredentials { get; set; }
    }

    public class SmtpCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
