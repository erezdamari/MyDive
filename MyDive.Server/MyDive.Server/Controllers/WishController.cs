using MyDive.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyDive.Server.Controllers
{
    [RoutePrefix("wish")]
    public class WishController : ApiController
    {
        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreateWish([FromBody] Wish i_Wish)
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                int wishID = MyDiveDB.stp_CreateNewWish(i_Wish.SiteID, i_Wish.UserID);

                return Ok(wishID);
            }
        }

        [HttpPost]
        [Route("remove")]
        public IHttpActionResult RemoveWish([FromBody] Wish i_Wish)
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                int wishID = MyDiveDB.stp_RemoveFromWishList(i_Wish.UserID, i_Wish.SiteID);

                return Ok(wishID);
            }
        }
    }
}
