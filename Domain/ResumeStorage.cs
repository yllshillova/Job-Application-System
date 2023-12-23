using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class ResumeStorage
    {
        [Key]
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
    }
}