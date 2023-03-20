using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DataContext
{
    public class DataContext : DbContext, IApplicationDbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasOne<Professor>(c => c.Professor)
                .WithMany(p => p.Courses)
                .HasForeignKey(c => c.ProfessorId);
            modelBuilder.Entity<Notification>()
                .HasOne<Student>(n => n.Student)
                .WithMany(s => s.Notifications)
                .HasForeignKey(n => n.StudentId);
            modelBuilder.Entity<StudentCourseRelation>().HasKey(sc => new { sc.StudentId, sc.CourseId });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourseRelation> StudentCourses { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Notification> Notifications { get; set; }





        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
