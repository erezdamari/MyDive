using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDive.Server.Models
{
    public class UserWishListModel
    {
        public int WishID { get; set; }

        public int SiteID { get; set; }

        public int UserID { get; set; }

        public string SiteName { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public double? Rating { get; set; }
    }
}