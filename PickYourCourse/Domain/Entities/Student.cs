using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Student: BaseUser
    {
        public int Year { get; set; }
        public int Score { get; set; }
        public int CoursesNumber { get; set; }
        public int MyProperty { get; set; }
    }
}
