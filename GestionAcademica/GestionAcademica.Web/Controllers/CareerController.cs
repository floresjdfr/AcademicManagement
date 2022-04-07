
using GestionAcademica.Web.Models;
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

        private HttpClient httpClient = new HttpClient();
        private string url = "https://localhost:44367/api/Career/";

        // GET: CareerController
        public async Task<ActionResult> Index()
        {
            List<Career> model = new List<Career>();
            try
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                model = JsonSerializer.Deserialize<List<Career>>(json);
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CareerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CareerController/Delete/5
        public async Task<ActionResult> Delete(int id)
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

        // POST: CareerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var response = await httpClient.DeleteAsync(url + id.ToString());
                response.EnsureSuccessStatusCode();


            }
            catch
            {

            }
            return RedirectToAction("Index");
        }
    }
}
