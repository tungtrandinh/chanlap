using AutoMapper;
using ChanLap.Model;
using ChanLap.Web.Models;


namespace ChanLap.Web.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        
        protected override void Configure()
        {
            Mapper.CreateMap<UserViewModel, User>();
            Mapper.CreateMap<RoleViewModel, Role>();
            Mapper.CreateMap<UserRoleViewModel, UserRole>();
        }
    }
}