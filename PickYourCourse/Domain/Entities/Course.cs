using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int MinScore { get; set; }
        public int MaxStudentsNumber { get; set; }
        public int Year { get; set; }
        public int Term { get; set; }
        public int ProfessorId { get; set; }
        public Professor? Professor { get; set; }
        public IList<StudentCourseRelation>? StudentCourses { get; set; }
    }
}
