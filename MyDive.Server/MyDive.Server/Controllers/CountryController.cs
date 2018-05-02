using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyDive.Server.Controllers
{
    public class CountryController : ApiController
    {
        [HttpGet]
        [Route("Country/GetCounties")]
        public IHttpActionResult GetCountries()
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                ObjectResult<stp_GetAllCountries_Result> countries = MyDiveDB.stp_GetAllCountries();

                return Ok(countries);
            }
        }
    }
}
