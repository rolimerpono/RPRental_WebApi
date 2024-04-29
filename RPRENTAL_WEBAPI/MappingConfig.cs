using AutoMapper;
using DataServices.Common.DTO;
using Model;



namespace RPRENTAL_WEBAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<Room, RoomDTO>().ReverseMap();     
            CreateMap<Room, RoomCreateDTO>().ReverseMap();
            CreateMap<Room, RoomUpdateDTO>().ReverseMap();

        
        }
    }
}
