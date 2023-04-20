using System.Security.Cryptography;
using System.Text;
using Domain.Entities;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Services;

public class MapperService: IMapper
{
    public Student StudentVMToStudent(RegisterViewModel registerViewModel)
    {
        using var hmac = new HMACSHA512();
        return new Student 
        {
            FirstName = registerViewModel.FirstName,
            LastName = registerViewModel.LastName,
            Email = registerViewModel.Email,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerViewModel.Password)),
            PasswordSalt = hmac.Key,
            Year = registerViewModel.Year
        };
    }
    
    public Professor ProfessorVNToProfessor(RegisterViewModel registerViewModel)
    {
        using var hmac = new HMACSHA512();
        return new Professor 
        {
            FirstName = registerViewModel.FirstName,
            LastName = registerViewModel.LastName,
            Email = registerViewModel.Email,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerViewModel.Password)),
            PasswordSalt = hmac.Key,
            IsActivated = false
        };
    }
    public Manager ManagerVMToManager(RegisterViewModel registerViewModel)
    {
        using var hmac = new HMACSHA512();
        return new Manager 
        {
            FirstName = registerViewModel.FirstName,
            LastName = registerViewModel.LastName,
            Email = registerViewModel.Email,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerViewModel.Password)),
            PasswordSalt = hmac.Key,
            IsManager = true
        };
    }
}
