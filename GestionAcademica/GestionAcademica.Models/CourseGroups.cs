using System;
using System.Collections.Generic;
using System.Text;

namespace GestionAcademica.Models
{
    public class CourseGroups : Base
    {
        public Course Course { get; set; }
        public Group Group { get; set; }
    }
}
