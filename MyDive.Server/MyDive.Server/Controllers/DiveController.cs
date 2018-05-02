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
    public class DiveController : ApiController
    {
        [HttpPost]
        [Route("Dive/Create")]
        public IHttpActionResult CreateDiveLog(DiveLog i_Dive)
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                int diveID = MyDiveDB.stp_CreateDiveLog
                    (i_Dive.SiteID,
                    i_Dive.MaxDepth,
                    i_Dive.Description,
                    i_Dive.DiveTypeID,
                    i_Dive.UserID,
                    i_Dive.BottomTypeID,
                    i_Dive.SalinityID,
                    i_Dive.WaterTypeID,
                    i_Dive.Location);

                return Ok(diveID);
            }
        }

        [HttpGet]
        [Route("Dive/GetTypes")]
        public IHttpActionResult GetFiveTypes()
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                ObjectResult<stp_GetDiveTypes_Result> diveTypes = MyDiveDB.stp_GetDiveTypes();

                return Ok(diveTypes);
            }
        }
    }
}
