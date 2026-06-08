using System.ComponentModel.DataAnnotations;

namespace Batmen.Entity
{
    public class BatmanSkill
    {
        //[Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [EmailAddress]
        [Required]
        [StringLength(80)]
        public string Email { get; set; } = null!;

    }
}
