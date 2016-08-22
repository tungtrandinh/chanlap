using AutoMapper;
using ChanLap.Model;
using ChanLap.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChanLap.Web.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<User, UserViewModel>();
            Mapper.CreateMap<Role, RoleViewModel>();
            Mapper.CreateMap<UserRole, UserRoleViewModel>();
            
        }
    }
}