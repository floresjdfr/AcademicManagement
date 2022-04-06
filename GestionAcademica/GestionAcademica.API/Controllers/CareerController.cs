using GestionAcademica.Logic;
using GestionAcademica.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAcademica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CareerController : ControllerBase
    {
        #region Variables
        private CareerLogic careerLogic;
        #endregion

        #region Constructor
        public CareerController()
        {
            careerLogic = new CareerLogic();
        }
        #endregion

        [HttpGet]
        public IEnumerable<Career> GetCareers()
        {
            return careerLogic.Get();
        }
        [HttpPost]
        public ActionResult<Career>PostCareer([FromBody] Career career)
        {
            var carrerAdded = careerLogic.Create(career);
            return CreatedAtAction(nameof(GetCareers), new { id = carrerAdded.PK }, career);
        }

    }
}
