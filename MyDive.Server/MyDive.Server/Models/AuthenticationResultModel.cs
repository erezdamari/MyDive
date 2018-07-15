using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static MyDive.Server.Enums;

namespace MyDive.Server.Models
{
    public class AuthenticationResultModel
    {
        [JsonProperty(PropertyName = "ID")]
        public int UserID { get; set; }

        public eUserRole UserRole { get; set; }

        public eErrors Error { get; set; }

        public bool HasError { get; set; }
    }
}