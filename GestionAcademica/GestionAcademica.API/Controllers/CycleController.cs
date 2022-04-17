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
    public class CycleController : ControllerBase
    {
        #region Variables
        private readonly CycleService CycleService = new CycleService();
        #endregion

        // GET: api/<CycleController>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get()
        {
            var result = await CycleService.ListCycle();
            if (result == null) return BadRequest();

            return Ok(result);
        }

        // GET api/<CycleController>/5
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await CycleService.FindCycle(new Cycle { ID = id });
            if (result == null) return NotFound();

            return Ok(result);
        }
        // GET api/Year/<CycleController>/5
        [HttpGet("Year/{year}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByYear(int year)
        {
            var result = await CycleService.FindCycle(new Cycle { Year = year });
            if (result == null) return NotFound();

            return Ok(result);
        }

        // POST api/<CycleController>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody] Cycle value)
        {
            var result = await CycleService.InsertCycle(value);
            if (result == false) return BadRequest();

            return Ok(result);
        }

        // PUT api/<CycleController>/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put(int id, [FromBody] Cycle value)
        {
            if (id != value.ID) return BadRequest();
            var result = await CycleService.UpdateCycle(id, value);
            if (result == false) return BadRequest();

            return Ok(result);
        }

        // DELETE api/<CycleController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await CycleService.DeleteCycle(id);
            if (result == false) return BadRequest();

            return Ok(result);
        }
    }
}
