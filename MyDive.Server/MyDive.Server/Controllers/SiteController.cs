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
    [RoutePrefix("size")]
    public class SiteController : MainController
    {
        [HttpGet]
        [Route("getsites/{i_SiteID}")]
        public IHttpActionResult GetSitesByCuntryID(int i_SiteID)
        {
            LogControllerEntring("getsites");
            IHttpActionResult result = Ok();
            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    ObjectResult<stp_GetSiteInfoById_Result> sitesResult = MyDiveDB.stp_GetSiteInfoById(i_SiteID);
                    List<SiteModel> sites = new List<SiteModel>();

                    foreach (stp_GetSiteInfoById_Result site in sitesResult)
                    {
                        sites.Add(new SiteModel
                        {
                            SiteID = site.SiteID,
                            Name = site.Name,
                            CityID = site.CityID,
                            CountryID = site.CountryID,
                            Rating = site.Rating,
                            Lat = site.Lat,
                            Long = site.Long
                        });
                    }

                    Logger.Instance.Notify("Fetch sites", eLogType.Info);
                    result = Ok(sites);
                }
            }
            catch (Exception ex)
            {
                result = LogException(ex);
            }

            return result;
        }

        [HttpGet]
        [Route("getsites/{i_CuntryID, i_CityID}")]
        public IHttpActionResult GetSitesByCuntryIDAndCityID(int i_CuntryID, int i_CityID)
        {
            LogControllerEntring("getsites");
            IHttpActionResult result = Ok();
            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    ObjectResult<stp_GetSitesByCountryAndCity_Result> sitesResult = MyDiveDB.stp_GetSitesByCountryAndCity(i_CuntryID, i_CityID);
                    List<SiteModel> sites = new List<SiteModel>();

                    foreach (stp_GetSitesByCountryAndCity_Result site in sitesResult)
                    {
                        sites.Add(new SiteModel
                        {
                            SiteID = site.SiteID,
                            Name = site.Name,
                            CityID = site.CityID,
                            CountryID = site.CountryID,
                            Rating = site.Rating,
                            Lat = site.Lat,
                            Long = site.Long
                        });
                    }

                    Logger.Instance.Notify("Fetch sites", eLogType.Info);
                    result = Ok(sites);
                }
            }
            catch (Exception ex)
            {
                result = LogException(ex);
            }

            return result;
        }

        [HttpGet]
        [Route("getsites/{i_Keyword}")]
        public IHttpActionResult GetSitesByKeyword(string i_Keyword)
        {
            LogControllerEntring("getsites");
            IHttpActionResult result = Ok();
            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    ObjectResult<stp_GetSitesByKeywors_Result> sitesResult = MyDiveDB.stp_GetSitesByKeywors(i_Keyword);
                    List<SiteModel> sites = new List<SiteModel>();

                    foreach (stp_GetSitesByKeywors_Result site in sitesResult)
                    {
                        sites.Add(new SiteModel
                        {
                            SiteID = site.SiteID,
                            Name = site.Name,
                            CityID = site.CityID,
                            CountryID = site.CountryID,
                            Rating = site.Rating,
                            Lat = site.Lat,
                            Long = site.Long
                        });
                    }

                    Logger.Instance.Notify("Fetch sites", eLogType.Info);
                    result = Ok(sites);
                }
            }
            catch (Exception ex)
            {
                result = LogException(ex);
            }

            return result;
        }

        [HttpPost]
        [Route("rate")]
        public IHttpActionResult RateSite([FromBody] RatingModel i_Rate)
        {
            LogControllerEntring("rate");
            IHttpActionResult result = Ok();
            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    int rateID = MyDiveDB.stp_RateSite(i_Rate.EntityID, i_Rate.Rate, i_Rate.Comment);

                    Logger.Instance.Notify("Rate sites", eLogType.Info);
                    result = Ok(rateID);
                }
            }
            catch (Exception ex)
            {
                result = LogException(ex);
            }

            return result;
        }
    }
}
