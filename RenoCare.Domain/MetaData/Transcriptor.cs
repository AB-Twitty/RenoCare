namespace RenoCare.Domain.MetaData
{
    /// <summary>
    /// Represents meta messages within the application.
    /// </summary>
    public static class Transcriptor
    {
        /// <summary>
        /// Represents validation messages.
        /// </summary>
        public static class Validations
        {
            public const string NotNull = "'{PropertyName}' is required.";
            public const string NotEmpty = "'{PropertyName}' can't be empty.";
            public const string AlreadyExist = "'{PropertyValue}' already exists.";
        }

        /// <summary>
        /// Represents authentication and identity messages.
        /// </summary>
        public static class Identity
        {
            public const string InvalidAuthentication = "Invalid login attempt.";
            public const string UserNotFound = "Unable to load user with '{0}'.";
            public const string ConfirmationEmailSent = "Verification email sent. Please check your email.";
            public const string EmailConfirmedSuccess = "Thank you for confirming your email.";
            public const string EmailSendingFailure = "Error sending the email.";
            public const string ResetPasswordEmailSent = "Reset password email sent. Please check your email.";
        }

        /// <summary>
        /// Represents response messages.
        /// </summary>
        public static class Response
        {
            public const string EntityNotFound = "Entity with id '{0}' not found.";
        }

    }
}
