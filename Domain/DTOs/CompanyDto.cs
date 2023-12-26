using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Industry { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public List<Recruiter> Recruiters { get; set; }
        public List<EmailNotification> EmailNotifications {get; set;}

    }
}