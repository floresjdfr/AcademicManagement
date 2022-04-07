using System;
using System.Collections.Generic;
using System.Text;

namespace GestionAcademica.API.Models
{
    public class Career : Base
    {
        public string Code { get; set; }
        public string CareerName { get; set; }
        public string DegreeName { get; set; }
    }
}
