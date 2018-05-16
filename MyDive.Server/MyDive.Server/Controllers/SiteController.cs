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
                ObjectResult<stp_GetSiteInfoById_Result> sitesResult = MyDiveDB.stp_GetSiteInfoById(i_SiteID);
                List<Site> sites = new List<Site>();

                foreach (stp_GetSiteInfoById_Result site in sitesResult)
                {
                    sites.Add(new Site
                    {
                        SiteID = site.SiteID,
                        Name = site.Name,
                        CityID = site.CityID,
                        CountryID = site.CountryID,
                        Polygon = site.Polygon,
                        Rating = site.Rating
                    });
                }

                return Ok(sites);
            }
        }

        [HttpGet]
        [Route("Site/GetSite/{i_CuntryID, i_CityID}")]
        public IHttpActionResult GetSiteByCuntryIDAndCityID(int i_CuntryID, int i_CityID)
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                ObjectResult<stp_GetSitesByCountryAndCity_Result> sitesResult = MyDiveDB.stp_GetSitesByCountryAndCity(i_CuntryID, i_CityID);
                List<Site> sites = new List<Site>();

                foreach (stp_GetSitesByCountryAndCity_Result site in sitesResult)
                {
                    sites.Add(new Site
                    {
                        SiteID = site.SiteID,
                        Name = site.Name,
                        CityID = site.CityID,
                        CountryID = site.CountryID,
                        Polygon = site.Polygon,
                        Rating = site.Rating
                    });
                }

                return Ok(sites);
            }
        }

        [HttpGet]
        [Route("Site/GetSite/{i_Keyword}")]
        public IHttpActionResult GetSiteByKeyword(string i_Keyword)
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                ObjectResult<stp_GetSitesByKeywors_Result> sitesResult = MyDiveDB.stp_GetSitesByKeywors(i_Keyword);
                List<Site> sites = new List<Site>();

                foreach (stp_GetSitesByKeywors_Result site in sitesResult)
                {
                    sites.Add(new Site
                    {
                        SiteID = site.SiteID,
                        Name = site.Name,
                        CityID = site.CityID,
                        CountryID = site.CountryID,
                        Polygon = site.Polygon,
                        Rating = site.Rating
                    });
                }

                return Ok(sites);
            }
        }

        [HttpPost]
        [Route("Site/Rate")]
        public IHttpActionResult RateSite([FromBody] Rating i_Rate)
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                int rateID = MyDiveDB.stp_RateSite(i_Rate.EntityID, i_Rate.Rate, i_Rate.Comment);

                return Ok(rateID);
            }
        }
    }
}
