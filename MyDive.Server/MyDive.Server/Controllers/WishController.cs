﻿using MyDive.Server.Models;
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
        public IHttpActionResult CreateWish([FromBody] WishModel i_Wish)
        {
            LogControllerEntring("create");
            IHttpActionResult result = null;

            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    int? wishID = -1;
                    wishID = MyDiveDB.stp_CreateNewWish(i_Wish.SiteID, i_Wish.UserID);
                    result =  Ok(wishID != -1 ? wishID : null);
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
        public IHttpActionResult RemoveWish([FromBody] WishModel i_Wish)
        {
            LogControllerEntring("remove");
            IHttpActionResult result = null;

            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    int? wishID = -1;
                    wishID = MyDiveDB.stp_RemoveFromWishList(i_Wish.UserID, i_Wish.SiteID);
                    result =  Ok(wishID != -1 ? wishID : null);
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
            LogControllerEntring("getuserwish");
            IHttpActionResult result = null;
            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    ObjectResult<stp_GetUserWishList_Result> serverResult = MyDiveDB.stp_GetUserWishList(i_UserId);
                    List<UserWishListModel> userWishList = new List<UserWishListModel>();

                    foreach (stp_GetUserWishList_Result res in serverResult)
                    {
                        userWishList.Add(new UserWishListModel
                        {
                            WishID = res.WishID,
                            SiteID = res.SiteID,
                            UserID = res.UserID
                        });
                    }
                    result = Ok(userWishList.Count > 0 ? userWishList : null);
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
