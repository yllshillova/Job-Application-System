using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class ApplicationDto
{
    public Guid Id { get; set; }
    public string Status { get; set; }
    public DateTime DateSubmitted { get; set; }
    public ResumeStorageDto ResumeFile { get; set; }
    public Guid JobPostId { get; set; }
    public JobPostDto JobPost { get; set; }
    public Guid JobSeekerId { get; set; }
    public JobSeekerDto JobSeeker { get; set; }
    public Guid EmailNotificationId { get; set; }
    public EmailNotificationDto EmailNotification { get; set; }
}
}