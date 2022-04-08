using GestionAcademica.API.DataAccess;
using GestionAcademica.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionAcademica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CareerCoursesController : ControllerBase
    {
        #region Variables
        CareerCoursesService careerCoursesService = new CareerCoursesService();
        #endregion

        // GET api/<CareerCoursesController>/5
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpGet("{careerId}")]
        public async Task<IActionResult> Get (int careerId)
        {
            var result = await careerCoursesService.GetCoursesByCareer(careerId);
            if (result == null) return BadRequest();
            return Ok(result);
        }

        // POST api/<CareerCoursesController>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody] CareerCourses value)
        {
            var result = await careerCoursesService.AddCourseToCareer(value);
            if (result == false) return BadRequest();
            return Ok(result);
        }

        //// PUT api/<CareerCoursesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<CareerCoursesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await careerCoursesService.DeleteCourseFromCareer(id);
            if (result == false) return BadRequest();
            return Ok(result);
        }
    }
}
