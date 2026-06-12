using System.ComponentModel.DataAnnotations;

namespace EmptyProjectTesting.Entites
{
    public class SpiralNotebook
    {
        [Key]
        public int Id { get; set; }
        [MinLength(2)]
        [MaxLength(60)]
        public string? Name { get; set; }
        [Required,EmailAddress]
        public string? Email { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
    }
}
