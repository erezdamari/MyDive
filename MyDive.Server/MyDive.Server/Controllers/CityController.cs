using MyDive.Server.Log;
using MyDive.Server.Logic;
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
    [RoutePrefix("city")]
    public class CityController : MainController
    {
        private CityLogic m_Logic = new CityLogic();

        [HttpGet]
        [Route("getcities/{i_CountryID}")]
        public IHttpActionResult GetCitiesByCuntryID(int i_CountryID)
        {
            LogControllerEntring("getcities");
            IHttpActionResult result = Ok();
            try
            {
                result = Ok(m_Logic.GetCities(i_CountryID));
            }
            catch(Exception ex)
            {
                result = LogException(ex, null);
            }

            return result;
        }
    }
}
