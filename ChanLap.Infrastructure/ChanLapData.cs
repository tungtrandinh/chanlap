using System;
using System.Data.Entity;

namespace ChanLap.Infrastructure
{
    public class ChanLapData : DropCreateDatabaseIfModelChanges<ChanLapEntity>
    {
        protected override void Seed(ChanLapEntity context)
        {
            context.Commit();
        }
    }
}
