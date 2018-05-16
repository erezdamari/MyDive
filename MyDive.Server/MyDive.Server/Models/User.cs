using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDive.Server.Models
{
    public class User
    {
        public int UserID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? Association { get; set; }

        public string UserLicenseNumber { get; set; }

        public int? LicenseTypeID { get; set; }

        public DateTime? Birthday { get; set; }
    }
}