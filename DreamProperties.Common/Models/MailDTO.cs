using System;
using System.ComponentModel.DataAnnotations;

namespace DreamProperties.Common.Models
{
    public class MailDTO
    {
        [Required]
        public string FromEmail { get; set; }
        [Required]
        public string ToEmail { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int PropertyId { get; set; }
        [Required]
        public string PropertyTitle { get; set; }
    }
}
