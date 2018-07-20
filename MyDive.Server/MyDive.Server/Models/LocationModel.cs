using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDive.Server.Models
{
    public class LocationModel
    {
        [JsonProperty(PropertyName = "lat")]
        public double? Lat { get; set; }

        [JsonProperty(PropertyName = "lng")]
        public double? Long { get; set; }

        public int? Place { get; set; }
    }
}