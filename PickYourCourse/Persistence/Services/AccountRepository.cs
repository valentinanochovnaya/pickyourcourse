
using Application.Interfaces;
using Domain.Entities;

namespace Persistence.Services;

public class AccountRepository: IAccountRepository
{
    private readonly DataContext.DataContext _context;

    public AccountRepository(DataContext.DataContext context)
    {
        _context = context;
    }
    public async Task RegisterStudent(Student student)
    {
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
    }
    
    public async Task RegisterProfessor(Professor professor)
    {
        await _context.Professors.AddAsync(professor);
        await _context.SaveChangesAsync();
    }
}
