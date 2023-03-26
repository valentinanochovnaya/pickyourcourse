
using System.Security.Cryptography;
using System.Text;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;

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

    public String GetRole(String email, String password)
    {
        
        BaseUser? user = _context.Students.SingleOrDefault(student => student.Email == email);
        String Role = "Student";

        if (user == null)
        {
            user = _context.Professors.SingleOrDefault(professor => professor.Email == email);
            Role = "Professor";
        }
        if (user == null)
        {
            Role = "Manager";
            user = _context.Managers.SingleOrDefault(professor => professor.Email == email);
        }

        if (user != null && password!="")
        {
            var hmac = new HMACSHA512(user.PasswordSalt);
            if (!user.PasswordHash.SequenceEqual(hmac.ComputeHash(Encoding.UTF8.GetBytes(password))))
            {
                Role = "";
            }
        }
        return Role;
    }
}
