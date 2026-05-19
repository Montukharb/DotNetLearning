namespace WebApplicationBackend.Entities
{
    //STEP 1-> Create console app / asp.net core api app
    //STEP 2-> install these packages:
    /*
     Microsoft.EntityFrameworkCore.SqlServer  //use connect to database EF core
     Microsoft.EntityFrameworkCore.Tools // use to run migration commands and create/update database structure
     */

    //STEP 3-> to create entity class
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Salary { get; set; }
    }
}
