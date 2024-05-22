using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Services.Interface
{
    public interface IWorker
    {
        IRoomRepository tbl_Rooms { get; }
        IApplicationUserRepository tbl_User { get; }
        IRoomNumberRepository tbl_RoomNumber { get; }
        IAmenityRepository tbl_Amenity { get; }
        IRoomAmenityRepository tbl_RoomAmenity { get; }
        IBookingRepository tbl_Bookings { get; }
        IResetPasswordRepository tbl_ResetPassword { get; }     



    }
}
