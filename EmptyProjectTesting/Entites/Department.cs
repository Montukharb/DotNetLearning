using System.ComponentModel.DataAnnotations;

namespace EmptyProjectTesting.Entites
{
    public class Department
    {
        public int Id { get; set; }
       
        public string Name { get; set; }
        public string HOD { get; set; } //HOD = Head Of Department

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
