using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DTOs;

namespace Domain.DTOs
{
    public class RecruiterDto : UserDto
    {
        [Required]
        public Guid CompanyId { get; set; }
        // public CompanyDto Company { get; set; }
        public List<JobPostDto> JobPosts { get; set; }
    }
}