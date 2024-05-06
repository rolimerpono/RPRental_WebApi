using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Email
    {

        [Key]
        public int Id { get;set; }

        [Required]
        [MaxLength(50)]
        public string SenderName { get; set; }

        [Required]
        [MaxLength(50)]
        public string SenderEmail { get; set; }

        [Required]
        [MaxLength(50)]
        public string RecieverName { get; set; }

        [Required]
        [MaxLength(50)]
        public string ReceiverEmail { get; set; }

        [Required]
        [MaxLength(50)]
        public string Subject { get; set; }

        [ValidateNever]
        public string TextContent { get; set; }

        [ValidateNever]
        public string HtmlContent { get; set; }

        [ValidateNever]
        public DateTime CreatedDate { get; set; }
    }
}
