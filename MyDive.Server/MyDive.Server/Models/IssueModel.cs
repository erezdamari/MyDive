using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDive.Server.Models
{
    public class IssueModel
    {
        public int IssueId { get; set; }

        public string Subject { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }

        public int Status { get; set; }
    }
}