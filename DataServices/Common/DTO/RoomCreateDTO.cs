using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DataServices.Common.DTO
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


    }
}
