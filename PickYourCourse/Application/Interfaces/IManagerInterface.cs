using Domain.Entities;

namespace Application.Interfaces;

public interface IManagerInterface
{
    Task ApproveProfessor(string email);
    Task RejectProfessor(string email);
    Task<int> ApproveManager(string email);
    Task<List<Professor>> GetProfessorsWithManagerRights();
}
