using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using WebApplicationBackend.Entities;


namespace WebApplicationBackend.Context
{
    //Step 4-> DbContext means database manager class
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {  //Database Connection Options

        }

        //Table representation
        public DbSet<Employee> Employees { get; set; } //Employees is a table name
        public DbSet<Member> Members { get; set; }
        //public DbSet<State> States { get; set; }

        public DbSet<PartyDepartment> PartyDepartments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            //using fluent Api Table name change
            //modelBuilder.Entity<PartyDepartment>().ToTable("PartyDepartments");
            /*
            Entity class ka naam Member rahega,
            DbSet ka naam Members rahega,
            but database table ka naam INCMembers banega.

            */
            modelBuilder.Entity<Member>().HasData(
                       new Member
                       {
                           Id = 1,
                           MemberName = "Rahul Gandhi",
                           Age = 54,
                           Gender = "Male",
                           Phone = "9876543210",
                           Email = "rahul@inc.com",
                           JoinDate = new DateTime(2004, 5, 10)
                       },

            new Member
            {
                Id = 2,
                MemberName = "Mallikarjun Kharge",
                Age = 82,
                Gender = "Male",
                Phone = "9876543211",
                Email = "kharge@inc.com",
                JoinDate = new DateTime(1969, 3, 15)
            },

            new Member
            {
                Id = 3,
                MemberName = "Priyanka Gandhi",
                Age = 53,
                Gender = "Female",
                Phone = "9876543212",
                Email = "priyanka@inc.com",
                JoinDate = new DateTime(1999, 8, 20)
            },

            new Member
            {
                Id = 4,
                MemberName = "Shashi Tharoor",
                Age = 70,
                Gender = "Male",
                Phone = "9876543213",
                Email = "tharoor@inc.com",
                JoinDate = new DateTime(2009, 1, 12)
            },

            new Member
            {
                Id = 5,
                MemberName = "Sachin Pilot",
                Age = 48,
                Gender = "Male",
                Phone = "9876543214",
                Email = "pilot@inc.com",
                JoinDate = new DateTime(2004, 9, 5)
            }
                );//base.OnModelCreating(modelBuilder);



    }
}


[Table("Students")] //table name change fluent api isko bhi override kar sakti hai 
public class Student
{
    // PRIMARY KEY
    [Key]
    public int Id { get; set; }


    // REQUIRED + LENGTH
    [Required]
    [StringLength(50, MinimumLength = 3)]
    //50 means maximum,
    public string Name { get; set; } = null!;


    // RANGE
    [Range(18, 30)] //age 35 invalid age 25 valid
    public int Age { get; set; }


    // EMAIL VALIDATION
    [MaxLength(80)] //maximum length set 
    [EmailAddress] //emailformat validation

    public string Email { get; set; } = null!;


    // PHONE VALIDATION
    [MinLength(10)] // minimum character validation
    [Phone] //phone number validation
    public string PhoneNumber { get; set; } = null!;

    // COLUMN NAME CHANGE and type 
    [Column("Student_City", TypeName = "varchar(100)")]
    public string City { get; set; }


    // DECIMAL TYPE
    [Column(TypeName = "decimal(10,2)")]
    //both are right
    [Precision(12, 2)]
    public decimal Fees { get; set; }

    [Unicode(true)]  //means nvarchar enabled
    /*
     Unicode ka matlab text me almost all languages ke characters support: Hindi, Arabic, Chinese, emoji, special symbols, etc.

Non-Unicode mainly limited character sets ke liye hota hai, jaise English/ASCII type data. 
     */
    public string Name2 { get; set; }
    // DATE TYPE
    [DataType(DataType.Date)]
    public DateTime DOB { get; set; }


    // MAX LENGTH
    [MaxLength(200)]
    public string Address { get; set; }


    // IGNORE COLUMN
    [NotMapped]
    public string TempData { get; set; }
}
