using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using ChanLap.Model;

namespace ChanLap.Infrastructure
{
    public class ChanLapEntity : DbContext
    {
        public DbSet<User> User { get; set; }
        public ChanLapEntity() : base() {
            Database.SetInitializer<ChanLapEntity>(null);
            this.Configuration.LazyLoadingEnabled       = true;
            this.Configuration.ProxyCreationEnabled     = true;
            this.Configuration.AutoDetectChangesEnabled = true;
        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }
    }
}
