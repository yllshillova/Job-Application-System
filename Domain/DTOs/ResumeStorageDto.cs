using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs
{
    public class ResumeStorageDto
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
    }
}