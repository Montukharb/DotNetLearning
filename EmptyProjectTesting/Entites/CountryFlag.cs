using System.ComponentModel.DataAnnotations;

namespace EmptyProjectTesting.Entites
{
    public class CountryFlag
    {
        [Key]
        public string Code { get; set; }
        public string CountryName { get; set; }
        public string CapitalName { get; set; }
        public string CurrencyName { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
