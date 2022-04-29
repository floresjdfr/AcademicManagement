using GestionAcademica.Models;
using GestionAcademica.Web.Helpers;
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
    public class CareerCoursesController : Controller
    {
        private static readonly string urlCareerCourses = "https://localhost:44367/api/CareerCourses/";
        private static readonly HttpClient httpClient = new HttpClient();
        public static readonly string ControllerName = "CareerCourse";


        // GET: CareerController
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Career");
        }
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                if (RolesHelper.IsAuthorized(HttpContext, new ERole[] { ERole.ADMINISTRADOR }))
                {
                    var response = await httpClient.GetAsync(urlCareerCourses + id.ToString());
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    var model = JsonSerializer.Deserialize<CareerCourses>(json);

                    return PartialView("_Edit", model);
                }
                else
                {
                    return Json(new { url = Url.Action("Index", "Login") });
                }
            }
            catch (Exception ex)
            {
                return Json(new { url = Url.Action("Error", "Home", new { error = ex.Message }) });
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Edit(CareerCourses careerCourse)
        {
            try
            {
                var modelString = JsonSerializer.Serialize(careerCourse);
                var content = new StringContent(modelString, Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync(urlCareerCourses + careerCourse.ID.ToString(), content);
                response.EnsureSuccessStatusCode();
            }
            catch
            {

            }
            return RedirectToAction("Details", "Career", new { id = careerCourse.Career.ID });
        }

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (RolesHelper.IsAuthorized(HttpContext, new ERole[] { ERole.ADMINISTRADOR }))
                {
                    var response = await httpClient.GetAsync(urlCareerCourses + id.ToString());
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    var model = JsonSerializer.Deserialize<CareerCourses>(json);

                    return PartialView("_Delete", model);
                }
                else
                {
                    return Json(new { url = Url.Action("Index", "Login") });
                }
            }
            catch (Exception ex)
            {
                return Json(new { url = Url.Action("Error", "Home", new { error = ex.Message }) });
            }
        }

        // POST: CareerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var careerID = Convert.ToInt32(collection["Career.ID"]);
                var response = await httpClient.DeleteAsync(urlCareerCourses + id.ToString());
                response.EnsureSuccessStatusCode();
                return RedirectToAction("Details", "Career", new { id = careerID });
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
