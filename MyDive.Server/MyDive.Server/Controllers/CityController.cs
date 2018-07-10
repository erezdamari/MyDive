using MyDive.Server.Logic;
using System;
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
