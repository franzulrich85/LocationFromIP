using LocationFromIP.Api.Enums;
using LocationFromIP.Application.Exceptions;

namespace LocationFromIP.Api.Models
{
    public class ErrorResponse
    {
        public ApiFailureCodes Code { get; set; }
        public string? Message { get; set; }
        public ValidationError[]? Errors { get; set; }
    }
}
