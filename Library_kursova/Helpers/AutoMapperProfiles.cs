using AutoMapper;
using Library_kursova.DTO;
using Library_kursova.Entities;

namespace Library_kursova.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<User, RegisterDTO>();
        }
    }
}
