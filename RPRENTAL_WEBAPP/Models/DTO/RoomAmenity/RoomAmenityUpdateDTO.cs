using RPRENTAL_WEBAPP.Models.DTO.Amenity;
using RPRENTAL_WEBAPP.Models.DTO.Room;

namespace RPRENTAL_WEBAPP.Models.DTO.RoomAmenity
{
    public  class RoomAmenityUpdateDTO
    {

        public int Id { get; set; }

        public int RoomId { get; set; }

        public RoomDTO? Room { get; set; }

        public int AmenityId { get; set; }

        public AmenityDTO? Amenity { get; set; }


    }
}
