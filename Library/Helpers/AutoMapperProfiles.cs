using AutoMapper;
using Library.DTO;
using Library.Entities;

namespace Library.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<User, RegisterDTO>();
        }
    }
}
