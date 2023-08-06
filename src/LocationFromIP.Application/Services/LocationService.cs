using LocationFromIP.Application.Entities;
using LocationFromIP.Application.Exceptions;
using LocationFromIP.Application.Helpers;
using LocationFromIP.Application.Interfaces.Geoapify;
using LocationFromIP.Application.Interfaces.Persistence;
using LocationFromIP.Application.Interfaces.Services;
using LocationFromIP.Application.Models.Geoapify;

namespace LocationFromIP.Application.Services
{
    public class LocationService : ILocationService
    {
        private readonly ICacheService _cacheService;
        private readonly IIpLocationRepository _ipLocationRepository;
        private readonly IGeoapifyClient _geoapifyClient;

        public LocationService(
            ICacheService cacheService,
            IIpLocationRepository ipLocationRepository,
            IGeoapifyClient geoapifyClient
            ) 
        {
            _cacheService = cacheService;
            _ipLocationRepository = ipLocationRepository;
            _geoapifyClient = geoapifyClient;
        }

        public async Task<IpLocation> Get(string ipV4Address)
        {
            ValidateRequest(ipV4Address);

            //LOOK UP LOCATION IN CACHE
            if (_cacheService.TryGet(ipV4Address, out IpLocation cachedIpLocation))
                return cachedIpLocation;

            //GET LOCATION FROM 3RD PARTY
            var ipLocationResponse = await _geoapifyClient.GetLocation(ipV4Address);

            //SAVE TO DB
            var dbSave = await _ipLocationRepository.AddAsync(MapIpLocationEntity(ipLocationResponse));

            //PUSH TO CACHE AND RETURN DATA
            return _cacheService.Set(ipV4Address, dbSave);
        }

        private static void ValidateRequest(string ipV4Address)
        {
            if (!IpV4AddressHelper.IsValidateIp(ipV4Address))
                throw new BadRequestException($"IPv4 address '{ipV4Address}' not valid", new ValidationError[]
                {
                    new ValidationError(Enums.ValidationErrorCode.FieldValueInvalid, $"IPv4 address '{ipV4Address}' not valid")
                });
        }

        private static IpLocation MapIpLocationEntity(IpLookupResponse data) => new()
        {
            Country = data.Country, 
            CountryCode = data.CountryCode,
            Region = data.Region,
            RegionName = data.RegionName,
            City = data.City,
            Zip = data.Zip,
            Latitude = data.Lat,
            Longitude = data.Lon,
            Timezone = data.Timezone,
            IspProvider = data.Isp,
            As = data.As
        };
    }
}
