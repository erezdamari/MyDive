using MyDive.Server.Log;
using MyDive.Server.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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

        public eErrors ChangeRole(ChangeRoleModel i_Model)
        {
            eErrors error = eErrors.None;
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                ObjectResult<int?> serverResult = MyDiveDB.stp_GetUserRole(i_Model.AdminId);
                List<int> result = new List<int>();
                foreach(int res in serverResult)
                {
                    result.Add(res);
                }

                if(result.Count > 0)
                {
                    if(result[0] == 1)
                    {
                        MyDiveDB.stp_ChangeUserRole(i_Model.UserId, (int)i_Model.Role);
                    }
                    else
                    {
                        error = eErrors.UserIsNotAdmin;
                    }
                }
                else
                {
                    error = eErrors.AdminNotExist;
                }
            }

            return error;
        }

        public AuthenticationResultModel CreateUser(UserModel i_Model)
        {
            AuthenticationResultModel registerResult = new AuthenticationResultModel
            {
                Error = eErrors.None,
                HasError = false,
                UserID = i_Model.UserID,
                UserRole = eUserRole.RegularUser
            };

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
                            (int)eUserRole.RegularUser);
                    }
            }
            else
            {
                Logger.Instance.Notify("user info in insufficient", eLogType.Error, JsonConvert.SerializeObject(i_Model));
                registerResult.Error = eErrors.InsufficientData;
                registerResult.HasError = true;
            }

            return registerResult;
        }
    }
}