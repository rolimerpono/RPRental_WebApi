using RPRENTAL_WEBAPP.Models.DTO.RoomAmenity;

namespace RPRENTAL_WEBAPP.Models.DTO.Home
{
    public class HomeDTO
    {

        public HomeDTO()
        {
            RoomAmenities = new List<RoomAmenityDTO>();
        }

        public int RoomId { get; set; }

        public string RoomName { get; set; }

        public string? Description { get; set; }

        public required double RoomPrice { get; set; }

        public required int MaxOccupancy { get; set; }

        public string? ImageUrl { get; set; }

        public IFormFile? Image { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public IEnumerable<RoomAmenityDTO>? RoomAmenities { get; set; }

        public Boolean IsRoomAvailable { get; set; } = true;

        public DateOnly? CheckinDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public DateOnly? CheckoutDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
