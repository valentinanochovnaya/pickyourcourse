using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Professor: BaseUser
    {
        public bool IsActivated { get; set; }
        public bool IsPending { get; set; }
        public IList<Course>? Courses { get; set; }
    }
}
