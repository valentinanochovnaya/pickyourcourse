using Domain.Entities;

namespace Application.Interfaces;

public interface IProfessorInterface
{
    Task<List<Professor>> GetRequestedProfessors();
    Task<List<Course>> GetProfessorCourses(Professor professor);
}
