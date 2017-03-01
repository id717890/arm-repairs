using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using arm_repairs_project.Models.Data;

namespace arm_repairs_project.Models
{
    public class Demands
    {
        public IEnumerable<Demand> DemandsList { get; set; } 
    }
}