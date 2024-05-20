using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using RPRENTAL_WEBAPP.Models.DTO.Room;

namespace RPRENTAL_WEBAPP.Models.DTO.RoomNumber
{
    public class RoomNumberDTO
    {
        public RoomNumberDTO()
        {
            RoomNo = 0;
            Description = String.Empty;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RoomNo { get; set; }


        [MaxLength(500)]
        public string Description { get; set; }

        [ValidateNever]
        public RoomDTO? Room { get; set; }

    }

}

