using LocationFromIP.Api.Helpers;
using LocationFromIP.Api.Models;
using LocationFromIP.Application.Entities;
using LocationFromIP.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace LocationFromIP.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILogger<LocationController> _logger;
        private readonly ILocationService _locationService;

        public LocationController(ILogger<LocationController> logger, ILocationService locationService)
        {
            _logger = logger;
            _locationService = locationService;
        }

        [HttpGet("{ipV4Address}")]
        [SwaggerOperation(
            Summary = "Location from IP Address",
            Description = "Retrieve the location for IPv4",
            OperationId = "GetLocationFromIP",
            Tags = new[] { "Location for IP"}
            )]
        [SwaggerResponse((int)HttpStatusCode.OK, "GET", typeof(DataResponse<IpLocation>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Validation Issue", typeof(ErrorResponse))]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.TooManyRequests)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        [Produces("application/json")]
        public async Task<ActionResult<DataResponse<IpLocation>>> Get(string ipV4Address)
        {
            var locationData = await _locationService.Get(ipV4Address);
            return Ok(ResponseHelper.Create(MapIpLocationResponse(locationData)));
        }

        private static IpLocationResponse MapIpLocationResponse(IpLocation data) => new()
        {
            Country = data.Country,
            CountryCode = data.CountryCode,
            Region = data.Region,
            RegionName = data.RegionName,
            City = data.City,
            Zip = data.Zip,
            Latitude = data.Latitude,
            Longitude = data.Longitude,
        };
    }
}