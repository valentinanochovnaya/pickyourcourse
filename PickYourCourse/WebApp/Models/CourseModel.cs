namespace WebApp.Models;

public class CourseModel
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Year { get; set; }
    public int Term { get; set; }
    
    public int ProfessorId { get; set; }
}
