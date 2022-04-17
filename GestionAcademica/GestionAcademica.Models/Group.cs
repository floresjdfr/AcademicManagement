using System;
using System.Collections.Generic;
using System.Text;

namespace GestionAcademica.Models
{
    public class Group : Base
    {
        public Teacher Teacher { get; set; }
        public Cycle Cycle { get; set; }
        public string Number { get; set; }
        public string Schedule { get; set; }
    }
}
