﻿using GestionAcademica.Models;
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
    public class CareerController : Controller
    {
        private readonly string careerUrl = "https://localhost:44367/api/career/";
        private readonly string careerCoursesUrl = "https://localhost:44367/api/CareerCourses/";
        private readonly HttpClient httpClient = new HttpClient();
        public static readonly string ControllerName = "Career";

        // GET: CareerController
        public async Task<ActionResult> Index()
        {
            CareerVM model = new CareerVM();
            try
            {
                if (RolesHelper.IsAuthorized(HttpContext, new ERole[] { ERole.ADMINISTRADOR }))
                {
                    var response = await httpClient.GetAsync(careerUrl);
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    model.ListCareer = JsonSerializer.Deserialize<List<Career>>(json);
                    HttpContext.Session.SetObjectAsJson("ListCareer", model.ListCareer);
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { error = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Filter(CareerVM model)
        {
            try
            {
                model.ListCareer = HttpContext.Session.GetObjectFromJson<List<Career>>("ListCareer");
                if (model.ListCareer == null) throw new Exception();

                if (!string.IsNullOrEmpty(model.Career.Code))
                    model.ListCareer = model.ListCareer.Where(item => item.Code.Contains(model.Career.Code, StringComparison.OrdinalIgnoreCase)).ToList();
                if (!string.IsNullOrEmpty(model.Career.CareerName))
                    model.ListCareer = model.ListCareer.Where(item => item.CareerName.Contains(model.Career.CareerName, StringComparison.OrdinalIgnoreCase)).ToList();

                return Json(new { isValid = true, html = RazorHelper.RenderRazorViewToString(this, "_CareerListTable", model) });
            }
            catch
            {
                return Json(new { isValid = false, url = Url.Action("Index") });
            }
        }

        // GET: CareerController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                if (RolesHelper.IsAuthorized(HttpContext, new ERole[] { ERole.ADMINISTRADOR }))
                {
                    CareerVM model = new CareerVM();
                    var responseCareer = await httpClient.GetAsync(careerUrl + id.ToString());
                    var url = careerCoursesUrl + "GetByCareer/" + id.ToString();
                    var responseCareerCourses = await httpClient.GetAsync(url);

                    responseCareer.EnsureSuccessStatusCode();
                    responseCareerCourses.EnsureSuccessStatusCode();

                    var jsonCareer = await responseCareer.Content.ReadAsStringAsync();
                    model.Career = JsonSerializer.Deserialize<Career>(jsonCareer);

                    var jsonCareerCourses = await responseCareerCourses.Content.ReadAsStringAsync();
                    model.ListCareerCourses = JsonSerializer.Deserialize<List<CareerCourses>>(jsonCareerCourses);

                    HttpContext.Session.SetObjectAsJson("ListCareerCourses", model.ListCareerCourses);

                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { error = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FilterGroups(CareerVM model)
        {
            try
            {
                model.ListCareerCourses = HttpContext.Session.GetObjectFromJson<List<CareerCourses>>("ListCareerCourses");
                if (model.ListCareerCourses == null) throw new Exception();

                if (!string.IsNullOrEmpty(model.Course.Code))
                    model.ListCareerCourses = model.ListCareerCourses.Where(item => item.Course.Code.Contains(model.Course.Code, StringComparison.OrdinalIgnoreCase)).ToList();
                if (!string.IsNullOrEmpty(model.Course.Name))
                    model.ListCareerCourses = model.ListCareerCourses.Where(item => item.Course.Name.Contains(model.Course.Name, StringComparison.OrdinalIgnoreCase)).ToList();

                return Json(new { isValid = true, html = RazorHelper.RenderRazorViewToString(this, "_CareerCoursesList", model) });
            }
            catch
            {
                return Json(new { isValid = false, url = Url.Action("Index") });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Career career)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var jsonText = JsonSerializer.Serialize(career);
                    var content = new StringContent(jsonText, Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync(careerUrl, content);
                    return RedirectToAction(nameof(Index));
                }
                else
                    return View(career);
            }
            catch
            {
                return View();
            }
        }

        // GET: CareerController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (RolesHelper.IsAuthorized(HttpContext, new ERole[] { ERole.ADMINISTRADOR }))
            {
                var model = new CareerVM();

                var response = await httpClient.GetAsync(careerUrl + id.ToString());
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                model.Career = JsonSerializer.Deserialize<Career>(json);
                return PartialView("_Edit", model);
            }
            else
            {
                return Json(new { url = Url.Action("Index", "Login") });
            }
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
                var response = await httpClient.PutAsync(careerUrl + id.ToString(), content);
                response.EnsureSuccessStatusCode();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: CareerController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (RolesHelper.IsAuthorized(HttpContext, new ERole[] { ERole.ADMINISTRADOR }))
            {
                CareerVM model = new CareerVM();
                var response = await httpClient.GetAsync(careerUrl + id.ToString());
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                model.Career = JsonSerializer.Deserialize<Career>(json);
                return PartialView("_Delete", model);
            }
            else
            {
                return Json(new { url = Url.Action("Index", "Login") });
            }
        }

        // POST: CareerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var response = await httpClient.DeleteAsync(careerUrl + id.ToString());
                response.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddNewCourse(CareerVM model)
        {
            HttpResponseMessage response = null;
            try
            {
                model.CareerCourse.Career = model.Career;
                var careerCourseJson = JsonSerializer.Serialize(model.CareerCourse);
                var requestContent = new StringContent(careerCourseJson, Encoding.UTF8, "application/json");
                response = await httpClient.PostAsync(careerCoursesUrl, requestContent);
                response.EnsureSuccessStatusCode();
                return RedirectToAction("Details", new { id = model.CareerCourse.Career.ID });
            }
            catch
            {
                var jsonError = await response.Content.ReadAsStringAsync();
                var error = JsonSerializer.Deserialize<Error>(jsonError);
                TempData["Error"] = error.ErrorMessage;
                return RedirectToAction("Details", new { id = model.CareerCourse.Career.ID });
            }
        }
    }
}
