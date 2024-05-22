using DataServices.Common.DTO.Amenity;
using DataServices.Common.DTO.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Common.DTO.RoomAmenity
{
    public class RoomAmenityCreateDTO
    {
   

        public int RoomId { get; set; }

        public RoomDTO? Room { get; set; }

        public int AmenityId { get; set; }

        public AmenityDTO? Amenity { get; set; }
    }
}
