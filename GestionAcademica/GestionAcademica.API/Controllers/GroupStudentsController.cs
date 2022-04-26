using GestionAcademica.API.DataAccess;
using GestionAcademica.Models;
using Microsoft.AspNetCore.Http;
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
    public class GroupStudentsController : ControllerBase
    {
        #region Variables
        GroupStudentsService groupStudentService = new GroupStudentsService();
        #endregion

        // GET api/<CourseGroupsController>/
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var result = await groupStudentService.FindGroupStudents();
            if (result == null) return BadRequest();
            return Ok(result);
        }

        // GET api/<CourseGroupsController>/GetByGroup/5
        [HttpGet("GetByGroup/{groupId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByGroup(int groupId)
        {
            var result = await groupStudentService.FindGroupStudents(null, null, groupId, null);
            if (result == null) return BadRequest();
            return Ok(result);
        }

        // GET api/<CourseGroupsController>/studentID/5
        [HttpGet("GetByStudent/{studentID}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByStudent(int studentID)
        {
            var result = await groupStudentService.FindGroupStudents(null, studentID, null, null);
            if (result == null) return BadRequest();
            return Ok(result);
        }

        // GET api/<CourseGroupsController>/studentID/5
        [HttpGet("GetByStudentAndCycle/{studentID}/{cycleID}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByStudentAndCycle(int studentID, int cycleID)
        {
            var result = await groupStudentService.FindGroupStudents(null, studentID, null, cycleID);
            if (result == null) return BadRequest();
            return Ok(result);
        }

        // GET api/<CourseGroupsController>/studentID/5
        [HttpGet("GetByStudentAndGroup/{studentID}/{groupID}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByStudentAndGroup(int studentID, int groupID)
        {
            var result = await groupStudentService.FindGroupStudents(null, studentID, groupID, null);
            if (result == null) return BadRequest();
            return Ok(result);
        }

        // POST api/<CourseGroupsController>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody] GroupStudents value)
        {
            var result = await groupStudentService.InsertGroupStudents(value);
            if (result == false) return BadRequest();
            return Ok(result);
        }

        // PUT api/<CycleController>/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put(int id, [FromBody] GroupStudents value)
        {
            if (id != value.ID) return BadRequest();
            var result = await groupStudentService.UpdateGroupStudentScore(id, value);
            if (result == false) return BadRequest();

            return Ok(result);
        }

        // DELETE api/<CourseGroupsController>/5
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await groupStudentService.DeleteGroupStudent(id);
            if (result == false) return BadRequest();
            return Ok(result);
        }
    }
}
