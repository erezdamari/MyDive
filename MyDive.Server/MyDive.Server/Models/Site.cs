using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDive.Server.Models
{
    public class Site
    {
        public int SiteID { get; set; }
        public string Name { get; set; }
        public string Polygon { get; set; }
        public Nullable<double> Rating { get; set; }
        public int CountryID { get; set; }
        public int CityID { get; set; }
    }
}