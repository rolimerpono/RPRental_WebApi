
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace RPRENTAL_WEBAPP.Models.DTO.Amenity
{
    public class AmenityDTO
    {      
        public int AmenityId { get; set; }

        [MaxLength(100)]
        public string AmenityName { get; set; }

        public DateOnly? CreatedDate { get; set; }

        public DateOnly? UpdatedDate { get; set; }

        [ValidateNever]
        public Boolean IsCheck { get; set; }

      
    }
}
