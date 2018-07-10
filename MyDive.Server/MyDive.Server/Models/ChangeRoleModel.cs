using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static MyDive.Server.Enums;

namespace MyDive.Server.Models
{
    public class ChangeRoleModel
    {
        public int AdminId { get; set; }

        public int UserId { get; set; }

        public eUserRole Role { get; set; }
    }
}