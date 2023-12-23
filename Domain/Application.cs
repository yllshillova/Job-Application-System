using System.ComponentModel.DataAnnotations;
namespace Domain
{
    public class Application
    {
        [Key]
        public Guid Id { get; set; }
        public string Status { get; set; }
        public DateTime DateSubmitted { get; set; }
        public ResumeStorage ResumeFile { get; set; }
        public Guid JobPostId { get; set; }
        public JobPost JobPost { get; set; }
        public Guid JobSeekerId { get; set; }
        public JobSeeker JobSeeker { get; set; }
        public Guid EmailNotificationId { get; set; }
        public EmailNotification EmailNotification { get; set; }


    }
}