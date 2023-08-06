using LocationFromIP.Application.Enums;
using LocationFromIP.Application.Exceptions;
using LocationFromIP.Application.Interfaces.Geoapify;
using LocationFromIP.Application.Models.Geoapify;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace LocationFromIP.Infrastructure.Geoapify
{
    public class GeoapifyClient : IGeoapifyClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<GeoapifyClient> _logger;

        public GeoapifyClient(HttpClient httpClient, ILogger<GeoapifyClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IpLookupResponse> GetLocation(string ipAddress)
        {
            var response = await _httpClient.GetAsync($"json/{ipAddress}");

            //SUCCESS
            //Status Code: 200
            //Body: status (property) = "success"

            //FAILED 
            //Status Code: 200
            //Body: status (property) = "fail"

            if (response.StatusCode == HttpStatusCode.TooManyRequests)
                throw new TooManyRequestsException($"Too many requests made in {nameof(GeoapifyClient)}");

            var rawData = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("{ServiceName} returned {StatusCode} with {Response} for IPv4 {IpAddress}", nameof(GeoapifyClient), response.StatusCode, rawData, ipAddress);
                throw new ApplicationException($"Response was not successful. Status: '{response.StatusCode}'. IPv4: '{ipAddress}'. Response : '{rawData}'");
            }
                
            var jsonData = JsonConvert.DeserializeObject<IpLookupResponse>(rawData);

            if (jsonData?.Status != "success")
                throw new BadRequestException(
                    $"Request not successful. Message: '{jsonData?.Message}'",
                    new ValidationError[] { new ValidationError(ValidationErrorCode.InvalidQuery, jsonData?.Message) });

            return jsonData;
        }
    }
}
