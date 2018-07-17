using MyDive.Server.Log;
using MyDive.Server.Logic;
using MyDive.Server.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Web.Http;
using static MyDive.Server.Enums;

namespace MyDive.Server.Controllers
{
    [RoutePrefix("site")]
    public class SiteController : MainController
    {
        private SiteLogic m_Logic = new SiteLogic();

        [HttpGet]
        [Route("getbycountryandcity/{i_CountryId}/{i_CityId}")]
        public IHttpActionResult GetSitesByCuntryIDAndCityID(int i_CountryId, int i_CityId)
        {
            LogControllerEntring("getsites");
            IHttpActionResult result = Ok();
            try
            {
                List<SiteModel> sites = m_Logic.GetSitesByCountryAndCityId(i_CountryId, i_CityId);
                result = Ok(sites.Count > 0 ? sites : null);
            }
            catch (Exception ex)
            {
                result = LogException(ex, null);
            }

            return result;
        }

        [HttpGet]
        [Route("getsitessearch/{i_Keyword}")]
        public IHttpActionResult GetSitesByKeyword(string i_Keyword)
        {
            LogControllerEntring("getsites");
            IHttpActionResult result = Ok();
            try
            {
                List<SiteModel> sites = m_Logic.GetSitesByKeyword(i_Keyword);
                result = Ok(sites.Count > 0 ? sites : null);
            }
            catch (Exception ex)
            {
                result = LogException(ex, null);
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
                    int? rateID = -1;
                    rateID = MyDiveDB.stp_RateSite(i_Rate.EntityID, i_Rate.Rate, i_Rate.Comment);

                    LogData("Rate sites", i_Rate);
                    result = Ok(rateID != -1 ? rateID : null);
                }
            }
            catch (Exception ex)
            {
                result = LogException(ex, null);
            }

            return result;
        }

        [HttpGet]
        [Route("pictures/{i_SiteId}")]
        public IHttpActionResult GetSitePictures(int i_SiteId)
        {
            LogControllerEntring("pictures");
            IHttpActionResult result = Ok();
            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    var serverAnswer = MyDiveDB.stp_GetSitePictures(i_SiteId);
                    List<PictureModel> sitePictures = new List<PictureModel>();
                    foreach (stp_GetSitePictures_Result res in serverAnswer)
                    {
                        sitePictures.Add(new PictureModel
                        {
                            Picture = res.Picture,
                            PictureType = (ePictureType)res.PictureType
                        });
                    }

                    LogData("Fetch site pictures", i_SiteId);
                    result = Ok(sitePictures.Count > 0 ? sitePictures : null);
                }
            }
            catch (Exception ex)
            {
                result = LogException(ex, null);
            }

            return result;
        }
    }
}
