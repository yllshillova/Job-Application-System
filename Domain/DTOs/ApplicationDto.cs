using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class ApplicationDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateSubmitted { get; set; }
        [Required]
        public ResumeStorageDto ResumeFile { get; set; }
        [Required]
        public Guid JobPostId { get; set; }
        // public JobPostDto JobPost { get; set; }
        [Required]
        public Guid JobSeekerId { get; set; }
        // public JobSeekerDto JobSeeker { get; set; }
        [Required]
        public Guid EmailNotificationId { get; set; }
        // public EmailNotificationDto EmailNotification { get; set; }
    }
}