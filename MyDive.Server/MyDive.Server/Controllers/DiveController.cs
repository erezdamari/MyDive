using MyDive.Server.Log;
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
    [RoutePrefix("dive")]
    public class DiveController : MainController
    {
        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreateDiveLog([FromBody] DiveLogModel i_Dive)
        {
            LogControllerEntring("create");
            IHttpActionResult result = Ok();
            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    int? diveID = -1;
                    diveID = MyDiveDB.stp_CreateDiveLog
                        (i_Dive.SiteID,
                        i_Dive.MaxDepth,
                        i_Dive.Description,
                        i_Dive.DiveTypeID,
                        i_Dive.UserID,
                        i_Dive.BottomTypeID,
                        i_Dive.SalinityID,
                        i_Dive.WaterTypeID,
                        i_Dive.Location.Lat,
                        i_Dive.Location.Long);

                    Logger.Instance.Notify("Dive log created", eLogType.Info);
                    result = Ok(diveID != -1 ? diveID : null);
                }
            }
            catch(Exception ex)
            {
                result = LogException(ex);
            }

            return result;
        }
    }
}
