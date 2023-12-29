using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class EmailNotificationDto
    {
        public Guid Id { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string SentAt { get; set; }
        [Required]
        public Guid CompanyId { get; set; }
        // public CompanyDto Company { get; set; }
    }
}