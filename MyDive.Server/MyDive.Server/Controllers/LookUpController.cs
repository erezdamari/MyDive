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
    public class LookUpController : ApiController
    {
        [HttpGet]
        [Route("bottom")]
        public IHttpActionResult GetBottomTypes()
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                ObjectResult<stp_GetBottomType_Result> bottomTypesResult = MyDiveDB.stp_GetBottomType();
                List<BottomType> bottomTypes = new List<BottomType>();

                foreach (stp_GetBottomType_Result type in bottomTypesResult)
                {
                    bottomTypes.Add(new BottomType
                    {
                        BottomTypeID = type.BottomTypeID,
                        BottomType = type.BottomType
                    });
                }

                return Ok(bottomTypes);
            }
        }

        [HttpGet]
        [Route("salinity")]
        public IHttpActionResult GetSalinityTypes()
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                ObjectResult<stp_GetSalinityTypes_Result> salinityTypesResult = MyDiveDB.stp_GetSalinityTypes();
                List<SalinityType> salinityTypes = new List<SalinityType>();

                foreach (stp_GetSalinityTypes_Result type in salinityTypesResult)
                {
                    salinityTypes.Add(new SalinityType
                    {
                        SalinityID = type.SalinityID,
                        Salinity = type.Salinity
                    });
                }

                return Ok(salinityTypes);
            }
        }

        [HttpGet]
        [Route("water")]
        public IHttpActionResult GetWaterTypes()
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                ObjectResult<stp_GetWaterTypes_Result> waterTypesResult = MyDiveDB.stp_GetWaterTypes();
                List<WaterType> waterTypes = new List<WaterType>();

                foreach (stp_GetWaterTypes_Result type in waterTypesResult)
                {
                    waterTypes.Add(new WaterType
                    {
                        WaterTypeID = type.WaterTypeID,
                        WaterType = type.WaterType
                    });
                }

                return Ok(waterTypes);
            }
        }
    }
}
