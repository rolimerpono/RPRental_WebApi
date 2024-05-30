using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using DataServices.Common.DTO.RoomAmenity;

namespace DataServices.Common.DTO.Room
{
    public class RoomDTO
    {
        public RoomDTO()
        {
            Description = string.Empty;
        }

        public int RoomId { get; set; }
        public string RoomName { get; set; }

        public string Description { get; set; }


        [Required]
        public double RoomPrice { get; set; }

        [Required]
        public int MaxOccupancy { get; set; }


        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [ValidateNever]
        public IEnumerable<RoomAmenityDTO>? RoomAmenities { get; set; }

        [NotMapped]
        [ValidateNever]
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageUrlLocalPath { get; set; }

        [ValidateNever]
        public bool IsRoomAvailable { get; set; }


        [ValidateNever]
        public DateOnly? CheckInDate { get; set; }

        [ValidateNever]
        public DateOnly? CheckOutDate { get; set; }
 
     
    }
}
