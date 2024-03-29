﻿using GestionAcademica.Models;
using GestionAcademica.Web.Helpers;
using GestionAcademica.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GestionAcademica.Web.Controllers
{
    public class AcademicOfferController : Controller
    {
        private List<SelectListItem> cyclesList = new List<SelectListItem>
        {
            new SelectListItem{Value = 1.ToString(), Text="Primer Ciclo"},
            new SelectListItem{Value = 2.ToString(), Text="Segundo Ciclo"}
        };
        private readonly string careerCoursesUrl = "https://localhost:44367/api/CareerCourses/";
        private readonly string courseGroupsUrl = "https://localhost:44367/api/CourseGroups/";
        private readonly string careersUrl = "https://localhost:44367/api/career/";
        private readonly string teacherUrl = "https://localhost:44367/api/teacher/";
        private readonly string courseUrl = "https://localhost:44367/api/course/";
        private readonly string groupUrl = "https://localhost:44367/api/group/";
        private readonly HttpClient httpClient = new HttpClient();
        public static readonly string ControllerName = "AcademicOffer";

        public async Task<IActionResult> Index()
        {
            var model = new AcademicOfferVM();
            try
            {
                if (RolesHelper.IsAuthorized(HttpContext, new ERole[] { ERole.ADMINISTRADOR }))
                {
                    var ciclesList = new List<SelectListItem>
                {
                    new SelectListItem{Value = "1", Text = "Primer Ciclo"},
                    new SelectListItem{Value = "2", Text = "Segundo Ciclo"},
                };

                    var responseCareers = await httpClient.GetAsync(careersUrl);
                    responseCareers.EnsureSuccessStatusCode();

                    var jsonCareers = await responseCareers.Content.ReadAsStringAsync();
                    model.CareerList = JsonSerializer.Deserialize<List<Career>>(jsonCareers);
                    model.SelectList = ciclesList;

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

        // POST: GroupController/Courses
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Courses(AcademicOfferVM model)
        {
            model.CareerCoursesList = new List<CareerCourses>();
            try
            {
                //CareerCourses
                var url = careerCoursesUrl + "GetByCareerAndCycle/" + model.CareerCourse.Career.ID + "/" + model.CareerCourse.Cycle;
                var responseCareerCourses = await httpClient.GetAsync(url);
                responseCareerCourses.EnsureSuccessStatusCode();
                var json = await responseCareerCourses.Content.ReadAsStringAsync();
                model.CareerCoursesList = JsonSerializer.Deserialize<List<CareerCourses>>(json);


            }
            catch
            {

            }
            return PartialView("_CoursesList", model);
        }
        public async Task<ActionResult> CourseGroups(int id)
        {
            var model = new AcademicOfferVM();
            try
            {
                if (RolesHelper.IsAuthorized(HttpContext, new ERole[] { ERole.ADMINISTRADOR }))
                {
                    TempData["CourseID"] = id;

                    //Course
                    var newCourseUrl = courseUrl + id;
                    var responseCourse = await httpClient.GetAsync(newCourseUrl);
                    responseCourse.EnsureSuccessStatusCode();
                    var courseJson = await responseCourse.Content.ReadAsStringAsync();
                    model.CourseGroup = new CourseGroups { Course = JsonSerializer.Deserialize<Course>(courseJson) };

                    //CourseGroups
                    var url = courseGroupsUrl + "GetByCourse/" + id;
                    var response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    model.CourseGroupsList = JsonSerializer.Deserialize<List<CourseGroups>>(json);

                    //Teachers
                    var responseTeachers = await httpClient.GetAsync(teacherUrl);
                    responseTeachers.EnsureSuccessStatusCode();
                    var jsonTeachers = await responseTeachers.Content.ReadAsStringAsync();
                    model.TeachersList = JsonSerializer.Deserialize<List<Teacher>>(jsonTeachers);

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
        public async Task<ActionResult> EditCourseGroup(int id)
        {
            var model = new AcademicOfferVM();
            try
            {
                if (RolesHelper.IsAuthorized(HttpContext, new ERole[] { ERole.ADMINISTRADOR }))
                {
                    //Group
                    var newGroupUrl = groupUrl + id;
                    var responseGroup = await httpClient.GetAsync(newGroupUrl);
                    responseGroup.EnsureSuccessStatusCode();
                    var groupJson = await responseGroup.Content.ReadAsStringAsync();
                    model.Group = JsonSerializer.Deserialize<Group>(groupJson);

                    //Teachers
                    var responseTeachers = await httpClient.GetAsync(teacherUrl);
                    responseTeachers.EnsureSuccessStatusCode();
                    var jsonTeachers = await responseTeachers.Content.ReadAsStringAsync();
                    model.TeachersList = JsonSerializer.Deserialize<List<Teacher>>(jsonTeachers);

                    return PartialView("_EditGroup", model);
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

        // POST: GroupController/EditCourseGroup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCourseGroup(AcademicOfferVM model)
        {
            try
            {
                //Group
                var newGroupUrl = groupUrl + model.Group.ID;
                var jsonText = JsonSerializer.Serialize(model.Group);
                var content = new StringContent(jsonText, Encoding.UTF8, "application/json");
                var responseGroup = await httpClient.PutAsync(newGroupUrl, content);
            }
            catch
            {

            }
            var courseID = TempData["CourseID"] as int?;
            return RedirectToAction("CourseGroups", new { id = courseID });
        }

        // POST: GroupController/CreateCourseGroup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCourseGroup(AcademicOfferVM model)
        {
            HttpResponseMessage response = null;
            var courseID = TempData["CourseID"] as int?;
            TempData["CourseID"] = courseID;

            if (ModelState.IsValid)
            {
                try
                {
                    var jsonText = JsonSerializer.Serialize(model.CourseGroup);
                    var content = new StringContent(jsonText, Encoding.UTF8, "application/json");
                    response = await httpClient.PostAsync(courseGroupsUrl, content);
                    response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException)
                {
                    var jsonError = await response.Content.ReadAsStringAsync();
                    var error = JsonSerializer.Deserialize<Error>(jsonError);
                    TempData["Error"] = error.ErrorMessage;
                }
                catch
                {
                    TempData["Error"] = "An unexpected error has ocurred.";
                }
                return Json(new { isValid = true, url = Url.Action("CourseGroups", new { id = courseID }) });
            }
            //Teachers
            var responseTeachers = await httpClient.GetAsync(teacherUrl);
            responseTeachers.EnsureSuccessStatusCode();
            var jsonTeachers = await responseTeachers.Content.ReadAsStringAsync();
            model.TeachersList = JsonSerializer.Deserialize<List<Teacher>>(jsonTeachers);

            //Course
            var newCourseUrl = courseUrl + courseID;
            var responseCourse = await httpClient.GetAsync(newCourseUrl);
            responseCourse.EnsureSuccessStatusCode();
            var courseJson = await responseCourse.Content.ReadAsStringAsync();
            model.CourseGroup = new CourseGroups { Course = JsonSerializer.Deserialize<Course>(courseJson) };

            return Json(new { isValid = false, html = RazorHelper.RenderRazorViewToString(this, "_CreateGroup", model) });
        }

        public async Task<ActionResult> DeleteCourseGroup(int id)
        {
            var model = new AcademicOfferVM();
            try
            {
                if (RolesHelper.IsAuthorized(HttpContext, new ERole[] { ERole.ADMINISTRADOR }))
                {
                    //CourseGroup
                    var newCourseGroupUrl = courseGroupsUrl + id;
                    var responseCourseGroup = await httpClient.GetAsync(newCourseGroupUrl);
                    responseCourseGroup.EnsureSuccessStatusCode();
                    var courseGroupJson = await responseCourseGroup.Content.ReadAsStringAsync();
                    model.CourseGroup = JsonSerializer.Deserialize<CourseGroups>(courseGroupJson);

                    return PartialView("_DeleteGroup", model);
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

        // POST: GroupController/CreateCourseGroup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCourseGroup(AcademicOfferVM model, int id)
        {
            try
            {
                var newUrl = courseGroupsUrl + id;
                var response = await httpClient.DeleteAsync(newUrl);

            }
            catch
            {

            }
            var courseID = TempData["CourseID"] as int?;
            return RedirectToAction("CourseGroups", new { id = courseID });

        }
    }
}
