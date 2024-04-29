using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DatabaseAccess
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {    

        }

        public DbSet<Room> tbl_Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region tbl_Rooms        

            builder.Entity<Room>().HasData(
            new Room
            {
                RoomId = 1,
                RoomName = "Single Room",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed finibus sed purus consequat porta. Praesent vitae tincidunt dolor, bibendum lacinia urna.",
                MaxOccupancy = 1,
                Price = 85,
                ImageUrl = @"\\img\\Rooms\\Single.jpg",
                CreatedDate = DateTime.Now,
                UpdatedDate = Convert.ToDateTime("1900-01-01")
            },
            new Room
            {
                RoomId = 2,
                RoomName = "Double Room",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed finibus sed purus consequat porta. Praesent vitae tincidunt dolor, bibendum lacinia urna.",
                MaxOccupancy = 2,
                Price = 90,
                ImageUrl = @"\\img\\Rooms\\Double.jpg",
                CreatedDate = DateTime.Now,
                UpdatedDate = Convert.ToDateTime("1900-01-01")


            },
            new Room
            {
                RoomId = 3,
                RoomName = "Deluxed Room",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed finibus sed purus consequat porta. Praesent vitae tincidunt dolor, bibendum lacinia urna.",
                MaxOccupancy = 3,
                Price = 100,
                ImageUrl = @"\\img\\Rooms\\Deluxed.jpg",
                CreatedDate = DateTime.Now,
                UpdatedDate = Convert.ToDateTime("1900-01-01")
            },
            new Room
            {
                RoomId = 4,
                RoomName = "Queens Room",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed finibus sed purus consequat porta. Praesent vitae tincidunt dolor, bibendum lacinia urna.",
                MaxOccupancy = 4,
                Price = 120,
                ImageUrl = @"\\img\\Rooms\\Queens.jpg",
                CreatedDate = DateTime.Now,
                UpdatedDate = Convert.ToDateTime("1900-01-01")
            },

            new Room
            {
                RoomId = 5,
                RoomName = "Kings Room",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed finibus sed purus consequat porta. Praesent vitae tincidunt dolor, bibendum lacinia urna.",
                MaxOccupancy = 5,
                Price = 130,
                ImageUrl = @"\\img\\Rooms\\Kings.jpg",
                CreatedDate = DateTime.Now,
                UpdatedDate = Convert.ToDateTime("1900-01-01")

            },
            new Room
            {
                RoomId = 6,
                RoomName = "Executive Suite",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed finibus sed purus consequat porta. Praesent vitae tincidunt dolor, bibendum lacinia urna.",
                MaxOccupancy = 10,
                Price = 100,
                ImageUrl = @"\\img\\Rooms\\Executive.jpg",
                CreatedDate = DateTime.Now,
                UpdatedDate = Convert.ToDateTime("1900-01-01")
            },
            new Room
            {
                RoomId = 7,
                RoomName = "Super Deluxed",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed finibus sed purus consequat porta. Praesent vitae tincidunt dolor, bibendum lacinia urna.",
                MaxOccupancy = 10,
                Price = 110,
                ImageUrl = @"\\img\\Rooms\\Super Deluxed.jpg",
                CreatedDate = DateTime.Now,
                UpdatedDate = Convert.ToDateTime("1900-01-01")

            },
            new Room
            {
                RoomId = 8,
                RoomName = "Diamond Room",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed finibus sed purus consequat porta. Praesent vitae tincidunt dolor, bibendum lacinia urna.",
                MaxOccupancy = 10,
                Price = 87,
                ImageUrl = @"\\img\\Rooms\\Diamond Room.jpg",
                CreatedDate = DateTime.Now,
                UpdatedDate = Convert.ToDateTime("1900-01-01")

            },
            new Room
            {
                RoomId = 9,
                RoomName = "Emerald Deluxed",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed finibus sed purus consequat porta. Praesent vitae tincidunt dolor, bibendum lacinia urna.",
                MaxOccupancy = 10,
                Price = 98,
                ImageUrl = @"\\img\\Rooms\\Emerald Room.jpg",
                CreatedDate = DateTime.Now,
                UpdatedDate = Convert.ToDateTime("1900-01-01")

            });

            #endregion

        }


    }
}
