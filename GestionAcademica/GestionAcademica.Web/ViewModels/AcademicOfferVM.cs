using GestionAcademica.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAcademica.Web.ViewModels
{
    public class AcademicOfferVM
    {
        public Career Career { get; set; }
        public List<Career> CareerList { get; set; }
        public Cycle Cycle { get; set; }
        public List<Cycle> CyclesList { get; set; }
        public Group Group { get; set; }
        public List<Group> GroupsList { get; set; }
        public CareerCourses CareerCourse { get; set; }
        public List<CareerCourses> CareerCoursesList { get; set; }
        public CourseGroups CourseGroup { get; set; }
        public List<CourseGroups> CourseGroupsList { get; set; }
        public List<SelectListItem> SelectList { get; set; }
        public List<Teacher> TeachersList { get; set; }

    }
}
