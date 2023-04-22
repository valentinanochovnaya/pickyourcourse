using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

    public async Task<int> ApproveManager(string email)
    {
        var professor = _context.Professors.FirstOrDefault(p => p.Email == email);
        if (professor != null)
        {
            professor.IsManager = true;
           // _context.Managers.AddAsync(professor)
            await _context.SaveChangesAsync();
            return 1;
        }

        return 0;
    }
    
    public async Task<List<Professor>> GetProfessorsWithManagerRights()
    {
        return await _context.Professors.Where(p => p.IsManager).ToListAsync();
    }
}
