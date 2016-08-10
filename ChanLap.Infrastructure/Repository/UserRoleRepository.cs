using ChanLap.Model;

namespace ChanLap.Infrastructure.Repository
{
    public class UserRoleRepository : RepositoryBase<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {
        }
    }

    public interface IUserRoleRepository : IRepository<UserRole>
    { 
        
    }
}
