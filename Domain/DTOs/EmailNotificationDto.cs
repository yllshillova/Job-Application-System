using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class EmailNotificationDto
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string SentAt { get; set; }
        public Guid CompanyId { get; set; }
        public CompanyDto Company { get; set; }
    }
}