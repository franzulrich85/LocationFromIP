using LocationFromIP.Application.Models.Geoapify;

namespace LocationFromIP.Application.Interfaces.Geoapify
{
    public interface IGeoapifyClient
    {
        Task<IpLookupResponse> GetLocation(string ipAddress);
    }
}
