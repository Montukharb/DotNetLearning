using EmptyProjectTesting.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmptyProjectTesting.Entity_Configuration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.Id); // Primary key
            builder.Property<string>(s => s.Name).HasMaxLength(30);
            builder.HasOne(e => e.Department).WithMany(d => d.Students).HasForeignKey(f => f.DepartmentId);
            //hasone means single navaigation prperty
            //hasmany means multiple navigations property
            //hasforeignkey which property is foreing key from another table primary key
            builder.HasOne(e => e.CountryFlag).WithMany(c => c.Students).HasForeignKey(f => f.CountryCode);
        }
    }
}

