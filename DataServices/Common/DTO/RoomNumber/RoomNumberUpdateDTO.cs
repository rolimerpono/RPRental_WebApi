using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using DataServices.Common.DTO.Room;


namespace DataServices.Common.DTO.RoomNumber
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

    }
}
