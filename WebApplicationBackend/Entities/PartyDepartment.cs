
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplicationBackend.Entities
{
    /*
     Data annotation
        | Method           | Location               |
        | ---------------- | ---------------------- |
        | Data Annotations | Model class ke andar   |
        | Fluent API       | `OnModelCreating()` me |
     */



    public class PartyDepartment
    {
        [Key] //key annotation mean create Another_Id as a Primary key
        public int Another_Id { get; set; }

        [Required]
        public int Id { get; set; } //according to naming convention Id apply identity and primary key auto

        [MaxLength(50)]
        public string PartyDepartmentName { get; set; }

        public int PartyExperiences { get; set; }
        public int MemberId { get; set; } // Forigen Key auto create using naming convention
        // Navigation property Member
        public Member Member { get; set; }
    }
}
