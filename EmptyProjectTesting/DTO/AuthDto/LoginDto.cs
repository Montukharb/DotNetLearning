using System.ComponentModel.DataAnnotations;

namespace EmptyProjectTesting.DTO.AuthDto
{
    public class LoginDto
    {
        [Required]
        required public string Email { get; set; }
        [Required]
        required public string Password { get; set; }
    }
}
