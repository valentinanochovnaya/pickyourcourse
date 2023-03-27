using Domain.Entities;

namespace Application.Interfaces;

public interface IAccountRepository
{
    Task RegisterStudent(Student student);
    Task RegisterProfessor(Professor professor);
    String GetRole(String email, String password);
}
