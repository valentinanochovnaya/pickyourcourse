namespace Application.Interfaces;

public interface IManagerInterface
{
    Task ApproveProfessor(string email);
    Task RejectProfessor(string email);
}
