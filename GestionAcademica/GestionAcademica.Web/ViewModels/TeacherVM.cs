using GestionAcademica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAcademica.Web.ViewModels
{
    public class TeacherVM
    {
        public List<Teacher> TeacherList { get; set; }
        public Teacher Teacher { get; set; }
        public List<Course> CoursesList { get; set; }
        public Course Course { get; set; }
        public List<GroupStudents> GroupStudentsList { get; set; }
        public GroupStudents GroupStudent { get; set; }
        public List<Group> GroupsList { get; set; }
        public Group Group { get; set; }
    }
}
