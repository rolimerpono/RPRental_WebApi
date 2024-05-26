namespace RPRENTAL_WEBAPP.Models.DTO.RoomAmenity
{
    public class RoomAmenityCreateDTO
    {  

        public int RoomId { get; set; }

        public List<int> AmenityId { get; set; } = new List<int>();
    }
}
