using GestionAcademica.API.DataAccess;
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
    public class CycleStateController : ControllerBase
    {
        #region Variables
        private readonly CycleStateService CycleStateService = new CycleStateService();
        #endregion

        // GET: api/<UserController>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get()
        {
            var result = await CycleStateService.ListCycleState();
            if (result == null) return BadRequest();

            return Ok(result);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await CycleStateService.FindCycleState(id);
            if (result == null) return NotFound();

            return Ok(result);
        }
    }
}
