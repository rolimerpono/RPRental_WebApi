
using System.ComponentModel.DataAnnotations;

namespace RPRENTAL_WEBAPP.Models.DTO.Amenity
{
    public class AmenityUpdateDTO
    {      
        public int AmenityId { get; set; }

        [MaxLength(100)]
        public string AmenityName { get; set; }
     
    }
}
