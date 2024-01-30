using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.AccountDTOs;

public class RegisterRecruiterDto : RegisterDto
{

    [Required]
    public Guid CompanyId { get; set; }

}
