using ChanLap.Service.Abstract;
using ChanLap.Model;
using System;
using System.Collections.Generic;
using System.Linq;


using System.Security.Principal;
using ChanLap.Infrastructure.Repository;

namespace ChanLap.Service
{
    public class MembershipService : IMembershipService
    {
        private readonly IEncryptionService _encryptionService;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;

        public MembershipService(IEncryptionService encryptionService, IUserRepository userRepository, IRoleRepository roleRepository, IUserRoleRepository userRoleRepository)
        {
            _encryptionService = encryptionService;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }
        public User CreateUser(string name, string email, string password, int[] roles)
        {
            var existingUser = _userRepository.Get(t => t.Email == email);
            
            if (existingUser != null)
            {
                throw new Exception("Username is already in use");
            }

            var passwordSalt = _encryptionService.CreateSalt();

            var user = new User()
            {
                Name = name,
                Email = email,
                IsLocked = false,
                Salt = passwordSalt,
                Password = _encryptionService.EncryptPassword(password, passwordSalt)
            };

            _userRepository.InsertAndSubmit(user);

            return user;
        }

        public User GetUser(int userId)
        {
            return _userRepository.GetById(userId);
        }

        public List<Role> GetUserRoles(string email)
        {
            var user = _userRepository.Get(t => t.Email == email);
            List<int> roleIds = _userRoleRepository.GetMany(t => t.UserId == user.Id).Select(t=>t.RoleId).ToList();
            
            return _roleRepository.GetMany(t => roleIds.Contains(t.Id)).ToList();
        }

        public Membership ValidateUser(string email, string password)
        {
            var membershipCtx = new Membership();
            var user = _userRepository.Get(t => t.Email == email);
            if (user != null && isUserValid(user, password))
            {
                var userRoles = GetUserRoles(user.Email);
                membershipCtx.User = user;

                var identity = new GenericIdentity(user.Email);
                membershipCtx.Principal = new GenericPrincipal(
                    identity,
                    userRoles.Select(x => x.Name).ToArray());
            }

            return membershipCtx;
        }

        private bool isPasswordValid(User user, string password)
        {
            return string.Equals(_encryptionService.EncryptPassword(password, user.Salt), user.Password);
        }

        private bool isUserValid(User user, string password)
        {
            if (isPasswordValid(user, password))
            {
                return !user.IsLocked;
            }

            return false;
        }
    }
}
