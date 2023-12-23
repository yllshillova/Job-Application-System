using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Headline { get; set; }
        public string Summary { get; set; }
        public string Location { get; set; }
        public string ContactNumber { get; set; }
        public List<Skill> Skills{ get; set; }
        public List<Education> Educations{ get; set; }
        public List<Experience> Experiences{ get; set; }
    }
}