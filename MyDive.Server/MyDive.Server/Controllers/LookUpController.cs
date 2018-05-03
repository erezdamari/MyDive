using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyDive.Server.Controllers
{
    public class LookUpController : ApiController
    {
        [HttpGet]
        [Route("bottom")]
        public IHttpActionResult GetBottomTypes()
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                ObjectResult<stp_GetBottomType_Result> bottomTypes = MyDiveDB.stp_GetBottomType();

                return Ok(bottomTypes);
            }
        }

        [HttpGet]
        [Route("salinity")]
        public IHttpActionResult GetSalinityTypes()
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                ObjectResult<stp_GetSalinityTypes_Result> salinityTypes = MyDiveDB.stp_GetSalinityTypes();

                return Ok(salinityTypes);
            }
        }

        [HttpGet]
        [Route("water")]
        public IHttpActionResult GetWaterTypes()
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                ObjectResult<stp_GetWaterTypes_Result> waterTypes = MyDiveDB.stp_GetWaterTypes();

                return Ok(waterTypes);
            }
        }
    }
}
