using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class SkillDto
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string SkillName { get; set; }
        public string SkillDescription { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}