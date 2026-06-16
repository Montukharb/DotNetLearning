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
            return await _context.Students.Include(i=>i.Department).ToListAsync();
            //return await _context.Students.Include(i=>i.Department).Include(j=>j.CountryFlag).ToListAsync();
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
