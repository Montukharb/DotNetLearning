using EmptyProjectTesting.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmptyProjectTesting.Entity_Configuration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            //builder.ToTable("NewTableName");
            builder.HasData(

                new Department
                {
                    Id = 1,
                    Name = "Computer Science Department",
                    HOD = "Dr. Rajesh Sharma"
                },

                new Department
                {
                    Id = 2,
                    Name = "Information Technology Department",
                    HOD = "Dr. Priya Verma"
                },

                new Department
                {
                    Id = 3,
                    Name = "Electronics Engineering Department",
                    HOD = "Dr. Amit Gupta"
                },

                new Department
                {
                    Id = 4,
                    Name = "Mechanical Engineering Department",
                    HOD = "Dr. Vikram Singh"
                },

                new Department
                {
                    Id = 5,
                    Name = "Civil Engineering Department",
                    HOD = "Dr. Neha Bansal"
                },

                new Department
                {
                    Id = 6,
                    Name = "Electrical Engineering Department",
                    HOD = "Dr. Anil Kumar"
                },

                new Department
                {
                    Id = 7,
                    Name = "Mathematics Department",
                    HOD = "Dr. Sunita Mehta"
                },

                new Department
                {
                    Id = 8,
                    Name = "Physics Department",
                    HOD = "Dr. Rakesh Malhotra"
                },

                new Department
                {
                    Id = 9,
                    Name = "Chemistry Department",
                    HOD = "Dr. Pooja Arora"
                },

                new Department
                {
                    Id = 10,
                    Name = "Biology Department",
                    HOD = "Dr. Rabindranath Tagore"
                },

                new Department
                {
                    Id = 11,
                    Name = "Commerce Department",
                    HOD = "Dr. Deepak Aggarwal"
                },

                new Department
                {
                    Id = 12,
                    Name = "Management Studies Department",
                    HOD = "Dr. Kavita Yadav"
                },

                new Department
                {
                    Id = 13,
                    Name = "Economics Department",
                    HOD = "Dr. Nitin Sethi"
                },

                new Department
                {
                    Id = 14,
                    Name = "English Department",
                    HOD = "Dr. Shalini Kapoor"
                },

                new Department
                {
                    Id = 15,
                    Name = "Hindi Department",
                    HOD = "Dr. Suresh Chandra"
                },

                new Department
                {
                    Id = 16,
                    Name = "History Department",
                    HOD = "Dr. Ashok Mishra"
                },

                new Department
                {
                    Id = 17,
                    Name = "Political Science Department",
                    HOD = "Dr. Meenakshi Jain"
                },

                new Department
                {
                    Id = 18,
                    Name = "Psychology Department",
                    HOD = "Dr. Rohit Khanna"
                },

                new Department
                {
                    Id = 19,
                    Name = "Environmental Science Department",
                    HOD = "Dr. Garima Joshi"
                },

                new Department
                {
                    Id = 20,
                    Name = "Data Science Department",
                    HOD = "Dr. Arjun Malhotra"
                }
                );
        }
    }
}
