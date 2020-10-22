namespace Ticketmanagement.BusinessLogic
{
    internal static class ValidationErrorMessage
    {
        /// <summary>
        /// Error message for descrition, when it is null, empty or contains only white space.
        /// </summary>
        public const string DescriptionNullError = "Description can't be null or contains only white spaces.";

        /// <summary>
        /// Error message for address, when it is null, empty or contains only white space.
        /// </summary>
        public const string AddressNullError = "Address can't be null or contains only white spaces.";

        /// <summary>
        /// Error message for name, when it is null, empty or contains only white space.
        /// </summary>
        public const string NameNullError = "Name can't be null or contains only white spaces.";

        /// <summary>
        /// Error message for row, when it is less or equal to 0.
        /// </summary>
        public const string NotPossitiveRowError = "Row should be more than 0.";

        /// <summary>
        /// Error message for number, when it is less or equal to 0.
        /// </summary>
        public const string NotPossitiveNumberError = "Number should be more than 0.";

        /// <summary>
        /// Error message for phone, when phone don't consist of only 12 digits or emty.
        /// </summary>
        public const string IncorrectPhoneError = "Phone should be contains 12 digits(f.e. 123456789012) or empty";
    }
}
