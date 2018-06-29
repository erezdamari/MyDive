using MyDive.Server.Log;
using MyDive.Server.Logic;
using MyDive.Server.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Web.Http;

namespace MyDive.Server.Controllers
{
    [RoutePrefix("user")]
    public class UserController : MainController
    {
        [HttpPost]
        [Route("login")]
        public IHttpActionResult AuthenticateLogin([FromBody] UserLoginModel i_UserLoginInfo)
        {
            LogControllerEntring("login");
            IHttpActionResult result = null;
            if (UserLogic.CheckUserLoginValidation(i_UserLoginInfo))
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
                            Logger.Instance.Notify(
                                string.Format("user {0} were unable to login", i_UserLoginInfo.Username)
                                , eLogType.Info);
                            result = BadRequest();
                        }
                        else
                        {
                            Logger.Instance.Notify(
                                string.Format("user {0} is logged in", i_UserLoginInfo.Username)
                                , eLogType.Info);
                            result = Ok(userToReturn[0]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    result = LogException(ex);
                }
            }
            else
            {
                Logger.Instance.Notify("user info in insufficient", eLogType.Error);
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

            if (UserLogic.CheckUserRegistrationValidation(i_User))
            {
                try
                {
                    using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                    {
                        int userID = MyDiveDB.stp_CreateUser(
                            i_User.Username,
                            i_User.Password,
                            i_User.Email,
                            i_User.FirstName,
                            i_User.LastName,
                            i_User.Association,
                            i_User.UserLicenseNumber,
                            i_User.LicenseTypeID,
                            i_User.Birthday);

                        result = Ok(userID);
                    }
                }
                catch (Exception ex)
                {
                    result = LogException(ex);
                }
            }
            else
            {
                Logger.Instance.Notify("user info in insufficient", eLogType.Error);
                result = BadRequest();
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
                        userToReturn.Password = user.Password;
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
            catch(Exception ex)
            {
                result = LogException(ex);
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
                    foreach(stp_GetUserDiveLogs_Result res in serverResult)
                    {
                        userDiveLog.Add(new DiveLogModel
                        {
                            BottomTypeID = res.ButtomTypeID,
                            Description = res.Description,
                            DiveTypeID = res.DiveTypeID,
                            Location = new LocationModel { Lat = res.Lat, Long = res.Long},
                            MaxDepth = res.MaxDepth,
                            SalinityID = res.SalinityID,
                            SiteID = res.SiteID,
                            UserID = res.UserID,
                            WaterTypeID = res.WaterTypeID
                        });
                    }

                    result = Ok(userDiveLog.Count > 0 ? userDiveLog : null);
                }
            }
            catch (Exception ex)
            {
                result = LogException(ex);
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
                    i_User.FirstName = i_User.FirstName == null ? i_User.FirstName : "";
                    i_User.LastName = i_User.LastName == null ? i_User.LastName : "";
                    int userID = MyDiveDB.stp_EditUserProfile(
                        i_User.UserID,
                        i_User.FirstName,
                        i_User.LastName,
                        i_User.LicenseTypeID);

                    result = Ok(userID);
                }
            }
            catch (Exception ex)
            {
                result = LogException(ex);
            }

            return result;
        }
    }
}
