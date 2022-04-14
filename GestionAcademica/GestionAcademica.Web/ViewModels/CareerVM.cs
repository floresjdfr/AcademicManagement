using GestionAcademica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAcademica.Web.ViewModels
{
    public class CareerVM
    {
        public Career Career { get; set; }
        public List<Career> ListCareer { get; set; }
    }
}
