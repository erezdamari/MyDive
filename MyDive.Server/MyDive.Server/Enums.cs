using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDive.Server
{
    public class Enums
    {
        public enum eErrors
        {
            None = 0,
            PasswordIsWrong = -1,
            PasswordAreNotEqual = -2,
            InsufficientData = -3,
            AdminNotExist = -4,
            UserIsNotAdmin = -5
        }

        public enum eLogType
        {
            All = 0,
            Error = 1,
            Info = 2,
            Debug = 3
        }

        public enum eUserRole
        {
            Admin = 1,
            RegularUser = 2
        }
    }
}