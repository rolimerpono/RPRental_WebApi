using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace RPRENTAL_WEBAPP.Models.DTO
{
    public class RoomCreateDTO
    {
 

        [Required]
        public String RoomName { get; set; }

        public string? Description { get; set; }


        [Required]
        public double Price { get; set; }

        [Required]
        public int MaxOccupancy { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        [NotMapped]
        public Boolean IsRoomAvailable { get; set; }


        [NotMapped]
        public DateOnly? CheckInDate { get; set; }

        [NotMapped]
        public DateOnly? CheckOutDate { get; set; }
    }
}
