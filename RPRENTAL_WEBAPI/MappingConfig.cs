using AutoMapper;
using DataServices.Common.DTO.Amenity;
using DataServices.Common.DTO.Room;
using DataServices.Common.DTO.RoomNumber;
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


            CreateMap<RoomNumber, RoomNumberDTO>().ReverseMap();
            CreateMap<RoomNumber, RoomNumberCreateDTO>().ReverseMap();
            CreateMap<RoomNumber, RoomNumberUpdateDTO>().ReverseMap();


            CreateMap<Amenity, AmenityDTO>().ReverseMap();
            CreateMap<Amenity, AmenityCreateDTO>().ReverseMap();
            CreateMap<Amenity,AmenityUpdateDTO>().ReverseMap();

        }
    }
}
