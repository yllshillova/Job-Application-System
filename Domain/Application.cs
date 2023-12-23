using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ForeignKey("JobPostId")]
        public JobPost JobPost { get; set; }
        public Guid JobSeekerId { get; set; }
        [ForeignKey("JobSeekerId")]
        public JobSeeker JobSeeker { get; set; }
        public Guid EmailNotificationId { get; set; }
        [ForeignKey("EmailNotificationId")]
        public EmailNotification EmailNotification { get; set; }


    }
}