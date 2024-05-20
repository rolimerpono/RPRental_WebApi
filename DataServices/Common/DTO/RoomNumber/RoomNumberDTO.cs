using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

using Model;
using DataServices.Common.DTO.Room;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DataServices.Common.DTO.RoomNumber
{
    public class RoomNumberDTO
    {
        public RoomNumberDTO()
        {
            RoomNo = 0;
            Description = string.Empty;
  
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RoomNo { get; set; }


        [MaxLength(500)]
        public string Description { get; set; }

       public RoomDTO? Room { get; set; }

    }

}

