namespace RenoCare.Infrastructure.Authentication
{
    /// <summary>
    /// Represents Jwt token creational options.
    /// </summary>
    public static class JwtSettings
    {
        /// <summary>
        /// Represents jwt token signing credentials secret key.
        /// </summary>
        public static string Key = "0d53e461fa774946a7fe8b7ea8235d8d";
        /// <summary>
        /// Represents a value indicating the issuer of the token.
        /// </summary>
        public static string Issuer = "RenoCare";
        /// <summary>
        /// Represents a value indicating the audience of the token.
        /// </summary>
        public static string Audience = "RenoCare";
        /// <summary>
        /// Represents a value indicating the duration in minutes for the life time of the token.
        /// </summary>
        public static int ExpiredAfterMinutes = 10080;
    }
}
