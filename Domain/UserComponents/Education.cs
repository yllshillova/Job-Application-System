using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Education
    {
        [Key]
        public Guid Id { get; set; }
        public string Institution { get; set; }
        public string Degree { get; set; }
        public string GraduationYear { get; set; }

        public Guid UserId { get; set; }
         [ForeignKey("UserId")]
        public AppUser User { get; set; }
    }
}