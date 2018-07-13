using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDive.Server.Models
{
    public class SiteModel
    {
        [JsonProperty(PropertyName = "ID")]
        public int SiteID { get; set; }
        public string Name { get; set; }
        public List<LocationModel> Coordinates { get; set; }
        public double? Rating { get; set; }
    }
}