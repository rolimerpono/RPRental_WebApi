using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DataServices.Common.DTO.Room
{
    public class RoomCreateDTO
    {


        public string RoomName { get; set; }

        public string? Description { get; set; }


        [Required]
        public double RoomPrice { get; set; }

        [Required]
        public int MaxOccupancy { get; set; }

        [NotMapped]
        [ValidateNever]
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageUrlLocalPath { get; set; }

        public DateTime? CreatedDate { get; set; }


    }
}
