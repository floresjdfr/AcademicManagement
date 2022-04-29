using System;
using System.Collections.Generic;
using System.Text;

namespace GestionAcademica.Models
{
    public class Error : Base
    {
        public int? ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
