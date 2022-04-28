using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionAcademica.Models;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace GestionAcademica.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly string url = "https://localhost:44367/api/course/";
        private readonly HttpClient httpClient = new HttpClient();
        public static readonly string ControllerName = "Course";

        public Encoding Enconding { get; private set; }

        // GET: CourseController
        public async Task<ActionResult> Index()
        {
            try
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var model = JsonSerializer.Deserialize<List<Course>>(json);
                return View(model);
            }
            catch
            {
                return RedirectToAction("Index", "Home", new { });
            }
        }

        // GET: CourseController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var response = await httpClient.GetAsync(url + id.ToString());
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var model = JsonSerializer.Deserialize<Course>(json);
                return View(model);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: CourseController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course course)
        {
            try
            {
                var modelJson = JsonSerializer.Serialize(course);
                var content = new StringContent(modelJson, Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(url, content);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CourseController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var response = await httpClient.GetAsync(url + id.ToString());
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                var model = JsonSerializer.Deserialize<Course>(json);
                return View(model);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Course course)
        {
            try
            {
                var jsonText = JsonSerializer.Serialize(course);
                var content = new StringContent(jsonText, Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync(url + id.ToString(), content);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CourseController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var response = await httpClient.GetAsync(url + id.ToString());
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var model = JsonSerializer.Deserialize<Course>(json);
                return View(model);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // POST: CourseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var response = await httpClient.DeleteAsync(url + id.ToString());
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
