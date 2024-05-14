﻿using DatabaseAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model;
using Repository.Interface;
using Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataServices.Services.Interface;

namespace DataServices.Services.Implementation
{
    public class DBInitializer : IDBInitializer
    {
        private readonly ApplicationDBContext _db;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;

        public DBInitializer(ApplicationDBContext db, RoleManager<IdentityRole> rolemanager, UserManager<ApplicationUser> usermanager)
        {
            _db = db;
            _roleManager = rolemanager;
            _userManager = usermanager;
                
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
                if (!_roleManager.RoleExistsAsync(SD.UserRole.Admin.ToString()).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(SD.UserRole.Customer.ToString())).Wait();
                    _roleManager.CreateAsync(new IdentityRole(SD.UserRole.Admin.ToString())).Wait();

                    _userManager.CreateAsync(new ApplicationUser
                    {
                        UserName = "rolimer_pono@yahoo.com",
                        Email = "rolimer_pono@yahoo.com",
                        Fullname = "Rolimer Pono",
                        NormalizedUserName = "rolimer_pono@yahoo.com",
                        NormalizedEmail = "rolimer_pono@yahoo.com",
                        PhoneNumber = "0212477440",
                        CreatedDate = DateTime.Now,
                        EmailConfirmed = true,
                    },"Admin@123").GetAwaiter().GetResult();


                    ApplicationUser objUser = _db.tbl_User.Where(fw => fw.Email == "rolimer_pono@yahoo.com").FirstOrDefault()!;
                    _userManager.AddToRoleAsync(objUser, SD.UserRole.Admin.ToString()).GetAwaiter().GetResult();
                   
                }
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
