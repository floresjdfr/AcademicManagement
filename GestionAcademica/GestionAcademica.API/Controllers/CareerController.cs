﻿
using GestionAcademica.API.DataAccess;
using GestionAcademica.Models;
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
    public class CareerController : ControllerBase
    {
        #region Variables
        private readonly CareerService careerService = new CareerService();
        #endregion

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCareers()
        {
            var result = await careerService.ListCareer();
            if (result == null) return BadRequest();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCareers(int id)
        {
            var result = await careerService.FindCareerById(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> PostCareer([FromBody] Career career)
        {
            var result = await careerService.InsertCareer(career);
            if (result == false) return BadRequest();

            return Ok(result);
        }

        // PUT api/<CourseController>/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put(int id, [FromBody] Career value)
        {
            if (id != value.ID) return BadRequest();
            var result = await careerService.UpdateCareer(id, value);
            if (result == false) return BadRequest();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCareer(int id)
        {
            var result = await careerService.DeleteCareer(id);
            if (result == false) return BadRequest();

            return Ok(result);
        }
    }
}
