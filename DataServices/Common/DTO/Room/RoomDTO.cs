using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DataServices.Common.DTO.Room
{
    public class RoomDTO
    {

        public int RoomId { get; set; }
        public string RoomName { get; set; }

        public string? Description { get; set; }


        [Required]
        public double RoomPrice { get; set; }

        [Required]
        public int MaxOccupancy { get; set; }


        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [NotMapped]
        [ValidateNever]
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageUrlLocalPath { get; set; }

        [NotMapped]
        public bool IsRoomAvailable { get; set; }


        [NotMapped]
        public DateOnly? CheckInDate { get; set; }

        [NotMapped]
        public DateOnly? CheckOutDate { get; set; }
 
     
    }
}
