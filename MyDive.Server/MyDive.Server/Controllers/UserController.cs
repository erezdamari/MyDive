using MyDive.Server.Log;
using MyDive.Server.Logic;
using MyDive.Server.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Web.Http;
using static MyDive.Server.Enums;

namespace MyDive.Server.Controllers
{
    [RoutePrefix("user")]
    public class UserController : MainController
    {
        public UpdatePasswordLogic m_PasswordLogic { get; set; } = new UpdatePasswordLogic();
        public UserLogic m_Logic { get; set; } = new UserLogic();

        [HttpPost]
        [Route("login")]
        public IHttpActionResult AuthenticateLogin([FromBody] UserLoginModel i_UserLoginInfo)
        {
            LogControllerEntring("login");
            IHttpActionResult result = null;
            if (m_Logic.CheckUserLoginValidation(i_UserLoginInfo))
            {
                try
                {
                    using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                    {
                        ObjectResult<stp_AuthenticateLogin_Result> serverAnswer;
                        List<int> userToReturn = new List<int>();
                        serverAnswer = MyDiveDB.stp_AuthenticateLogin(i_UserLoginInfo.Username, i_UserLoginInfo.Password);
                        foreach (stp_AuthenticateLogin_Result res in serverAnswer)
                        {
                            userToReturn.Add(res.UserID);
                        }

                        if (userToReturn.Count == 0 || userToReturn.Count > 1)
                        {
                            LogData(
                                string.Format("user {0} were unable to login", i_UserLoginInfo.Username),
                                i_UserLoginInfo);
                            result = BadRequest();
                        }
                        else
                        {
                            LogData(string.Format("user {0} is logged in", i_UserLoginInfo.Username), i_UserLoginInfo);
                            result = Ok(userToReturn[0]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    result = LogException(ex, JsonConvert.SerializeObject(i_UserLoginInfo));
                }
            }
            else
            {
                LogError("user info in insufficient", JsonConvert.SerializeObject(i_UserLoginInfo));
                result = BadRequest();
            }

            return result;
        }

        [HttpPost]
        [Route("register")]
        public IHttpActionResult CreateUser([FromBody] UserModel i_User)
        {
            LogControllerEntring("register");
            IHttpActionResult result = null;
            eErrors error = eErrors.None;

            try
            {
                error = m_Logic.CreateUser(i_User);
                if(error != eErrors.None)
                {
                    result = BadRequest(((int)error).ToString());
                }
                LogData("user is register", i_User);
            }
            catch (Exception ex)
            {
                result = LogException(ex, JsonConvert.SerializeObject(i_User));
            }
            
            return result;
        }

        [HttpGet]
        [Route("getuser/{i_UserId}")]
        public IHttpActionResult GetUser(int i_UserId)
        {
            LogControllerEntring("getuser");
            IHttpActionResult result = null;
            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    ObjectResult<stp_GetUser_Result> userResult = MyDiveDB.stp_GetUser(i_UserId);
                    UserModel userToReturn = new UserModel();

                    foreach (stp_GetUser_Result user in userResult)
                    {
                        userToReturn.UserID = user.UserID;
                        userToReturn.Username = user.Username;
                        userToReturn.Email = user.Email;
                        userToReturn.FirstName = user.FirstName;
                        userToReturn.LastName = user.LastName;
                        userToReturn.Association = user.AssociationID;
                        userToReturn.UserLicenseNumber = user.UserLicenceNumber;
                        userToReturn.LicenseTypeID = user.LicenseTypeID;
                        userToReturn.Birthday = user.Birthday;
                    }

                    result = Ok(userToReturn);
                }
            }
            catch (Exception ex)
            {
                result = LogException(ex, JsonConvert.SerializeObject(i_UserId));
            }

            return result;
        }

        [HttpGet]
        [Route("getuserlog/{i_UserId}")]
        public IHttpActionResult GetUserDiveLog(int i_UserId)
        {
            LogControllerEntring("getuserlog");
            IHttpActionResult result = null;
            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    ObjectResult<stp_GetUserDiveLogs_Result> serverResult = MyDiveDB.stp_GetUserDiveLogs(i_UserId);
                    List<DiveLogModel> userDiveLog = new List<DiveLogModel>();
                    foreach (stp_GetUserDiveLogs_Result res in serverResult)
                    {
                        userDiveLog.Add(new DiveLogModel
                        {
                            BottomType = res.BottomType,
                            Description = res.Description,
                            DiveType = res.Type,
                            Coordinates = new LocationModel { Lat = res.Lat, Long = res.Long },
                            MaxDepth = res.MaxDepth,
                            Salinity = res.Salinity,
                            SiteName = res.Name,
                            WaterType = res.WaterType
                        });
                    }

                    result = Ok(userDiveLog.Count > 0 ? userDiveLog : null);
                }
            }
            catch (Exception ex)
            {
                result = LogException(ex, null);
            }

            return result;
        }

        [HttpPost]
        [Route("editprofile")]
        public IHttpActionResult EditUserProfile([FromBody] UserModel i_User)
        {
            LogControllerEntring("editprofile");
            IHttpActionResult result = null;

            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    i_User.FirstName = i_User.FirstName != null ? i_User.FirstName : "";
                    i_User.LastName = i_User.LastName != null ? i_User.LastName : "";
                    int userID = MyDiveDB.stp_EditUserProfile(
                        i_User.UserID,
                        i_User.FirstName,
                        i_User.LastName,
                        i_User.LicenseTypeID);

                    result = Ok(userID);
                    LogData("user edited", i_User);
                }
            }
            catch (Exception ex)
            {
                result = LogException(ex, JsonConvert.SerializeObject(i_User));
            }

            return result;
        }

        [HttpPost]
        [Route("changepassword")]
        public IHttpActionResult UpdatePassword([FromBody] UpdatePasswordModel i_NewPassword)
        {
            LogControllerEntring("changepassword");
            IHttpActionResult result = Ok();

            try
            {
                eErrors error = m_PasswordLogic.CheckPasswordValidation(i_NewPassword);

                if (error == eErrors.None)
                {
                    error = m_PasswordLogic.CheckIfPasswordsAreEquals(i_NewPassword);
                    if (error == eErrors.None)
                    {
                        using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                        {
                            MyDiveDB.stp_UpdateUserPassword(i_NewPassword.UserId, i_NewPassword.NewPassword);
                            LogData("password was changed", i_NewPassword);
                        }
                    }
                    else
                    {
                        result = LogInternalError(error, JsonConvert.SerializeObject(i_NewPassword));
                    }
                }
                else
                {
                    result = LogInternalError(error, JsonConvert.SerializeObject(i_NewPassword));
                }

            }
            catch (Exception ex)
            {
                result = LogException(ex, JsonConvert.SerializeObject(i_NewPassword));
            }

            return result;
        }
    }
}
