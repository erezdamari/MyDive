using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDive.Server.Models
{
    public class ClubModel
    {
        public int ClubID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public double? Rating { get; set; }
        public string SiteURL { get; set; }
        public LocationModel Coordinates { get; set; }
    }
}