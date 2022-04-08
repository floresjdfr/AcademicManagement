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
    public class CourseController : ControllerBase
    {
        #region Variables
        private CourseService courseService = new CourseService();
        #endregion

        // GET: api/<CourseController>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var result = await courseService.ListCourses();
            if (result == null) return BadRequest();

            return Ok(result);
        }

        // GET api/<CourseController>/5
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await courseService.FindCourseById(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        // POST api/<CourseController>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody] Course value)
        {
            var result = await courseService.InsertCourse(value);
            if (result == false) return BadRequest();

            return Ok(result);
        }

        // PUT api/<CourseController>/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put(int id, [FromBody] Course value)
        {
            if (id != value.ID) return BadRequest();
            var result = await courseService.UpdateCourse(id, value);
            if (result == false) return BadRequest();

            return Ok(result);
        }

        // DELETE api/<CourseController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await courseService.DeleteCourse(id);
            if (result == false) return BadRequest();

            return Ok(result);
        }
    }
}
