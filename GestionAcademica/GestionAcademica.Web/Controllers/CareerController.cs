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
    public class CareerController : Controller
    {
        private string url = "https://localhost:44367/api/career/";
        private HttpClient httpClient = new HttpClient();
        // GET: CareerController
        public async Task<ActionResult> Index()
        {
            CareerVM model = new CareerVM();
            try
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                model.ListCareer = JsonSerializer.Deserialize<List<Career>>(json);
                return View(model);
            }
            catch
            {

            }
            return View(model);
        }

        // GET: CareerController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var response = await httpClient.GetAsync(url + id.ToString());
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var model = JsonSerializer.Deserialize<Career>(json);
                return View(model);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: CareerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CareerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Career career)
        {
            try
            {
                var jsonText = JsonSerializer.Serialize(career);
                var content = new StringContent(jsonText, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(url, content);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CareerController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var response = await httpClient.GetAsync(url + id.ToString());
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonSerializer.Deserialize<Career>(json);
            return View(model);
        }

        // POST: CareerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Career career)
        {
            try
            {
                var jsonText = JsonSerializer.Serialize(career);
                var content = new StringContent(jsonText, Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync(url + id.ToString(), content);
                response.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(career);
            }
        }

        // GET: CareerController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var response = await httpClient.GetAsync(url + id.ToString());
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonSerializer.Deserialize<Career>(json);
            return View(model);
        }

        // POST: CareerController/Delete/5
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
                return RedirectToAction("Index");
            }
        }
    }
}
