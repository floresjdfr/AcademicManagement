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
    public class GroupController : Controller
    {
        private readonly string groupUrl = "https://localhost:44367/api/group/";
        private readonly string teacherUrl = "https://localhost:44367/api/teacher/";
        private readonly HttpClient httpClient = new HttpClient();
        public static readonly string ControllerName = "Group";

        // GET: GroupController
        public async Task<ActionResult> Index()
        {
            var model = new GroupVM();
            try
            {
                var response = await httpClient.GetAsync(groupUrl);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                model.GroupList = JsonSerializer.Deserialize<List<Group>>(json);
            }
            catch
            {

            }
            return View(model);
        }

        // GET: GroupController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // POST: GroupController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Group group)
        {
            try
            {
                var jsonText = JsonSerializer.Serialize(group);
                var content = new StringContent(jsonText, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(groupUrl, content);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GroupController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var response = await httpClient.GetAsync(groupUrl + id.ToString());
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonSerializer.Deserialize<Group>(json);
            return PartialView("_Edit", model);
        }

        // POST: GroupController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Group group)
        {
            try
            {
                var jsonText = JsonSerializer.Serialize(group);
                var content = new StringContent(jsonText, Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync(groupUrl + id.ToString(), content);
                response.EnsureSuccessStatusCode();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: GroupController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var response = await httpClient.GetAsync(groupUrl + id.ToString());
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonSerializer.Deserialize<Group>(json);
            return PartialView("_Delete", model);
        }

        // POST: GroupController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var response = await httpClient.DeleteAsync(groupUrl + id.ToString());
                response.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<ActionResult> AssignTeacher(int id)
        {
            var model = new GroupVM();
            try
            {
                //Group
                var response = await httpClient.GetAsync(groupUrl + id.ToString());
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                model.Group = JsonSerializer.Deserialize<Group>(json);

                //Teachers List
                var teacherResponse = await httpClient.GetAsync(teacherUrl);
                teacherResponse.EnsureSuccessStatusCode();

                var teachersJson = await teacherResponse.Content.ReadAsStringAsync();
                model.TeacherList = JsonSerializer.Deserialize<List<Teacher>>(teachersJson);

                return View("_AssignTeacher", model);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AssignTeacher(int id, GroupVM model)
        {
            try
            {
                var jsonText = JsonSerializer.Serialize(model.Group);
                var content = new StringContent(jsonText, Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync(groupUrl+"UpdateTeacher/" + id.ToString(), content);
                response.EnsureSuccessStatusCode();

            }
            catch
            {
            }
            return Json(new { url = Url.Action("Index") });
        }
    }
}
