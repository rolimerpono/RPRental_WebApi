using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Common.DTO.Amenity
{
    public class AmenityUpdateDTO
    {      
        public int AmenityId { get; set; }

        [MaxLength(100)]
        public string AmenityName { get; set; }
     
    }
}
