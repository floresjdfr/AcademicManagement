using GestionAcademica.Models;
using GestionAcademica.Web.Helpers;
using GestionAcademica.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GestionAcademica.Web.Controllers
{
    public class TeacherController : Controller
    {
        private readonly string teacherUrl = "https://localhost:44367/api/teacher/";
        private readonly string groupStudetsUrl = "https://localhost:44367/api/GroupStudents/";

        private readonly HttpClient httpClient = new HttpClient();

        // GET: TeacherController
        public async Task<ActionResult> Index()
        {
            var model = new TeacherVM();
            try
            {
                var response = await httpClient.GetAsync(teacherUrl);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                model.TeacherList = JsonSerializer.Deserialize<List<Teacher>>(json);
            }
            catch
            {

            }
            return View(model);
        }

        // GET: TeacherController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // POST: TeacherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Teacher teacher)
        {
            try
            {
                var jsonText = JsonSerializer.Serialize(teacher);
                var content = new StringContent(jsonText, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(teacherUrl, content);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TeacherController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var response = await httpClient.GetAsync(teacherUrl + id.ToString());
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonSerializer.Deserialize<Teacher>(json);
            return PartialView("_Edit", model);
        }

        // POST: TeacherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Teacher teacher)
        {
            try
            {
                var jsonText = JsonSerializer.Serialize(teacher);
                var content = new StringContent(jsonText, Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync(teacherUrl + id.ToString(), content);
                response.EnsureSuccessStatusCode();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: TeacherController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var response = await httpClient.GetAsync(teacherUrl + id.ToString());
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonSerializer.Deserialize<Teacher>(json);
            return PartialView("_Delete", model);
        }

        // POST: TeacherController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var response = await httpClient.DeleteAsync(teacherUrl + id.ToString());
                response.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<ActionResult> Courses()
        {
            //teacher logged in required
            HttpContext.Session.SetInt32("userID", 1);
            var userID = HttpContext.Session.GetInt32("userID");

            TeacherVM model = new TeacherVM();
            try
            {
                //Teacher
                var newTeacherUrl = teacherUrl + userID;
                var responseTeacher = await httpClient.GetAsync(newTeacherUrl);
                responseTeacher.EnsureSuccessStatusCode();
                var jsonTeacher = await responseTeacher.Content.ReadAsStringAsync();
                model.Teacher = JsonSerializer.Deserialize<Teacher>(jsonTeacher);

                //Courses
                var teacherCoursesUrl = teacherUrl + "GetTeacherCourses/" + userID;
                var responseCourses = await httpClient.GetAsync(teacherCoursesUrl);
                responseCourses.EnsureSuccessStatusCode();
                var jsonCourses = await responseCourses.Content.ReadAsStringAsync();
                model.CoursesList = JsonSerializer.Deserialize<List<Course>>(jsonCourses);
            }
            catch
            {

            }
            return View(model);
        }

        public async Task<ActionResult> Groups(int courseID)
        {
            TeacherVM model = new TeacherVM();
            //Get teacher Logged in
            var userID = HttpContext.Session.GetInt32("userID");

            try
            {
                //Teacher
                var newTeacherUrl = teacherUrl + userID;
                var responseTeacher = await httpClient.GetAsync(newTeacherUrl);
                responseTeacher.EnsureSuccessStatusCode();
                var jsonTeacher = await responseTeacher.Content.ReadAsStringAsync();
                model.Teacher = JsonSerializer.Deserialize<Teacher>(jsonTeacher);

                //Groups
                var teacherGroupsUrl = teacherUrl + "GetTeacherGroups/" + userID + "/" + courseID;
                var responseGroups = await httpClient.GetAsync(teacherGroupsUrl);
                responseGroups.EnsureSuccessStatusCode();
                var jsonGroups = await responseGroups.Content.ReadAsStringAsync();
                model.GroupsList = JsonSerializer.Deserialize<List<Group>>(jsonGroups);
            }
            catch (Exception ex)
            {


            }
            return View(model);
        }

        public async Task<ActionResult> Students(int groupID)
        {
            TeacherVM model = new TeacherVM();
            //Get teacher Logged in
            var userID = HttpContext.Session.GetInt32("userID");

            try
            {
                //Teacher
                var newTeacherUrl = teacherUrl + userID;
                var responseTeacher = await httpClient.GetAsync(newTeacherUrl);
                responseTeacher.EnsureSuccessStatusCode();
                var jsonTeacher = await responseTeacher.Content.ReadAsStringAsync();
                model.Teacher = JsonSerializer.Deserialize<Teacher>(jsonTeacher);

                //GroupStudents
                var studentsUrl = groupStudetsUrl + "GetByGroup/" + groupID;
                var responseStudents = await httpClient.GetAsync(studentsUrl);
                responseStudents.EnsureSuccessStatusCode();
                var jsonStudents = await responseStudents.Content.ReadAsStringAsync();
                model.GroupStudentsList = JsonSerializer.Deserialize<List<GroupStudents>>(jsonStudents);
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        public async Task<ActionResult> EditScore(int groupStudentID)
        {
            TeacherVM model = new TeacherVM();
            //Get teacher Logged in
            var userID = HttpContext.Session.GetInt32("userID");

            try
            {
                //GroupStudent
                var studentsUrl = groupStudetsUrl + groupStudentID;
                var responseStudents = await httpClient.GetAsync(studentsUrl);
                responseStudents.EnsureSuccessStatusCode();
                var jsonStudents = await responseStudents.Content.ReadAsStringAsync();
                model.GroupStudent = JsonSerializer.Deserialize<List<GroupStudents>>(jsonStudents).FirstOrDefault();
            }
            catch (Exception ex)
            {

            }
            return PartialView("_EditScore", model.GroupStudent);
        }

        [HttpPost]
        public async Task<ActionResult> EditScore(GroupStudents model)
        {
            //Get teacher Logged in
            var userID = HttpContext.Session.GetInt32("userID");

            try
            {
                //GroupStudent
                var studentsUrl = groupStudetsUrl + model.ID;

                var groupStudent = JsonSerializer.Serialize(model);
                var content = new StringContent(groupStudent, Encoding.UTF8, "application/json");
                var responseGroupStudent = await httpClient.PutAsync(studentsUrl, content);
                responseGroupStudent.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {

            }
            return Json(new { url = Url.Action("Students", new { groupID = model.Group.ID }) });
        }
    }
}
