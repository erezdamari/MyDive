using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDive.Server.Models
{
    public class LogNewDiveModel
    {
        public List<BottomTypeModel> BottomTypes { get; set; }

        public List<DiveTypeModel> DiveTypes { get; set; }

        public List<SalinityTypeModel> SalinityTypes { get; set; }

        public List<WaterTypeModel> WaterTypes { get; set; }
    }
}