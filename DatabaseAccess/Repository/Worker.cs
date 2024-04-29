using DataServices.Common.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.Repository
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
