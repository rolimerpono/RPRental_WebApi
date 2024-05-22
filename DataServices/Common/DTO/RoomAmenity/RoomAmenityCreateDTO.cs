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

        public List<int> AmenityId { get; set; } = new List<int>();
    }
}
