using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using RPRENTAL_WEBAPP.Models.DTO.Room;


namespace RPRENTAL_WEBAPP.Models.DTO.RoomNumber
{
    public class RoomNumberUpdateDTO
    {
        public int RoomNo { get; set; }

        public string? Description { get; set; }

        public int RoomId { get; set; }

        public RoomDTO? RoomDTO { get; set; }
    }
}
