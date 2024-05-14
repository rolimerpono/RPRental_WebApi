using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DataServices.Common.DTO.Room
{
    public class RoomDTO
    {

        public int RoomId { get; set; }

        [Required]
        public string RoomName { get; set; }

        public string? Description { get; set; }


        [Required]
        public double Price { get; set; }

        [Required]
        public int MaxOccupancy { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        [NotMapped]
        public bool IsRoomAvailable { get; set; }


        [NotMapped]
        public DateOnly? CheckInDate { get; set; }

        [NotMapped]
        public DateOnly? CheckOutDate { get; set; }
    }
}
