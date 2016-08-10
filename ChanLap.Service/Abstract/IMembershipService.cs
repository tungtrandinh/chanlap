using ChanLap.Model;
using System.Collections.Generic;

namespace ChanLap.Service.Abstract
{
    public interface IMembershipService
    {
        Membership ValidateUser(string email, string password);
        User CreateUser(string name, string email, string password, int[] roles);
        User GetUser(int userId);
        List<Role> GetUserRoles(string username);
    }
}
