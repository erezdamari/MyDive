using MyDive.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDive.Server.Logic
{
    public class IssueLogic
    {
        public static bool CheckIsValidInfo(IssueModel i_Model)
        {
            bool isValid = false;

            isValid = !string.IsNullOrEmpty(i_Model.Subject);
            isValid = isValid && !string.IsNullOrEmpty(i_Model.Subject);
            isValid = isValid && !string.IsNullOrEmpty(i_Model.Description);
            isValid = isValid && i_Model.Subject.Length <= 50;
            isValid = isValid && i_Model.Description.Length >= 10;
            isValid = isValid && i_Model.Description.Length <= 200;

            return isValid;
        }
    }
}