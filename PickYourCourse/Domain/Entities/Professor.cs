using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    internal class Professor: BaseUser
    {
        public bool IsActivated { get; set; }
        public List<Course> Courses { get; set; }
    }
}
