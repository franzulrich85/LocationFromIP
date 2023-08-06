namespace LocationFromIP.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public ValidationError[] ValidationErrors { get; set; }

        public BadRequestException(string message, ValidationError[] errors) : base(message) {
            ValidationErrors = errors;
        }
    }
}
