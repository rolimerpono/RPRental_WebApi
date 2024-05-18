using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DataServices.Common.DTO.Room
{
    public class RoomUpdateDTO
    {

        [Required]
        public int RoomId { get; set; }

        [Required]
        public string RoomName { get; set; }

        public string? Description { get; set; }


        [Required]
        public double RoomPrice { get; set; }

        [Required]
        public int MaxOccupancy { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime? UpdatedDate { get; set; } = DateTime.Now;


    }
}
