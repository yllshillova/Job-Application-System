using DTOs;

namespace Domain.DTOs
{
    public class JobSeekerDto : UserDto
    {
        public List<ApplicationDto> Applications { get; set; }
    }
}