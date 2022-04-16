using GestionAcademica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAcademica.Web.ViewModels
{
    public class UserVM
    {
        public User User { get; set; }
        public List<User> UserList { get; set; }
        public List<UserType> UserTypesList { get; set; }

    }
}
