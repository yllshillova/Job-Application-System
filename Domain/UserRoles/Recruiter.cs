namespace Domain
{
    public class Recruiter : User
    {
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

        public List<JobPost> JobPosts {get; set;} 
    }
}