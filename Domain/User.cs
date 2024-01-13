using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class User: IdentityUser<Guid>
    {
        public string UserLastName { get; set; }
        public string Headline { get; set; }
        public string Summary { get; set; }
        public string Location { get; set; }
        public string ContactNumber { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Education> Educations { get; set; }
        public List<Experience> Experiences { get; set; }
    }
}