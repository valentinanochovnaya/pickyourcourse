using Domain.Entities;

namespace WebApp.Models;

public class StudentViewModel
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public int? Year { get; set; }
    public int? Score { get; set; }
    public int? CoursesNumber { get; set; }
    public IList<Notification>? Notifications { get; set; }
    public IList<StudentCourseRelation>? StudentCourses { get; set; }
    public int? StudentCoursesNumber { get; set; }
}
