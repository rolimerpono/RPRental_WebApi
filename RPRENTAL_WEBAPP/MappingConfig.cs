﻿using AutoMapper;
using Model;
using RPRENTAL_WEBAPP.Models.DTO;


namespace RPRENTAL_WEBAPP{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<RoomDTO, Room>().ReverseMap();

        
        }
    }
}