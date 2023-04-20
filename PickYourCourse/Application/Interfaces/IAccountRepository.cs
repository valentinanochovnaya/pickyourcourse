using System.Security.Claims;
using Domain.Entities;

namespace Application.Interfaces;

public interface IAccountRepository
{
    Task RegisterStudent(Student student);
    Task RegisterProfessor(Professor professor);
    Task RegisterManager(Manager manager);
    String GetRole(String email, String password);
    List<Claim> Login(String email, String password, String role);
    Student GetStudent(String email);
    Professor GetProfessor(String email);
    Manager GetManager(String email);
}
