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
    }
}
