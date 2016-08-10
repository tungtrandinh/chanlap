using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChanLap.Infrastructure;
using ChanLap.Model;
using ChanLap.Infrastructure.Repository;

namespace ChanLap.Service
{
    public interface IUserService {
        User GetByEmail(string email);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;

        public UserService(IUserRepository _repository)
        {
            this.repository = _repository;
        }

        public User GetByEmail(string email)
        {
            return this.repository.Get(t=>t.Email == email);
        }
    }
}
