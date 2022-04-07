using System;
using System.Collections.Generic;
using System.Text;

namespace GestionAcademica.Models
{
    public class Course : Base
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public int WeeklyHours { get; set; }
    }
}
