using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GestionAcademica.Models
{
    public class Teacher : Base
    {
        public User User { get; set; }
        
        [DisplayName("Cédula")]
        public string IdIdentidad { get; set; }

        [DisplayName("Nombre")]
        public string Name { get; set; }

        [DisplayName("Número de Teléfono")]
        public string PhoneNumber { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }
    }
}
