using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string UserLastName { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Headline { get; set; }
        public string Summary { get; set; }
        public string Location { get; set; }
        [Phone]
        public string ContactNumber { get; set; }
        public string Token { get; set; }
        public List<SkillDto> Skills { get; set; }
        public List<EducationDto> Educations { get; set; }
        public List<ExperienceDto> Experiences { get; set; }
    }
}