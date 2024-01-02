using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class EducationDto
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Institution { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Degree { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string GraduationYear { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}