using StudentApi.Models;

namespace StudentApi.Services
{
    public interface IStudentService
    {
        List<Student> GetAllStudents();
        string GetAppInfo();
    }
}