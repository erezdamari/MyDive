﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDive.Server.Models
{
    public class SiteModel
    {
        public int SiteID { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public double? Rating { get; set; }
        public int CountryID { get; set; }
        public int CityID { get; set; }
    }
}