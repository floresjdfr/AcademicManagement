using GestionAcademica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAcademica.Web.ViewModels
{
    public class StudentVM
    {
        public Student Student { get; set; }
        public List<Student> StudentList { get; set; }
        public List<Career> CareersList { get; set; }
        public List<GroupStudents> GroupStudentsList { get; set; }
        public GroupStudents GroupStudent { get; set; }
        public List<CourseGroups> CourseGroupsList { get; set; }
        public CourseGroups CourseGroup { get; set; }
        public Dictionary<Course,List<CourseGroups>> CourseGroupsDict { get; set; }
        public Cycle Cycle { get; set; }
        public List<Cycle> CyclesList { get; set; }
    }
}
