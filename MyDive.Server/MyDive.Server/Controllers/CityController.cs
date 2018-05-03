using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyDive.Server.Controllers
{
    public class CityController : ApiController
    {
        [HttpGet]
        [Route("city/GetCities/{i_CuntryID}")]
        public IHttpActionResult GetCitiesByCuntryID(int i_CuntryID)
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                ObjectResult<stp_GetAllCitiesByCountryId_Result> cities = MyDiveDB.stp_GetAllCitiesByCountryId(i_CuntryID);

                return Ok(cities.ToList());
            }
        }
    }
}
