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
        //public Response Create(Career career)
        //{
        //    return careerService.InsertCareer(career);
        //}
        //public List<Base> Get()
        //{
        //    List<Base> responseList = new List<Base>();
        //    var response = careerService.ListCareer();

        //    if (response.Code == Response.OK)
        //        ((List<Career>)response.Content).ForEach(item => responseList.Add(item));
        //    else
        //        responseList.Add((Error)response.Content);

        //    return responseList;
        //}
        #endregion

    }
}
