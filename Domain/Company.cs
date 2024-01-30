using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey("EntrepreneurId")]
        public Entrepreneur Entrepreneur { get; set; }
        public List<Recruiter> Recruiters { get; set; }

    }
}