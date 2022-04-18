using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GestionAcademica.Models
{
    public class CycleState : Base
    {
        [DisplayName("Estado Ciclo")]
        public string StateDescription { get; set; }
    }
}
