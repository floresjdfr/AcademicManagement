using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestionAcademica.Models
{
    public class User : Base
    {
        public UserType UserType { get; set; }
        
        [DisplayName("Cédula")]
        public string UserID { get; set; }

        [DisplayName("Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
