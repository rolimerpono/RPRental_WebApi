using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Room
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  
        public int RoomId { get; set; }
        
        public String RoomName { get;set; }

        public string? Description { get; set; }

        [Range(10, 150)]
        public double RoomPrice { get; set; }

        [Range(1, 10)]
        public int MaxOccupancy { get; set; }

        public string? ImageUrl { get; set; }

        [ValidateNever]
        [NotMapped]
        public IFormFile? Image { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }


    }
}
