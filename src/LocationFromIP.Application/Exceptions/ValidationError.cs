using LocationFromIP.Application.Enums;

namespace LocationFromIP.Application.Exceptions
{
    public class ValidationError
    {
        public ValidationErrorCode ErrorCode { get; set; }
        public string Message { get; set; }

        public ValidationError(ValidationErrorCode errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }
    }
}
