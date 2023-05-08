using Domain.Entities;

namespace Application.Interfaces;

public interface IStudentInterface
{
    Task<Student> GetStudent(string email);
    Task<List<Course>> GetStudentCourses(Student student);
}
