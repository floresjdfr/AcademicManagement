using GestionAcademica.DataAccess;
using GestionAcademica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAcademica.Logic
{
    public class CareerLogic
    {
        #region Variables
        private CareerService careerService;
        #endregion

        #region Constructor
        public CareerLogic()
        {
            careerService = new CareerService();
        }
        #endregion

        #region Methods
        public Career Create(Career career)
        {
            return careerService.InsertCareer(career);
        }
        public List<Career> Get()
        {
            return careerService.ListCareer();
        }
        #endregion

    }
}
