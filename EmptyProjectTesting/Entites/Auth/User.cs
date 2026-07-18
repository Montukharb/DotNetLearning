using System.ComponentModel.DataAnnotations;

namespace EmptyProjectTesting.Entites.Auth
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        required public string Name { get; set; }
        [Required]
        required public string Email { get; set; }
        [Required]
        required public string PasswordHash { get; set; }

        //Refresh Token Properties
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}