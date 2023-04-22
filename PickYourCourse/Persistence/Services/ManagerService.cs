using Application.Interfaces;

namespace Persistence.Services;

public class ManagerService: IManagerInterface
{
    private readonly DataContext.DataContext _context;
    public ManagerService(DataContext.DataContext context)
    {
        _context = context;
    }

    public async Task ApproveProfessor(string email)
    {
        var professor = _context.Professors.FirstOrDefault(p => p.Email == email);
        if (professor != null)
        {
            professor.IsActivated = true;
            professor.IsPending = false;
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task RejectProfessor(string email)
    {
        var professor = _context.Professors.FirstOrDefault(p => p.Email == email);
        if (professor != null)
        {
            professor.IsActivated = false;
            professor.IsPending = false;
            await _context.SaveChangesAsync();
        }
    }
}
