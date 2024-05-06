using DatabaseAccess;
using Microsoft.EntityFrameworkCore;
using Model;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Repository.Implementation
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {

        private readonly ApplicationDBContext _db;

        public BookingRepository(ApplicationDBContext db)  : base(db)
        {
            _db = db;
        }

        public async Task UpdateAsync(Booking objBooking)
        {
            _db.tbl_Bookings.Update(objBooking);
        }

        public async Task UpdateBookingStatusAsync(int BookingID, string BookingStatus, int RoomNumber)
        {
            Booking objBooking = await _db.tbl_Bookings!.FirstOrDefaultAsync(fw => fw.BookingId == BookingID);

            SD.BookingStatus objStatus;
            objStatus = (SD.BookingStatus)Enum.Parse(typeof(SD.BookingStatus), BookingStatus);

            switch (objStatus)
            {
                case SD.BookingStatus.Checkin:
                    objBooking.RoomNo = RoomNumber;
                    objBooking.ActualCheckinDate = DateTime.Now;
                    objBooking.BookingStatus = BookingStatus;
                    break;

                case SD.BookingStatus.Checkout:
                    objBooking.ActualCheckoutDate = DateTime.Now;
                    objBooking.BookingStatus = BookingStatus;
                    break;

                case SD.BookingStatus.Approved:
                    objBooking.BookingDate = DateTime.Now;
                    objBooking.BookingStatus = BookingStatus;
                    break;

                case SD.BookingStatus.Cancelled:
                    objBooking.BookingStatus = BookingStatus;
                    objBooking.ActualCheckinDate = DateTime.Now;

                    break;

            }
        }


        public async Task UpdateStripePaymentIDAsync(int BookingID, string SessionID, string StripePaymentID)
        {
            Booking objBooking = await _db.tbl_Bookings!.FirstOrDefaultAsync(fw => fw.BookingId == BookingID);

            if (objBooking != null)
            {
                if (!string.IsNullOrEmpty(SessionID))
                {
                    objBooking.StripeSessionId = SessionID;
                }

                if (!string.IsNullOrEmpty(StripePaymentID))
                {
                    objBooking.StripePaymentIntentId = StripePaymentID;
                    objBooking.PaymentDate = DateTime.Now;
                    objBooking.IsPaymentSuccessfull = true;

                }

            }
        }

        public async Task UpdatePaypalPaymentIDAsync(int BookingID, string SessionID, string PaypalPaymentID)
        {
            throw new NotImplementedException();
        }

    }
}
