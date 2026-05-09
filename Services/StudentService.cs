using StudentApi.Data;
using StudentApi.Models;

namespace StudentApi.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        // Метод получения всех студентов
        public List<Student> GetAllStudents()
        {
            return _context.Students.ToList();
        }

        public string GetAppInfo()
        {
            return "Student API v1.0";
        }
    }
}