
using GestionAcademica.API.DataAccess;
using GestionAcademica.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GestionAcademica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CareerController : ControllerBase
    {
        #region Variables
        private CareerService careerService = new CareerService();
        #endregion

       

        [HttpGet]
        public async Task<IActionResult> GetCareers()
        {
            var result = await careerService.ListCareer();
            if (result == null) return BadRequest();

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCareers(int id)
        {
            var result = await careerService.FindCareerById(id);
            if (result == null) return NotFound();

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [HttpPost]
        public async Task<IActionResult> PostCareer([FromBody] Career career)
        {
            var result = await careerService.InsertCareer(career);
            if (result == false) return BadRequest();

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCareer(int id)
        {
            var result = await careerService.DeleteCareer(id);
            if (result == false) return BadRequest();

            return StatusCode((int)HttpStatusCode.OK, result);
        }
    }
}
