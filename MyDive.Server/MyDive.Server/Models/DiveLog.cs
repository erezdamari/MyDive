using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDive.Server.Models
{
    public class DiveLog
    {
        public int SiteID { get; set; }

        public float MaxDepth { get; set; }

        public string Description { get; set; }

        public int DiveTypeID { get; set; }

        public int UserID { get; set; }

        public int BottomTypeID { get; set; }

        public int SalinityID { get; set; }

        public int WaterTypeID { get; set; }

        public string Location { get; set; }
    }
}