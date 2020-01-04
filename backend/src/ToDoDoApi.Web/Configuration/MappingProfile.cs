using AutoMapper;
using ToDoDoApi.Core.Entities;
using ToDoDoApi.Web.Models;
using ToDoListApi.Models;

namespace ToDoDoApi.Web.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ToDoBindingModel, ToDo>();
            CreateMap<ToDo, ToDoApiModel>();
            CreateMap<AppUser, UserApiModel>();
            CreateMap<RegisterBindingModel, AppUser>();
        }
    }
}