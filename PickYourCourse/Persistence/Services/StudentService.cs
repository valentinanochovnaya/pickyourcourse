using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Services;

public class StudentService: IStudentInterface
{
    private readonly DataContext.DataContext _context;
    public StudentService(DataContext.DataContext context)
    {
        _context = context;
    }

    public async Task<Student> GetStudent(string email)
    {
        return await _context.Students?.FirstOrDefaultAsync(student => student.Email == email);
    }

    public async Task<List<Course>> GetStudentCourses(Student student)
    {
        return await _context.Courses.Where(course => course.Year == student.Year).ToListAsync();
    }
}
