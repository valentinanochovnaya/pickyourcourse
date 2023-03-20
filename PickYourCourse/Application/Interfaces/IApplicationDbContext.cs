using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Student> Students { get; set; }
         DbSet<Course> Courses { get; set; }
         DbSet<StudentCourseRelation> StudentCourses { get; set; }
         DbSet<Manager> Managers { get; set; }
         DbSet<Professor> Professors { get; set; }
         DbSet<Notification> Notifications { get; set; }
        Task<int> SaveChangesAsync();
    }
}
