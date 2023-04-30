using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Services;

public class ProfessorService: IProfessorInterface
{
    private readonly DataContext.DataContext _context;

    public ProfessorService(DataContext.DataContext context)
    {
        _context = context;
    }
    public async Task<List<Professor>> GetRequestedProfessors()
    {
        return await _context.Professors.Where(professor => professor.IsPending == true).ToListAsync();
    }
    public async Task<List<Course>> GetProfessorCourses(Professor professor)
    {
        return await _context.Courses.Where(course => course.ProfessorId == professor.Id).ToListAsync();
    }
}
