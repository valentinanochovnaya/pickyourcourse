using Domain.Entities;

namespace WebApp.Models;

public class ProfessorViewModel
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public bool? IsActivated { get; set; }
    
    public CourseModel? NewCourse { get; set; }
    public List<Course>? ProfessorCourses { get; set; }
}
