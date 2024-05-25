using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class RoomAmenity
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }

        public Room? Room { get; set; }

        [ForeignKey("Amenity")]
        public int AmenityId { get; set; }

        public Amenity? Amenity { get; set; }

    }
}
