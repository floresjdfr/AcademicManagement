using System;
using System.Collections.Generic;
using System.Text;

namespace GestionAcademica.Models
{
    public class CareerCourses : Base
    {
        public Course Course { get; set; }
        public Career Career { get; set; }
        public int Year { get; set; }
        public int Cycle { get; set; }
    }
}
