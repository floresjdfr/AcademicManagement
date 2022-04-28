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
    public class UserController : ControllerBase
    {
        #region Variables
        private readonly UserService userService = new UserService();
        #endregion

        // GET: api/<UserController>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get()
        {
            var result = await userService.ListUser();
            if (result == null) return BadRequest();
            result.ForEach(item => item .Password = "");

            return Ok(result);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await userService.FindUser(new User { ID = id });
            if (result == null) return NotFound();
            result.Password = null;

            return Ok(result);
        }

        // POST api/<UserController>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody] User value)
        {
            var result = await userService.InsertUser(value);
            if (result == false) return BadRequest();

            return Ok(result);
        }

        // POST api/<UserController>/Login
        [HttpPost("Login/")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Login([FromBody] User value)
        {
            try
            {
                value.ID = null;
                var result = await userService.FindUser(value);

                if (result == null) throw new Exception();
                if (value.Password != result.Password) throw new Exception();

                result.Password = null;

                return Ok(result);
            }
            catch
            {
                return BadRequest(new { errorID = -2, error = "Incorrect username or password" });
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put(int id, [FromBody] User value)
        {
            if (id != value.ID) return BadRequest();
            var result = await userService.UpdateUser(id, value);
            if (result == false) return BadRequest();

            return Ok(result);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await userService.DeleteUser(id);
            if (result == false) return BadRequest();

            return Ok(result);
        }
    }
}
