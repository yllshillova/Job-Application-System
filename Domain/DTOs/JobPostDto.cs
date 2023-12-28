using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.DTOs
{
   public class JobPostDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Requirements { get; set; }
    public DateTime DatePosted { get; set; }
    public Guid RecruiterId { get; set; }
    public RecruiterDto Recruiter { get; set; }
    // If you want to include Applications, you can add a property like:
    // public List<ApplicationDto> Applications { get; set; }
}
}