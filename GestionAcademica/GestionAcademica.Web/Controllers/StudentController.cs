using GestionAcademica.Models;
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
    public class StudentController : Controller
    {
        private readonly string StudentUrl = "https://localhost:44367/api/Student/";
        private readonly string careerUrl = "https://localhost:44367/api/career/";
        private readonly HttpClient httpClient = new HttpClient();

        // GET: StudentController
        public async Task<ActionResult> Index()
        {
            var model = new StudentVM();
            try
            {
                //Students
                var responseStudents = await httpClient.GetAsync(StudentUrl);
                responseStudents.EnsureSuccessStatusCode();

                var jsonStudents = await responseStudents.Content.ReadAsStringAsync();
                model.StudentList = JsonSerializer.Deserialize<List<Student>>(jsonStudents);

                //Careers
                var responseCareers = await httpClient.GetAsync(careerUrl);
                responseCareers.EnsureSuccessStatusCode();

                var jsonCareers = await responseCareers.Content.ReadAsStringAsync();
                model.CareersList = JsonSerializer.Deserialize<List<Career>>(jsonCareers);
            }
            catch
            {

            }
            return View(model);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Student Student)
        {
            try
            {
                var jsonText = JsonSerializer.Serialize(Student);
                var content = new StringContent(jsonText, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(StudentUrl, content);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Student model = new Student();
            try
            {
                var responseStudent = await httpClient.GetAsync(StudentUrl + id.ToString());
                responseStudent.EnsureSuccessStatusCode();

                var jsonStudent = await responseStudent.Content.ReadAsStringAsync();
                model = JsonSerializer.Deserialize<Student>(jsonStudent);
            }
            catch
            {

            }
            return PartialView("_Edit", model);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Student Student)
        {
            try
            {
                var jsonText = JsonSerializer.Serialize(Student);
                var content = new StringContent(jsonText, Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync(StudentUrl + id.ToString(), content);
                response.EnsureSuccessStatusCode();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: StudentController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var response = await httpClient.GetAsync(StudentUrl + id.ToString());
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonSerializer.Deserialize<Student>(json);
            return PartialView("_Delete", model);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var response = await httpClient.DeleteAsync(StudentUrl + id.ToString());
                response.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
