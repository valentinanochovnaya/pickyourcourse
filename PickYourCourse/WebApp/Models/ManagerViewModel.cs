using Domain.Entities;

namespace WebApp.Models;

public class ManagerViewModel
{
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public List<Professor>? RequestedProfessors { get; set; }
    public List<Professor>? ProfessorsWithManagerRights { get; set; }
    
}
