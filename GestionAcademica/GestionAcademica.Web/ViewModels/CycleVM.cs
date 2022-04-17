using GestionAcademica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAcademica.Web.ViewModels
{
    public class CycleVM
    {
        public Cycle Cycle { get; set; }
        public List<Cycle> CycleList { get; set; }
        public List<CycleState> CycleStateList { get; set; }

    }
}
