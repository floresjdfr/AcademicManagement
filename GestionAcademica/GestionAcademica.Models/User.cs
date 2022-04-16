using System;
using System.Collections.Generic;
using System.Text;

namespace GestionAcademica.Models
{
    public class User : Base
    {
        public UserType UserType { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
    }
}
