using DatabaseAccess;
using DataServices.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Model;
using Repository.Implementation;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Services.Implementation
{
    public class Worker : IWorker
    {
        
        private readonly ApplicationDBContext _db;
    
        public IRoomRepository tbl_Rooms { get; private set; }
        public IApplicationUserRepository tbl_User { get; private set; }
        public IRoomNumberRepository tbl_RoomNumber { get; private set; }        
        public IAmenityRepository tbl_Amenity { get;private set; }
        public IBookingRepository tbl_Bookings { get; private set; }
        public IRoomAmenityRepository tbl_RoomAmenity { get; private set; }
        public IResetPasswordRepository tbl_ResetPassword { get; private set; }     
        public Worker(ApplicationDBContext db)
        {
            _db = db;
            tbl_Rooms = new RoomRepository(_db);
            tbl_User = new ApplicationRepository(_db);
            tbl_RoomNumber = new RoomNumberRepository(_db);
            tbl_Amenity = new AmenityRepository(_db);
            tbl_RoomAmenity = new  RoomAmenityRepository(_db);
            tbl_ResetPassword = new ResetPasswordRepository(_db);   
            tbl_Bookings = new BookingRepository(_db);
        }
    }
}
