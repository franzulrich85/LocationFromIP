using System.Runtime.Serialization;

namespace LocationFromIP.Api.Enums
{
    public enum ApiFailureCodes
    {
        [EnumMember(Value = "400 Bad Request")]
        BadRequest,
        [EnumMember(Value = "404 Not Found")]
        NotFound,
        [EnumMember(Value = "429 Too Many Requests")]
        TooManyRequests,
        [EnumMember(Value = "500 Internal Server Error")]
        InternalServerError,
        [EnumMember(Value = "503 Service Unavailable")]
        ServiceUnavailable
    }
}
