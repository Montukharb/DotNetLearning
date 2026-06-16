using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmptyProjectTesting.Entites
{
    //[Table("Students")] to change table name using data annotation
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Range(18,100,ErrorMessage = "Age must be between 18 and 100" )]
        public int Age { get; set; }
        [AllowedValues("Male", "Female", "Transgender", ErrorMessage = "Invalid Gender Selected")]
        public string Gender { get; set; }
        [EmailAddress(ErrorMessage ="Invalid email address")]
        public string Email { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        [Required]
        public string CountryCode { get; set; }
        public CountryFlag CountryFlag { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
