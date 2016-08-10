using ChanLap.Model;

namespace ChanLap.Infrastructure.Repository
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {
        }
    }

    public interface IRoleRepository : IRepository<Role>
    { 
        
    }
}
