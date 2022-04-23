using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestionAcademica.Models
{
    public class Student : Base
    {
        public Career Career { get; set; }
        
        public User User { get; set; }
        
        [DisplayName("Cédula")]
        public string IdStudent { get; set; }
        
        [DisplayName("Nombre")]
        public string Name { get; set; }

        [DisplayName("Numero de Teléfono")]
        public string PhoneNumber { get; set; }

        [DisplayName("Correo Electrónico")]
        public string Email { get; set; }

        [DisplayName("Fecha Nacimiento")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
