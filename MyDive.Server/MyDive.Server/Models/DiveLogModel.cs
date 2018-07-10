using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDive.Server.Models
{
    public class DiveLogModel
    {
        public int LogId { get; set; }

        public string SiteName { get; set; }

        public int SiteID { get; set; }

        public double? MaxDepth { get; set; }

        public string Description { get; set; }

        public string DiveType { get; set; }

        public string BottomType { get; set; }

        public string Salinity { get; set; }

        public string WaterType { get; set; }

        public int DiveTypeID { get; set; }

        public int BottomTypeID { get; set; }

        public int SalinityID { get; set; }

        public int WaterTypeID { get; set; }

        public int UserID { get; set; }

        public LocationModel Coordinates { get; set; }
    }
}