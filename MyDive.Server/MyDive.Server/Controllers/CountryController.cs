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
    public class CountryController : ApiController
    {
        [HttpGet]
        [Route("country/GetCounties")]
        public IHttpActionResult GetCountries()
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                ObjectResult<stp_GetAllCountries_Result> countriesResult = MyDiveDB.stp_GetAllCountries();
                List<Country> countries = new List<Country>();

                foreach (stp_GetAllCountries_Result country in countriesResult)
                {
                    countries.Add(new Country
                    {
                        CountryID = country.CountryID,
                        CountryName = country.CountryName
                    });
                }

                return Ok(countries);
            }
        }
    }
}
