using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ResetPassword
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }

        [Required]
        [NotMapped]
        [MaxLength(16)]
        public string Password { get; set; }

        [Required]
        [NotMapped]
        [MaxLength(16)]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        [MaxLength(6)]
        [DataType(DataType.EmailAddress)]
        public string OTP { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ExpirationDate { get; set; }




    }
}
