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
    [RoutePrefix("country")]
    public class CountryController : MainController
    {
        [HttpGet]
        [Route("getcounties")]
        public IHttpActionResult GetCountries()
        {
            LogControllerEntring("getcountries");
            IHttpActionResult result = Ok();
            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    ObjectResult<stp_GetAllCountries_Result> countriesResult = MyDiveDB.stp_GetAllCountries();
                    List<CountryModel> countries = new List<CountryModel>();

                    foreach (stp_GetAllCountries_Result country in countriesResult)
                    {
                        countries.Add(new CountryModel
                        {
                            CountryID = country.CountryID,
                            CountryName = country.CountryName
                        });
                    }

                    Logger.Instance.Notify("Fetch all countries", eLogType.Info);
                    result = Ok(countries.Count > 0 ? countries : null);
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
