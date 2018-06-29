using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDive.Server.Models
{
    public class RatingModel
    {
        public int EntityID { get; set; }//club or site

        public int Rate { get; set; }

        public string Comment { get; set; }
    }
}