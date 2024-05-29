using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using RPRENTAL_WEBAPP.Models.DTO.Amenity;
using RPRENTAL_WEBAPP.Models.DTO.Room;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPRENTAL_WEBAPP.Models.DTO.RoomAmenity
{
    public class RoomAmenityDTO
    {
        public RoomAmenityDTO()
        {

            Room = new RoomDTO();
            Amenity = new AmenityDTO();
            Amenities = new List<AmenityDTO>();
        }

        public int Id { get; set; }
   
        public int RoomId { get; set; }

        public RoomDTO? Room { get; set; }
      
        public int AmenityId { get; set; }

        public AmenityDTO? Amenity { get; set; }

        [NotMapped]
        [ValidateNever]
        public IEnumerable<AmenityDTO> Amenities { get; set; }



    }
}
