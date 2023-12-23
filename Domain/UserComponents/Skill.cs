using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Skill
    {
        [Key]
        public Guid Id { get; set; }
        public string SkillName { get; set; }
        public string SkillDescription { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}