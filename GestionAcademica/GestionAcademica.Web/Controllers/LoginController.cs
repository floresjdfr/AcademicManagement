using GestionAcademica.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


using GestionAcademica.Web.Helpers;
using GestionAcademica.Web.ViewModels;


namespace GestionAcademica.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly string loginUrl = "https://localhost:44367/api/User/Login";
        private HttpClient httpClient = new HttpClient();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(User model)
        {
            try
            {
                var jsonUser = JsonSerializer.Serialize(model);
                var content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(loginUrl, content);
                response.EnsureSuccessStatusCode();

                var responseJson = await response.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<User>(responseJson);

                HttpContext.Session.SetObjectAsJson("User", user);

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("User");

            return RedirectToAction("Index", "Home");
        }
    }
}
