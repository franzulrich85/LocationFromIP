using LocationFromIP.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace LocationFromIP.Persistence.DatabaseContext
{
    public class IpAddressDatabaseContext : DbContext
    {
        public IpAddressDatabaseContext(DbContextOptions<IpAddressDatabaseContext> options) : base(options) 
        {}

        public DbSet<IpLocation> IpLocations { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.DateModified = DateTime.Now;
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity.Id == default(Guid))
                        entry.Entity.Id = Guid.NewGuid();

                    entry.Entity.DateCreated = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
