using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        public string Industry { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public Guid EntrepreneurId { get; set; }
        public List<RecruiterDto> Recruiters { get; set; }
        public List<EmailNotificationDto> EmailNotifications { get; set; }

    }
}