using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Room
    {
        public Room()
        {
            Description = string.Empty;
        }

        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomId { get; set; }

        [Required]
        public String RoomName { get;set; }

        public string Description { get; set; }

        [Range(10, 150)]
        public double RoomPrice { get; set; }

        [Range(1, 10)]
        public int MaxOccupancy { get; set; }    

        [NotMapped]
        [ValidateNever]    
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageUrlLocalPath { get; set; }

        [ValidateNever]
        public DateTime? CreatedDate { get; set; }

        [ValidateNever]
        public DateTime? UpdatedDate { get; set; }        

 

    }
}
