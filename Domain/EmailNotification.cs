using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class EmailNotification
    {
        [Key]
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        public string Subject { get; set; }

        public string Body {get; set;} 
        public string SentAt { get; set; }

        public Guid CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }


    }
}