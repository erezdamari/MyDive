using MyDive.Server.Log;
using MyDive.Server.Models;
using Newtonsoft.Json;
using System;
using System.Web.Http;
using static MyDive.Server.Enums;

namespace MyDive.Server.Logic
{
    public class UserLogic
    {
        public bool CheckUserLoginValidation(UserLoginModel i_model)
        {
            return !string.IsNullOrEmpty(i_model.Username) && !string.IsNullOrEmpty(i_model.Password);
        }

        public bool CheckUserRegistrationValidation(UserModel i_model)
        {
            return 
                !string.IsNullOrEmpty(i_model.Username) &&
                !string.IsNullOrEmpty(i_model.Password) &&
                !string.IsNullOrEmpty(i_model.Email);
        }

        public eErrors CreateUser(UserModel i_Model)
        {
            eErrors error = eErrors.None;

            if (CheckUserRegistrationValidation(i_Model))
            {
                    using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                    {
                        int userID = MyDiveDB.stp_CreateUser(
                            i_Model.Username,
                            i_Model.Password,
                            i_Model.Email,
                            i_Model.FirstName,
                            i_Model.LastName,
                            i_Model.Association,
                            i_Model.UserLicenseNumber,
                            i_Model.LicenseTypeID,
                            i_Model.Birthday,
                            (int)i_Model.UserRole);
                    }
            }
            else
            {
                Logger.Instance.Notify("user info in insufficient", eLogType.Error, JsonConvert.SerializeObject(i_Model));
                error = eErrors.InsufficientData;
            }

            return error;
        }
    }
}