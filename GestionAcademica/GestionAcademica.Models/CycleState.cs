using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GestionAcademica.Models
{
    public class CycleState : Base
    {
        [DisplayName("Estado")]
        public string StateDescription { get; set; }
    }
}
