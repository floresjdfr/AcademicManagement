using GestionAcademica.API.DataAccess;
using GestionAcademica.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionAcademica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly EnrollmentService enrollmentService = new EnrollmentService();
        //// GET: api/<EnrollmentController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<EnrollmentController>/GetAvailableGroups/5
        [HttpGet("GetAvailableGroups/{studentID}")]
        public async Task<IActionResult> GetAvailableGroups(int studentID)
        {
            try
            {
                var response = await enrollmentService.FindAvailableGroupsToEnroll(studentID);
                return Ok(response);
            }
            catch
            {
                return BadRequest("An error ocurred");
            }
        }

        // GET api/<EnrollmentController>/Get/5
        [HttpGet("GetStudentGroupsOfCurrentCycle/{studentID}")]
        public async Task<IActionResult> GetStudentGroupsOfCurrentCycle(int studentID)
        {
            try
            {
                var response = await enrollmentService.FindStudentGroupsByCycleState(studentID, 1);
                return Ok(response);
            }
            catch
            {
                return BadRequest("An error ocurred");
            }
        }


        //// POST api/<EnrollmentController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<EnrollmentController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<EnrollmentController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
