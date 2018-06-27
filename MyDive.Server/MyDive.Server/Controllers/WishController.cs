using MyDive.Server.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyDive.Server.Controllers
{
    [RoutePrefix("wish")]
    public class WishController : MainController
    {
        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreateWish([FromBody] Wish i_Wish)
        {
            LogControllerEntring();
            IHttpActionResult result = null;

            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    int wishID = MyDiveDB.stp_CreateNewWish(i_Wish.SiteID, i_Wish.UserID);
                    result =  Ok(wishID);
                }
            }
            catch(Exception ex)
            {
                result = LogException(ex);
            }

            return result;
        }

        [HttpPost]
        [Route("remove")]
        public IHttpActionResult RemoveWish([FromBody] Wish i_Wish)
        {
            LogControllerEntring();
            IHttpActionResult result = null;

            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    int wishID = MyDiveDB.stp_RemoveFromWishList(i_Wish.UserID, i_Wish.SiteID);
                    result =  Ok(wishID);
                }
            }
            catch(Exception ex)
            {
                result = LogException(ex);
            }

            return result;
        }

        [HttpGet]
        [Route("getuserwish/{i_UserId}")]
        public IHttpActionResult GetUserWishList(int i_UserId)
        {
            LogControllerEntring();
            IHttpActionResult result = null;
            try
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
                    result = Ok(userToReturn);
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
