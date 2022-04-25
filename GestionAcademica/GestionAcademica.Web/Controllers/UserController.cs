using GestionAcademica.Models;
using GestionAcademica.Web.Helpers;
using GestionAcademica.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GestionAcademica.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly string userUrl = "https://localhost:44367/api/user/";
        private readonly string userTypeUrl = "https://localhost:44367/api/UserType/";
        private readonly HttpClient httpClient = new HttpClient();

        // GET: UserController
        public async Task<ActionResult> Index()
        {
            var model = new UserVM();
            try
            {
                var responseUsers = await httpClient.GetAsync(userUrl);
                responseUsers.EnsureSuccessStatusCode();

                var responseUserTypes = await httpClient.GetAsync(userTypeUrl);
                responseUserTypes.EnsureSuccessStatusCode();

                var jsonUsers = await responseUsers.Content.ReadAsStringAsync();
                var jsonUserTypes = await responseUserTypes.Content.ReadAsStringAsync();

                model.UserList = JsonSerializer.Deserialize<List<User>>(jsonUsers);
                model.UserTypesList = JsonSerializer.Deserialize<List<UserType>>(jsonUserTypes);

            }
            catch
            {

            }
            return View(model);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(User user)
        {
            try
            {
                var jsonText = JsonSerializer.Serialize(user);
                var content = new StringContent(jsonText, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(userUrl, content);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public async Task<ActionResult> Edit(int id, int type)
        {
            UserVM model = new UserVM();

            try
            {
                var responseUser = await httpClient.GetAsync(userUrl+id.ToString());
                responseUser.EnsureSuccessStatusCode();

                var responseUserTypes = await httpClient.GetAsync(userTypeUrl);
                responseUserTypes.EnsureSuccessStatusCode();

                var jsonUser = await responseUser.Content.ReadAsStringAsync();
                var jsonUserTypes = await responseUserTypes.Content.ReadAsStringAsync();

                model.User = JsonSerializer.Deserialize<User>(jsonUser);
                model.UserTypesList = JsonSerializer.Deserialize<List<UserType>>(jsonUserTypes);

            }
            catch
            {

            }
            return type == 0 ? PartialView("_Edit", model) : PartialView("_EditPassword", model);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, User user)
        {
            try
            {
                var jsonText = JsonSerializer.Serialize(user);
                var content = new StringContent(jsonText, Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync(userUrl + id.ToString(), content);
                response.EnsureSuccessStatusCode();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: UserController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var response = await httpClient.GetAsync(userUrl + id.ToString());
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonSerializer.Deserialize<User>(json);
            return PartialView("_Delete", model);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var response = await httpClient.DeleteAsync(userUrl + id.ToString());
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
