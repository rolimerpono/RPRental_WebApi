using AutoMapper;
using Model;
using RPRENTAL_WEBAPP.Models.DTO.Room;


namespace RPRENTAL_WEBAPP
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {

            CreateMap<Room, RoomCreateDTO>().ReverseMap();
            CreateMap<Room, RoomCreateDTO>().ReverseMap();
            CreateMap<Room, RoomUpdateDTO>().ReverseMap();

        }
    }
}
