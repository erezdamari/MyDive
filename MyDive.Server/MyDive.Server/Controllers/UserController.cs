using MyDive.Server.Models;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Web.Http;

namespace MyDive.Server.Controllers
{
    [RoutePrefix("user")]
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("login")]
        public IHttpActionResult AuthenticateLogin([FromBody] UserLogin i_UserLoginInfo)
        {
            ObjectResult<stp_AuthenticateLogin1_Result> result;
            List<int> userToReturn = new List<int>();

            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {

                result = MyDiveDB.stp_AuthenticateLogin1(i_UserLoginInfo.Username, i_UserLoginInfo.Password);

                foreach (stp_AuthenticateLogin1_Result res in result)
                {
                    userToReturn.Add(res.UserID);
                }

            }
            
            if (userToReturn.Count == 0 || userToReturn.Count > 1)
            {
                return BadRequest();
            }
            else
            {
                return Ok(userToReturn[0]);
            }
        }

        [HttpPost]
        [Route("register")]
        public IHttpActionResult CreateUser([FromBody] User i_User)
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

                return Ok(userID);
            }
        }

        [HttpGet]
        [Route("getuser/{i_UserId}")]
        public IHttpActionResult GetUser(int i_UserId)
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                ObjectResult<stp_GetUser_Result> userResult = MyDiveDB.stp_GetUser(i_UserId);
                User userToReturn = new User();

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

                return Ok(userToReturn);
            }
        }

        [HttpGet]
        [Route("getuserlog/{i_UserId}")]
        public IHttpActionResult GetUserDiveLog(int i_UserId)
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                ObjectResult<stp_GetUserDiveLogs_Result> user = MyDiveDB.stp_GetUserDiveLogs(i_UserId);

                return Ok(user);
            }
        }

        [HttpGet]
        [Route("getuserwish/{i_UserId}")]
        public IHttpActionResult GetUserWishList(int i_UserId)
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                ObjectResult<stp_GetUserWishList_Result> userResult = MyDiveDB.stp_GetUserWishList(i_UserId);
                UserWishList userToReturn = new UserWishList();

                foreach (stp_GetUserWishList_Result user in userResult)
                {
                    userToReturn.WishID = user.WishID;
                    userToReturn.SiteID = user.SiteID;
                    userToReturn.UserID = user.UserID;
                }
                return Ok(userToReturn);
            }
        }
    }
}
