using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GestionAcademica.Models
{
    public class Group : Base
    {
        public Teacher Teacher { get; set; }
        public Cycle Cycle { get; set; }

        [DisplayName("Número Grupo")]
        public string Number { get; set; }

        [DisplayName("Horario Grupo")]
        public string Schedule { get; set; }
    }
}
