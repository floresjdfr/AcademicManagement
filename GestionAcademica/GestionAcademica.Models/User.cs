using System;
using System.Collections.Generic;
using System.Text;

namespace GestionAcademica.Models
{
    public class User
    {
        public UserType UserType { get; set; }
        public string ID { get; set; }
        public string Password { get; set; }
    }
}
