using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DataServices.Common.DTO.Room;
using DataServices.Common.DTO.Amenity;

namespace DataServices.Common.DTO.RoomAmenity
{
    public class RoomAmenityDTO
    {
   
        public int Id { get; set; }
   
        public int RoomId { get; set; }

        public RoomDTO? Room { get; set; }
      
        public int AmenityId { get; set; }

        public AmenityDTO? Amenity { get; set; }

    }
}
