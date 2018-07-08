using MyDive.Server.Log;
using MyDive.Server.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static MyDive.Server.Enums;

namespace MyDive.Server.Controllers
{
    [RoutePrefix("club")]
    public class ClubController : MainController
    {
        [HttpGet]
        [Route("getclubs/{i_CuntryID}/{i_CityID}")]
        public IHttpActionResult GetClubsByCuntryIDAndCityID(int i_CountryID, int i_CityID)
        {
            LogControllerEntring("getclubs");
            IHttpActionResult result = Ok();
            List<ClubModel> clubs;
            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    ObjectResult<stp_GetAllClubsByCountryANDCityId_Result> clubsResult =
                        MyDiveDB.stp_GetAllClubsByCountryANDCityId(i_CountryID, i_CityID);
                    clubs = new List<ClubModel>();

                    foreach (stp_GetAllClubsByCountryANDCityId_Result club in clubsResult)
                    {
                        clubs.Add(new ClubModel
                        {
                            ClubID = club.ClubID,
                            Name = club.Name,
                            Phone = club.Phone,
                            Email = club.Email,
                            Address = club.Address,
                            Rating = club.Rating,
                            SiteURL = club.SiteURL,
                            Lat = club.Lat,
                            Long = club.Long
                        });
                    }

                    Logger.Instance.Notify("Fetch all clubs", eLogType.Info,
                        i_CityID.ToString() + i_CountryID.ToString());
                    result = Ok(clubs.Count > 0 ? clubs : null);
                }
            }
            catch (Exception ex)
            {
                clubs = null;
                result = LogException(ex, null);
            }

            return result;
        }

        [HttpGet]
        [Route("getclubs/{i_CuntryID}")]
        public IHttpActionResult GetClubsByCuntryID(int i_CuntryID)
        {
            LogControllerEntring("getclubs");
            IHttpActionResult result = Ok();
            List<ClubModel> clubs;
            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    ObjectResult<stp_GetAllClubsByCountryId_Result> clubsResult =
                        MyDiveDB.stp_GetAllClubsByCountryId(i_CuntryID);
                    clubs = new List<ClubModel>();

                    foreach (stp_GetAllClubsByCountryId_Result club in clubsResult)
                    {
                        clubs.Add(new ClubModel
                        {
                            ClubID = club.ClubID,
                            Name = club.Name,
                            Phone = club.Phone,
                            Email = club.Email,
                            Address = club.Address,
                            Rating = club.Rating,
                            SiteURL = club.SiteURL,
                            Lat = club.Lat,
                            Long = club.Long
                        });
                    }

                    Logger.Instance.Notify("Fetch all clubs", eLogType.Info, i_CuntryID.ToString());
                    result = Ok(clubs.Count > 0 ? clubs : null);
                }
            }
            catch (Exception ex)
            {
                clubs = null;
                result = LogException(ex, null);
            }

            return result;
        }

        [HttpGet]
        [Route("getclubssearch/{i_Keyword}")]
        public IHttpActionResult GetClubsByKeyword(string i_Keyword)
        {
            LogControllerEntring("getclubs");
            IHttpActionResult result = Ok();
            List<ClubModel> clubs;
            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    ObjectResult<stp_GetAllClubsBySearch_Result> clubsResult =
                        MyDiveDB.stp_GetAllClubsBySearch(i_Keyword);
                    clubs = new List<ClubModel>();

                    foreach (stp_GetAllClubsBySearch_Result club in clubsResult)
                    {
                        clubs.Add(new ClubModel
                        {
                            ClubID = club.ClubID,
                            Name = club.Name,
                            Phone = club.Phone,
                            Email = club.Email,
                            Address = club.Address,
                            Rating = club.Rating,
                            SiteURL = club.SiteURL,
                            Lat = club.Lat,
                            Long = club.Long
                        });
                    }

                    Logger.Instance.Notify("Fetch all clubs", eLogType.Info, i_Keyword);
                    result = Ok(clubs.Count > 0 ? clubs : null);
                }
            }
            catch (Exception ex)
            {
                clubs = null;
                result = LogException(ex, null);
            }

            return result;
        }

        [HttpGet]
        [Route("getclub/{i_ClubID}")]
        public IHttpActionResult GetClubsByID(int i_ClubID)
        {
            LogControllerEntring("getclub");
            IHttpActionResult result = Ok();
            List<ClubModel> clubs;
            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    ObjectResult<stp_GetClubInfo_Result> clubsResult =
                        MyDiveDB.stp_GetClubInfo(i_ClubID);
                    clubs = new List<ClubModel>();

                    foreach (stp_GetClubInfo_Result club in clubsResult)
                    {
                        clubs.Add(new ClubModel
                        {
                            ClubID = club.ClubID,
                            Name = club.Name,
                            Phone = club.Phone,
                            Email = club.Email,
                            Address = club.Address,
                            Rating = club.Rating,
                            SiteURL = club.SiteURL,
                            Lat = club.Lat,
                            Long = club.Long
                        });
                    }

                    Logger.Instance.Notify("Fetch club", eLogType.Info, i_ClubID.ToString());
                    result = Ok(clubs.Count > 0 ? clubs : null);
                }
            }
            catch (Exception ex)
            {
                clubs = null;
                result = LogException(ex, null);
            }

            return result;
        }

        [HttpPost]
        [Route("rate")]
        public IHttpActionResult RateClub([FromBody] RatingModel i_Rate)
        {
            LogControllerEntring("rate");
            IHttpActionResult result = Ok();
            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    int? rateID = -1;
                    rateID = MyDiveDB.stp_RateClub(i_Rate.EntityID, i_Rate.Rate, i_Rate.Comment);

                    Logger.Instance.Notify("rete club", eLogType.Info, JsonConvert.SerializeObject(i_Rate));
                    result = Ok(rateID != -1 ? rateID : null);
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
