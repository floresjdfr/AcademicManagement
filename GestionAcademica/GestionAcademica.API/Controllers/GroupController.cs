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
    public class GroupController : ControllerBase
    {
        #region Variables
        private readonly GroupService groupService = new GroupService();
        #endregion

        // GET: api/<GroupController>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get()
        {
            var result = await groupService.ListGroup();
            if (result == null) return BadRequest();

            return Ok(result);
        }

        // GET api/<GroupController>/5
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await groupService.FindGroup(new Group { ID = id });
            if (result == null) return NotFound();

            return Ok(result);
        }

        // POST api/<GroupController>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody] Group value)
        {
            var result = await groupService.InsertGroup(value);
            if (result == false) return BadRequest();

            return Ok(result);
        }

        // PUT api/<GroupController>/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put(int id, [FromBody] Group value)
        {
            if (id != value.ID) return BadRequest();
            var result = await groupService.UpdateGroup(id, value);
            if (result == false) return BadRequest();

            return Ok(result);
        }
        // PUT api/<GroupController>/5/6
        [HttpPut("UpdateTeacher/{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> PutTeacher(int id, [FromBody] Group value)
        {
            if (id != value.ID) return BadRequest();
            var result = await groupService.UpdateGroup(id, value);
            if (result == false) return BadRequest();

            return Ok(result);
        }

        // DELETE api/<GroupController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await groupService.DeleteGroup(id);
            if (result == false) return BadRequest();

            return Ok(result);
        }
    }
}
