using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Recruiter : User
    {
        public Guid CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        public List<JobPost> JobPosts {get; set;} 
    }
}