using GestionAcademica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAcademica.Web.ViewModels
{
    public class StudentVM
    {
        public Student Student { get; set; }
        public List<Student> StudentList { get; set; }
        public List<Career> CareersList { get; set; }
    }
}
