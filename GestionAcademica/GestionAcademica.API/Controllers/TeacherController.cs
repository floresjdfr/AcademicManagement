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
    public class TeacherController : ControllerBase
    {
        #region Variables
        private readonly TeacherService teacherService = new TeacherService();
        #endregion

        // GET: api/<TeacherController>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get()
        {
            var result = await teacherService.ListTeacher();
            if (result == null) return BadRequest();

            return Ok(result);
        }

        // GET api/<TeacherController>/5
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await teacherService.FindTeacher(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        // POST api/<TeacherController>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody] Teacher value)
        {
            var result = await teacherService.InsertTeacher(value);
            if (result == false) return BadRequest();

            return Ok(result);
        }

        // PUT api/<TeacherController>/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put(int id, [FromBody] Teacher value)
        {
            if (id != value.ID) return BadRequest();
            var result = await teacherService.UpdateTeacher(id, value);
            if (result == false) return BadRequest();

            return Ok(result);
        }

        // DELETE api/<TeacherController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await teacherService.DeleteTeacher(id);
            if (result == false) return BadRequest();

            return Ok(result);
        }
    }
}
