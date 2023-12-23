using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Experience
    {
        [Key]
        public Guid Id { get; set; }
        public string Position { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
         [ForeignKey("UserId")]
        public User User { get; set; }
    }
}