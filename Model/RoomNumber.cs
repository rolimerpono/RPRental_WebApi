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
    public class RoomNumber
    {
        public RoomNumber()
        {
            Description = string.Empty;
        }

        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RoomNo { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; } 

        public Room? Room { get; set; }

        [ValidateNever]
        public DateOnly CreatedDate { get; set; }
        [ValidateNever]
        public DateOnly UpdatedDate { get; set; }
    }
}
