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

    public enum EnumUserType
    {
        Administrador = 1,
        Matriculador = 2,
        Profesor = 3,
        Alumno = 4
    }
}
