using EmptyProjectTesting.DbContexts;
using EmptyProjectTesting.Entites;
using Microsoft.EntityFrameworkCore;

namespace EmptyProjectTesting.Repository
{
    public interface IStudentRepository
    {
        public Task<List<Student>> GetAllStudents();
        public Task<Student?> GetByIdStudents(int Id);

        public Task<bool> DeleteByIdStudents(int Id);
        public Task<bool> AddStudent(Student student);
    }
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetAllStudents()
        {
            return await _context.Students.Include(i => i.Department).ToListAsync();
            //return await _context.Students.Include(i=>i.Department).Include(j=>j.CountryFlag).ToListAsync();
            /*
             include ka use relationship entity ka data show karne ka liya use hota hai 
             for example one to one , one to many, many to one , many to many etc
             note: include se infinite json cycyle create hoti hai isko ignore karne ke liya program.cs ma service ma addcontroller karte hai 
             Code 
              builder.Services.AddControllers().AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    });
            ye error include ki waajah se directly nahi, balki JSON serialization ka time aata hai
             aise cycle banti hai
             student
               |
            Department
               |
            Student
               |
            Department
               |
            Student
               |
            Continue rahe ga

            Solution 1. (most Recommended) - DTO

            Entity directly return mat karo include se
            custom obect send karo

            var data = await context.Students.Select(s=> new StudentDto
            {
             Id = s.Id,
            Name = s.Name,
            DepartmentName = s.Department.Name,
            etc
            }).ToListAsync();

            return data;

            Solution 2. Ignore Cycles ye upper de rakah hai 

            best hoga dto se hi pass kare


********** types of include using ************
            1. var departments = context.Departments
                         .Include(d => d.Students)
                         .ToList();
            2. Multiple include 
            Agar student ki or bhi navigation property hai myltiple include use kar satke hai 
            var students = context.Students
                      .Include(s => s.Department)
                      .Include(s => s.Address)
                      .ToList();

            3. Theninclude()  jab relation ka ander relation load karna ho
            var departments = context.Departments
                         .Include(d => d.Students)
                         .ThenInclude(s => s.Courses)
                         .ToList();
            NestedRelation ka liys theninclude use karte hai.
             */
        }

        public async Task<Student?> GetByIdStudents(int Id)
        {
            return await _context.Students.FindAsync(Id);
        }

        public async Task<bool> DeleteByIdStudents(int Id)
        {
            Student? student = await _context.Students.FindAsync(Id);
            if (student == null)
            {
                return false;
            }
            _context.Students.Remove(student);
            int effectRow = await _context.SaveChangesAsync();
            return effectRow > 0;
        }


        public async Task<bool> AddStudent(Student student)
        {
            if (student == null)
            {
                return false;
            }
            await _context.Students.AddAsync(student);
            int rowEffected = await _context.SaveChangesAsync();
            return rowEffected > 0;
        }
    }
}
