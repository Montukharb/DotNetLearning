using System.ComponentModel.DataAnnotations;

namespace EmptyProjectTesting.DTO.IdentityDTO
{
    public class IdentityUpdatePasswordDto
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
