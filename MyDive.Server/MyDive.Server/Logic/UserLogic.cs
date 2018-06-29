using MyDive.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDive.Server.Logic
{
    public class UserLogic
    {
        public static bool CheckUserLoginValidation(UserLoginModel i_model)
        {
            return !string.IsNullOrEmpty(i_model.Username) && !string.IsNullOrEmpty(i_model.Password);
        }

        public static bool CheckUserRegistrationValidation(UserModel i_model)
        {
            return 
                !string.IsNullOrEmpty(i_model.Username) &&
                !string.IsNullOrEmpty(i_model.Password) &&
                !string.IsNullOrEmpty(i_model.Email);
        }
    }
}