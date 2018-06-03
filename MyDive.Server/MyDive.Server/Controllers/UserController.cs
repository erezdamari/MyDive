using MyDive.Server.Models;
using System.Data.Entity.Core.Objects;
using System.Web.Http;

namespace MyDive.Server.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("user/Login")]
        public IHttpActionResult AuthenticateLogin([FromBody] UserLogin i_UserLoginInfo)
        {
            bool isAuthenticated = true;

            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                isAuthenticated = MyDiveDB.stp_AuthenticateLogin(i_UserLoginInfo.Username, i_UserLoginInfo.Password) == 1;
            }

            if (isAuthenticated)
                return Ok("ok");//return user id
            else
                return NotFound();
        }

        [HttpPost]
        [Route("user/Register")]
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
        [Route("user/test")]
        public IHttpActionResult test()
        {
            return Ok("success");
        }

        [HttpGet]
        [Route("user/GetUser/{i_UserId}")]
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
        [Route("user/GetUserLog/{i_UserId}")]
        public IHttpActionResult GetUserDiveLog(int i_UserId)
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                ObjectResult<stp_GetUserDiveLogs_Result> user = MyDiveDB.stp_GetUserDiveLogs(i_UserId);

                return Ok(user);
            }
        }

        [HttpGet]
        [Route("user/GetUserWish/{i_UserId}")]
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
