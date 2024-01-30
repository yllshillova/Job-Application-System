using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.AccountDTOs;

public class RegisterDto
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string UserLastName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$",
     ErrorMessage ="Password must be at minimum 4 to maximum 8 characters, and must include a number, lower and uppercase letter ")]
    public string Password { get; set; }

}
