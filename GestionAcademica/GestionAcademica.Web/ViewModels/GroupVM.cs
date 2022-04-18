using GestionAcademica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAcademica.Web.ViewModels
{
    public class GroupVM
    {
        public Group Group { get; set; }
        public List<Group> GroupList { get; set; }

        public Teacher Teacher { get; set; }
        public List<Teacher> TeacherList { get; set; }
    }
}
