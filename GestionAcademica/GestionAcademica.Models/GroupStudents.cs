using System;
using System.Collections.Generic;
using System.Text;

namespace GestionAcademica.Models
{
    public class GroupStudents : Base
    {
        public Group Group { get; set; }
        public Student Student { get; set; }
        public double Score { get; set; }
    }
}
