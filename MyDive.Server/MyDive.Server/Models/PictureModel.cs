using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static MyDive.Server.Enums;

namespace MyDive.Server.Models
{
    public class PictureModel
    {
        public string Picture { get; set; }

        public ePictureType PictureType { get; set; }
    }
}