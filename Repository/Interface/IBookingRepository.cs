using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task UpdateAsync(Booking objBooking);

        Task UpdateBookingStatusAsync(int BookingID, string BookingStatus, int RoomNumber);

        Task UpdateStripePaymentIDAsync(int BookingID, string SessionID, string StripePaymentID);

        Task UpdatePaypalPaymentIDAsync(int BookingID, string SessionID, string PaypalPaymentID);
    

    }
}
