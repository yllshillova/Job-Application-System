using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class ExperienceDto
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Position { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string CompanyName { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Location { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}