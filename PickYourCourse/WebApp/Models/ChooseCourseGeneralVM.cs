using Domain.Entities;

namespace WebApp.Models;

public class ChooseCourseGeneralVM
{
    public string? Email { get; set; }
    public List<Course> StudentCourses { get; set; }
}
