using System;
using System.Collections.Generic;
using System.Text;

namespace GestionAcademica.Models
{
    public class Cycle : Base
    {
        public CycleState CycleState { get; set; }
        public int Year { get; set; }
        public int Number { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
