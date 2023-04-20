using Domain.Entities;

namespace Application.Interfaces;

public interface IProfessorInterface
{
    Task<List<Professor>> GetRequestedProfessors();
}
