using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Industry { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public Guid EntrepreneurId { get; set; }
        public Entrepreneur Entrepreneur { get; set; }
        public List<Recruiter> Recruiters { get; set; }
        public List<EmailNotification> EmailNotifications {get; set;}

    }
}