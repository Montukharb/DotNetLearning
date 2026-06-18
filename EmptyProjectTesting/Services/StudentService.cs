using EmptyProjectTesting.Entites;
using EmptyProjectTesting.Repository;
using Microsoft.EntityFrameworkCore;

namespace EmptyProjectTesting.Services
{
    public interface IStudentServices
    {


        Task<bool> AddStudent(Student student);

        Task<bool> DeleteByIdStudents(int id);

        Task<Student?> GetByIdStudent(int id);

        Task<List<Student>> GetAllStudents();

    }
    public class StudentService : IStudentServices
    {
        private readonly IStudentRepository _repository;
        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> AddStudent(Student student)
        {
            if(student.Age < 18)
            {
                return false;
            }
            return await _repository.AddStudent(student);
        }
        public async Task<bool> DeleteByIdStudents(int Id)
        {
            return await _repository.DeleteByIdStudents(Id);
        }
        public async Task<Student?> GetByIdStudent(int Id)
        {
            return await _repository.GetByIdStudents(Id);
        }
        public async Task<List<Student>> GetAllStudents()
        {
            return await _repository.GetAllStudents();
        }
    }
}
