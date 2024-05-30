using DataServices.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DataServices.Common.DTO
{
    public class Util
    {
        private readonly IWorker _iWorker;

        public Util(IWorker worker)
        {
            _iWorker = worker;

        }

        public async Task<int> GetRoomsAvailableCount(int RoomId, DateOnly CheckinDate, DateOnly CheckoutDate)
        {
         

            var roomNumbers = await _iWorker.tbl_RoomNumber.GetAllAsync(tr => tr.RoomId == RoomId);
            int room_number_count = roomNumbers.Count();

            var bookingCount = await _iWorker.tbl_Bookings.GetAllAsync(tb =>
            (tb.RoomId == RoomId) &&
            (tb.BookingStatus == SD.BookingStatus.Approved.ToString()) ||
            (tb.BookingStatus == SD.BookingStatus.Checkin.ToString()) &&
            (
                (CheckinDate >= tb.CheckinDate && CheckinDate < tb.CheckoutDate) ||
                (CheckoutDate > tb.CheckinDate && CheckoutDate <= tb.CheckoutDate) ||
                (CheckinDate <= tb.CheckinDate && CheckoutDate >= tb.CheckoutDate)
            ));

            int booking_count = bookingCount.Where(fw => fw.RoomId == RoomId).Count();

            int total_count = room_number_count - booking_count;

            return total_count;

        }

        //public List<string> GetRoomNumberAvailable(int RoomId, DateOnly CheckinDate, DateOnly CheckoutDate)
        //{
        //    var room_number_available = _iWorker.tbl_RoomNumber
        //                                .GetAll(tr => tr.RoomId == RoomId)
        //                                .Where(objRoom => !_iWorker.tbl_Booking.Any(tb =>
        //                                    tb.RoomId == RoomId &&
        //                                    ((CheckinDate >= tb.CheckinDate && CheckinDate < tb.CheckoutDate) ||
        //                                    (CheckoutDate > tb.CheckinDate && CheckoutDate <= tb.CheckoutDate) ||
        //                                    (CheckinDate <= tb.CheckinDate && CheckoutDate >= tb.CheckoutDate)) &&
        //                                    tb.RoomNo == objRoom.RoomNo &&
        //                                    tb.BookingStatus != SD.BookingStatus.Checkout.ToString()))
        //                                .Select(objRoom => objRoom.RoomNo.ToString())
        //                                .ToList();

        //    return room_number_available;
        //}



    }
}
