namespace RenoCare.Domain.MetaData
{
    /// <summary>
    /// Represents end points routing.
    /// </summary>
    public static class Router
    {
        public const string Root = "Api";
        public const string Version = "/V1";
        public const string Rule = Root + Version;

        /// <summary>
        /// Represents account controller end points routing.
        /// </summary>
        public static class AccountRouting
        {
            public const string Login = Rule + "/Login";
            public const string SendEmailConfirmation = Rule + "/EmailConfirmation/{userId}";
            public const string ConfirmEmail = Rule + "/ConfirmEmail/{userId}/";
            public const string SendResetPasswordEmail = Rule + "/ResetPassword";
            public const string ResetPassword = Rule + "/ResetPassword/{userId}/";
        }
    }
}
