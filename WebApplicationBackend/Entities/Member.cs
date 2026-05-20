namespace WebApplicationBackend.Entities
{
    public class Member
    {
        public int Id { get; set; }
        public string MemberName { get; set; } = string.Empty;

        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime JoinDate { get; set; }

    }
}
