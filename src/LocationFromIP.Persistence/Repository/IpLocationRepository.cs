using LocationFromIP.Application.Entities;
using LocationFromIP.Application.Interfaces.Persistence;
using LocationFromIP.Persistence.DatabaseContext;

namespace LocationFromIP.Persistence.Repository
{
    public class IpLocationRepository : GenericRepository<IpLocation>, IIpLocationRepository
    {
        public IpLocationRepository(IpAddressDatabaseContext dbContext) : base(dbContext)
        {}
    }
}