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
    public class Amenity
    {
       
        [Key] 
        public int AmenityId { get; set; }

        [Required]
        [MaxLength(100)]
        public string AmenityName { get; set; }

        public DateOnly? CreatedDate { get; set; }

        public DateOnly? UpdatedDate { get; set; }

        [NotMapped]
        [ValidateNever]
        public Boolean IsCheck { get; set; }

   
       }
}
