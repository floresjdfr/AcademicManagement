using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GestionAcademica.Models
{
    public class UserType : Base
    {
        [DisplayName("Tipo Usuario")]
        public string TypeDescription { get; set; }
    }
}
