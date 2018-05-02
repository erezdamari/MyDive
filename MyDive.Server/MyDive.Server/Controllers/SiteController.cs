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
    public class SiteController : ApiController
    {
        [HttpGet]
        [Route("Site/GetSite/{i_SiteID}")]
        public IHttpActionResult GetCitiesByCuntryID(int i_SiteID)
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                ObjectResult<stp_GetSiteInfoById_Result> site = MyDiveDB.stp_GetSiteInfoById(i_SiteID);

                return Ok(site);
            }
        }

        [HttpGet]
        [Route("Site/GetSite/{i_CuntryID, i_CityID}")]
        public IHttpActionResult GetSiteByCuntryIDAndCityID(int i_CuntryID, int i_CityID)
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                ObjectResult<stp_GetSitesByCountryAndCity_Result> site = MyDiveDB.stp_GetSitesByCountryAndCity(i_CuntryID, i_CityID);

                return Ok(site);
            }
        }

        [HttpGet]
        [Route("Site/GetSite/{i_Keyword}")]
        public IHttpActionResult GetSiteByKeyword(string i_Keyword)
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                ObjectResult<stp_GetSitesByKeywors_Result> site = MyDiveDB.stp_GetSitesByKeywors(i_Keyword);

                return Ok(site);
            }
        }

        [HttpPost]
        [Route("Site/Rate")]
        public IHttpActionResult RateSite(Rating i_Rate)
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                int rateID = MyDiveDB.stp_RateSite(i_Rate.EntityID, i_Rate.Rate, i_Rate.Comment);

                return Ok(rateID);
            }
        }
    }
}
