using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDive.Server.Models
{
    public class LogModel
    {
        public int Type { get; set; }

        public string Msg { get; set; }

        public DateTime Date { get; set; }

        public string Data { get; set; }
    }
}