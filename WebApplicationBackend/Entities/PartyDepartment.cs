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
        public int Id { get; set; } //according to naming convention Id apply identity and primary key auto
        public string PartyDepartmentName { get; set; }

        public int PartyExperiences { get; set; }
        public int MemberId { get; set; } // Forigen Key auto create using naming convention
        // Navigation property Member
        public Member Member { get; set; }
    }
}
