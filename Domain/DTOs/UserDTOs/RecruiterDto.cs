using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTOs;

namespace Domain.DTOs
{
    public class RecruiterDto : UserDto
    {
        public Guid CompanyId { get; set; }
        // public CompanyDto Company { get; set; }
        public List<JobPostDto> JobPosts { get; set; }
    }
}