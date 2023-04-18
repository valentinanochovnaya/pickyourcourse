using Domain.Entities;
using WebApp.Models;

namespace WebApp.Interfaces;

public interface IMapper
{
    Student StudentVMToStudent(RegisterViewModel registerViewModel);
    Professor ProfessorVNToProfessor(RegisterViewModel registerViewModel);
}
