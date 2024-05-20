using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using RPRENTAL_WEBAPP.Models.DTO.Room;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace RPRENTAL_WEBAPP.Models.DTO.RoomNumber
{
    public class RoomNumberUpdateDTO
    {
        public RoomNumberUpdateDTO()
        {
            RoomNo = 0;
            Description = string.Empty;
       
        }
        public int RoomNo { get; set; }

        public string Description { get; set; }

        public int RoomId { get; set; }

        [ValidateNever]
        public RoomDTO? Room { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem>? RoomList { get; set; }
    }
}
