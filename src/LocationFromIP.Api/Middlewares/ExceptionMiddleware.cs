using LocationFromIP.Api.Enums;
using LocationFromIP.Api.Models;
using LocationFromIP.Application.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace LocationFromIP.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            var errorResponse = new ErrorResponse {Code = ApiFailureCodes.InternalServerError };

            switch (ex)
            {
                case BadRequestException:
                    var exception = (BadRequestException)ex;
                    statusCode = HttpStatusCode.BadRequest;
                    errorResponse = new ErrorResponse
                    {
                        Code = ApiFailureCodes.BadRequest,
                        Message = ExceptionMessages.RequestInvalid,
                        Errors = exception.ValidationErrors
                    };
                    break;
                case TooManyRequestsException:
                    statusCode = HttpStatusCode.TooManyRequests;
                    errorResponse = new ErrorResponse
                    {
                        Code = ApiFailureCodes.TooManyRequests,
                        Message = ex.Message
                    };
                    break;
                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    errorResponse = new ErrorResponse
                    {
                        Code = ApiFailureCodes.NotFound,
                        Message = ExceptionMessages.RequestInvalid,
                    };
                    break;
                default:
                    errorResponse.Message = ex.Message;
                    break;
            }

            _logger.LogError(JsonConvert.SerializeObject(errorResponse));
            httpContext.Response.StatusCode = (int)statusCode;
            httpContext.Response.ContentType = "application/json";
            var settings = new JsonSerializerSettings 
            {
                NullValueHandling = NullValueHandling.Ignore,
                Converters = { new StringEnumConverter() },
                ContractResolver = new CamelCasePropertyNamesContractResolver()

            };
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse, settings));

        }
    }
}
