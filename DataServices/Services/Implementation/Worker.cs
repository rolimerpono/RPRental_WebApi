using DatabaseAccess;
using DataServices.Services.Interface;
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

        public Worker(ApplicationDBContext db)
        {
            _db = db;
            tbl_Rooms = new RoomRepository(_db);

        }
    }
}
