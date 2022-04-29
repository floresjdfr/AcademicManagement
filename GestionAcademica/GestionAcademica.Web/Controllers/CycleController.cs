using GestionAcademica.Models;
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
    public class CycleController : Controller
    {
        private readonly string CycleUrl = "https://localhost:44367/api/Cycle/";
        private readonly string CycleStatesUrl = "https://localhost:44367/api/CycleState/";
        private readonly HttpClient httpClient = new HttpClient();
        public static readonly string ControllerName = "Cycle";

        // GET: CycleController
        public async Task<ActionResult> Index()
        {
            var model = new CycleVM();
            try
            {
                if (RolesHelper.IsAuthorized(HttpContext, new ERole[] { ERole.ADMINISTRADOR }))
                {
                    var responseCycles = await httpClient.GetAsync(CycleUrl);
                    responseCycles.EnsureSuccessStatusCode();

                    var responseCycleStatesList = await httpClient.GetAsync(CycleStatesUrl);
                    responseCycleStatesList.EnsureSuccessStatusCode();

                    var jsonCycles = await responseCycles.Content.ReadAsStringAsync();
                    var jsonCycleStates = await responseCycleStatesList.Content.ReadAsStringAsync();

                    model.CycleList = JsonSerializer.Deserialize<List<Cycle>>(jsonCycles);
                    model.CycleStateList = JsonSerializer.Deserialize<List<CycleState>>(jsonCycleStates);

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

        // POST: CycleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Cycle Cycle)
        {
            try
            {
                var jsonText = JsonSerializer.Serialize(Cycle);
                var content = new StringContent(jsonText, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(CycleUrl, content);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CycleController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            CycleVM model = new CycleVM();
            try
            {
                if (RolesHelper.IsAuthorized(HttpContext, new ERole[] { ERole.ADMINISTRADOR }))
                {
                    var responseCycle = await httpClient.GetAsync(CycleUrl + id.ToString());
                    responseCycle.EnsureSuccessStatusCode();

                    var responseCycleStatesList = await httpClient.GetAsync(CycleStatesUrl);
                    responseCycleStatesList.EnsureSuccessStatusCode();

                    var jsonCycle = await responseCycle.Content.ReadAsStringAsync();
                    var jsonCycleStates = await responseCycleStatesList.Content.ReadAsStringAsync();

                    model.Cycle = JsonSerializer.Deserialize<Cycle>(jsonCycle);
                    model.CycleStateList = JsonSerializer.Deserialize<List<CycleState>>(jsonCycleStates);

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

        // POST: CycleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Cycle Cycle)
        {
            try
            {
                var jsonText = JsonSerializer.Serialize(Cycle);
                var content = new StringContent(jsonText, Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync(CycleUrl + id.ToString(), content);
                response.EnsureSuccessStatusCode();

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: CycleController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (RolesHelper.IsAuthorized(HttpContext, new ERole[] { ERole.ADMINISTRADOR }))
                {
                    var response = await httpClient.GetAsync(CycleUrl + id.ToString());
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    var model = JsonSerializer.Deserialize<Cycle>(json);
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

        // POST: CycleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var response = await httpClient.DeleteAsync(CycleUrl + id.ToString());
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
