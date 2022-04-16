using System;
using System.Collections.Generic;
using System.Text;

namespace GestionAcademica.Models
{
    public class Teacher : Base
    {
        public User User { get; set; }
        public string IdIdentidad { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
