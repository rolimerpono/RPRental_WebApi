using AutoMapper;
using Model;
using RPRENTAL_WEBAPP.Models.DTO.Room;
using RPRENTAL_WEBAPP.Models.DTO.RoomNumber;


namespace RPRENTAL_WEBAPP
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<Room, RoomDTO>().ReverseMap();
            CreateMap<Room, RoomCreateDTO>().ReverseMap();
            CreateMap<Room, RoomUpdateDTO>().ReverseMap();


            CreateMap<RoomNumber, RoomNumberDTO>().ReverseMap();
            CreateMap<RoomNumber, RoomNumberCreateDTO>().ReverseMap();
            CreateMap<RoomNumber, RoomNumberUpdateDTO>().ReverseMap();
        }
    }
}
