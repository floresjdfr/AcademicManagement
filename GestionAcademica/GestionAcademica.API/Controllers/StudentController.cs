using GestionAcademica.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionAcademica.API.DataAccess
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        #region Variables
        private readonly StudentService studentService = new StudentService();
        private readonly UserService userService = new UserService();
        #endregion

        // GET: api/<StudentController>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get()
        {
            var result = await studentService.ListStudent();
            if (result == null) return BadRequest();

            return Ok(result);
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await studentService.FindStudent(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        // GET api/<StudentController>/GetStudentByUser/5
        [HttpGet("GetStudentByUser/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetStudentByUser(int id)
        {
            var result = await studentService.FindStudentByUser(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        // GET api/<StudentController>/GetStudentCycles/5
        [HttpGet("GetStudentCycles/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetStudentCycles(int id)
        {
            try
            {
                var result = await studentService.FindStudentCycles(new Student { ID = id });
                if (result == null) throw new Exception();

                var filteredList = result
                    .GroupBy(item => item.ID)
                    .Select(group => group.FirstOrDefault())
                    .OrderBy(item => item.ID)
                    .ToList();

                return Ok(filteredList);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET api/<StudentController>/GetStudentAcademicHistory/5/1
        [HttpGet("GetStudentAcademicHistory/{id}/{cycleID?}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetStudentAcademicHistory(int id, int? cycleID)
        {
            try
            {
                var result = await studentService.FindStudentAcademicHistory(id, cycleID);
                if (result == null) throw new Exception();

                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        // POST api/<StudentController>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody] Student value)
        {
            User studentUser = new User
            {
                UserID = value.IdStudent,
                Password = value.User.Password,
                UserType = new UserType
                {
                    ID = (int)EnumUserType.Alumno
                }
            };
            var resultUser = await userService.InsertUser(studentUser);
            if (resultUser)
                value.User = await userService.FindUser(new User { UserID = studentUser.UserID });

            var resultStudent = await studentService.InsertStudent(value);
            if (!resultStudent) return BadRequest();

            return Ok(resultStudent);
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put(int id, [FromBody] Student value)
        {
            if (id != value.ID) return BadRequest();
            var result = await studentService.UpdateStudent(id, value);
            if (result == false) return BadRequest();

            return Ok(result);
        }
        // PUT api/<StudentController>/5/6
        [HttpPut("UpdateTeacher/{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> PutTeacher(int id, [FromBody] Student value)
        {
            if (id != value.ID) return BadRequest();
            var result = await studentService.UpdateStudent(id, value);
            if (result == false) return BadRequest();

            return Ok(result);
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await studentService.DeleteStudent(id);
            if (result == false) return BadRequest();

            return Ok(result);
        }
    }
}
