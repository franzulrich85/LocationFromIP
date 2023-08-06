using LocationFromIP.Application.Entities;

namespace LocationFromIP.Application.Interfaces.Services
{
    public interface ILocationService
    {
        Task<IpLocation> Get(string ipV4Address);
    }
}
