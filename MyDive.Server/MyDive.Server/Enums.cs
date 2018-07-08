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
            PasswordAreNotEqual = -2
        }

        public enum eLogType
        {
            All = 0,
            Error = 1,
            Info = 2,
            Debug = 3
        }
    }
}