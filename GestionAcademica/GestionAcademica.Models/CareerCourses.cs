using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GestionAcademica.Models
{
    public class CareerCourses : Base
    {
        public Course Course { get; set; }
        public Career Career { get; set; }
        
        [DisplayName("Año Carrera")]
        public int Year { get; set; }

        [DisplayName("Ciclo")]
        public int Cycle { get; set; }
    }
}
