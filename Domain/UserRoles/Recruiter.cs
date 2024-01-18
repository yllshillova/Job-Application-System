using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Recruiter : AppUser
    {
        public Guid CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        public List<JobPost> JobPosts {get; set;} 
    }
}