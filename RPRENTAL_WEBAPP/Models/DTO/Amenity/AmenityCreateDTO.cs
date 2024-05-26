
using System.ComponentModel.DataAnnotations;

namespace RPRENTAL_WEBAPP.Models.DTO.Amenity
{
    public class AmenityCreateDTO
    {
      
        [MaxLength(100)]
        public string AmenityName { get; set; }
    }
}
