using DTOs;

namespace Domain.DTOs.UserDTOs
{
    public class EntrepreneurDto : UserDto
    {
        public List<CompanyDto> Companies { get; set; }
    }
}