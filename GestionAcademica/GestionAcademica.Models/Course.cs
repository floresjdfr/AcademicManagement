﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GestionAcademica.Models
{
    public class Course : Base
    {
        [DisplayName("Código Curso")]
        public string Code { get; set; }
        
        [DisplayName("Nombre Curso")]
        public string Name { get; set; }
        
        [DisplayName("Creditos")]
        public int Credits { get; set; }
        
        [DisplayName("Horas Semanales")]
        public int WeeklyHours { get; set; }
    }
}
