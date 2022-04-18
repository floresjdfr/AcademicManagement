using System;
using System.Collections.Generic;
using System.Text;

namespace GestionAcademica.Models
{
    public class Student : Base
    {
        public Career Career { get; set; }
        public User User { get; set; }
        public string IdStudent { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
