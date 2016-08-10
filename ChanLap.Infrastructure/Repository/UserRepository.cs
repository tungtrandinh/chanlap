using ChanLap.Infrastructure;
using ChanLap.Model;
using System;
using System.Linq.Expressions;

namespace ChanLap.Infrastructure.Repository
{
    public class UserRepository: RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {
            }
    }

    public interface IUserRepository : IRepository<User>
    {
        
    }
}