using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestionAcademica.Models
{
    public class Cycle : Base
    {
        public CycleState CycleState { get; set; }
        
        [DisplayName("Año Ciclo")]
        public int? Year { get; set; }

        [DisplayName("Numero Ciclo")]
        public int? Number { get; set; }
       
        [DisplayName("Fecha Inicio")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DisplayName("Fecha Finalización")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
    }
}
