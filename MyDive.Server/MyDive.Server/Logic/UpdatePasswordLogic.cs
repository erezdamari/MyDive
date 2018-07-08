using MyDive.Server.Log;
using MyDive.Server.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using static MyDive.Server.Enums;

namespace MyDive.Server.Logic
{
    public class UpdatePasswordLogic
    {
        public eErrors CheckPasswordValidation(UpdatePasswordModel i_Model)
        {
            eErrors error = eErrors.None;

            try
            {
                using(MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    ObjectResult<string> serverResult = MyDiveDB.stp_GetUserPassword(i_Model.UserId);
                    List<string> userPass = serverResult.ToList();
                    if(userPass.Count > 0)
                    {
                        if (!userPass[0].Equals(i_Model.OldPassword))
                        {
                            error = eErrors.PasswordAreNotEqual;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.Instance.Notify(ex.StackTrace, eLogType.Error, JsonConvert.SerializeObject(i_Model));
            }

            return error;
        }

        public eErrors CheckIfPasswordsAreEquals(UpdatePasswordModel i_NewPassword)
        {
            eErrors error = eErrors.None;

            if (!(i_NewPassword.NewPassword.Equals(i_NewPassword.NewConfirmPassword)))
            {
                error = eErrors.PasswordAreNotEqual;
            }

            return error;
        }
    }
}