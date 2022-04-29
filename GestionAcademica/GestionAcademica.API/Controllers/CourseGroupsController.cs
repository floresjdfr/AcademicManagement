using GestionAcademica.API.DataAccess;
using GestionAcademica.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        //// GET: api/<CourseGroupsController>/GetByCurrentCycle/
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[HttpGet("GetByCurrentCycle/")]
        //public async Task<IActionResult> GetByCurrentCycle()
        //{
        //    var result = await courseGroupsService.FindCourseGroupsByCurrentCycle();
        //    if (result == null) return BadRequest();
        //    var dict = result.GroupBy(item => item.Course.)
        //    return Ok(result);
        //}

        // GET api/<CourseGroupsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await courseGroupsService.FindCourseGroupsByID(id);
            if (result == null) return BadRequest();
            return Ok(result);
        }

        // POST api/<CourseGroupsController>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody] CourseGroups value)
        {
            try
            {
                var result = await courseGroupsService.AddGroupToCourse(value);
                if (result == false) throw new Exception("Unexpected error");
                return Ok(result);
            }
            catch (SqlException ex)
            {
                if (ex.Number >= 51000)
                {
                    var error = new Error
                    {
                        ErrorCode = ex.Number,
                        ErrorMessage = ex.Message
                    };
                    return BadRequest(error);
                }
                else
                    throw new Exception("Unexpected error");
            }
            catch (Exception ex)
            {
                var error = new Error { ErrorCode = -1, ErrorMessage = ex.Message };
                return BadRequest(error);
            }

        }

        // PUT api/<CourseGroupsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CourseGroupsController>/5
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await courseGroupsService.DeleteCourseFromCareer(id);
            if (result == false) return BadRequest();
            return Ok(result);
        }
    }
}
