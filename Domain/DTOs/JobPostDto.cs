using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class JobPostDto
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Requirements { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DatePosted { get; set; }
        [Required]
        public Guid RecruiterId { get; set; }
        public List<ApplicationDto> Applications { get; set; }
    }
}