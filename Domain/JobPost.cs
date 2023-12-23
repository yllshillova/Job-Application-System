using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class JobPost
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public DateTime DatePosted { get; set; }
        public Guid RecruiterId { get; set; }
        public Recruiter Recruiter { get; set; }
        public List<Application> Applications { get; set; }

    }
}