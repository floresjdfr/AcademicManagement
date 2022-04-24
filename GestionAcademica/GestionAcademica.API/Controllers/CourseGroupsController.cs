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
    public class CourseGroupsController : ControllerBase
    {
        #region Variables
        CourseGroupsService courseGroupsService = new CourseGroupsService();
        #endregion

        // GET: api/<CourseGroupsController>
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpGet("GetByCourse/{courseId}")]
        public async Task<IActionResult> GetByCourse(int courseId)
        {
            var result = await courseGroupsService.FindGroupsByCourse(courseId);
            if (result == null) return BadRequest();
            return Ok(result);
        }

        // GET api/<CourseGroupsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CourseGroupsController>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> Post([FromBody] CourseGroups value)
        {
            var result = await courseGroupsService.AddGroupToCourse(value);
            if (result == false) return BadRequest();
            return Ok(result);
        }

        // PUT api/<CourseGroupsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CourseGroupsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
