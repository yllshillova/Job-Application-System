using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey("RecruiterId")]
        public Recruiter Recruiter { get; set; }
        public List<ApplicationEntity> Applications { get; set; }
        
    }
}