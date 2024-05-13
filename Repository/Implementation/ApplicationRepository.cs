using DatabaseAccess;
using Microsoft.AspNetCore.Identity;
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
    public  class ApplicationRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {


        
        private readonly ApplicationDBContext _db;
        public ApplicationRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;        

        }

       
    }
}
