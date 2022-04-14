﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GestionAcademica.Models
{
    public class Career : Base
    {
        [DisplayName("Código")]
        public string Code { get; set; }
        [DisplayName("Carrera")]
        public string CareerName { get; set; }
        [DisplayName("Título")]
        public string DegreeName { get; set; }
    }
}
