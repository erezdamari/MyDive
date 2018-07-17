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
        [Route("getclubsbycountryandcity/{i_CuntryID}/{i_CityID}")]
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
                            Coordinates = new LocationModel { Lat = club.Lat, Long = club.Long}
                        });
                    }

                    LogData("Fetch all clubs", i_CityID.ToString() + i_CountryID.ToString());
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
        [Route("getclubsbycountry/{i_CuntryID}")]
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
                            Coordinates = new LocationModel { Lat = club.Lat, Long = club.Long }
                        });
                    }

                    LogData("Fetch all clubs", i_CuntryID.ToString());
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
                            Coordinates = new LocationModel { Lat = club.Lat, Long = club.Long }
                        });
                    }

                    LogData("Fetch all clubs", i_Keyword);
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

                    LogData("rete club", i_Rate);
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
        [Route("pictures/{i_ClubId}")]
        public IHttpActionResult GetClubPictures(int i_ClubId)
        {
            LogControllerEntring("pictures");
            IHttpActionResult result = Ok();
            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    var serverAnswer = MyDiveDB.stp_GetClubPictures(i_ClubId);
                    List<PictureModel> clubPictures = new List<PictureModel>();
                    foreach(stp_GetClubPictures_Result res in serverAnswer)
                    {
                        clubPictures.Add(new PictureModel
                        {
                            Picture = res.Picture,
                            PictureType = (ePictureType)res.PictureType
                        });
                    }

                    LogData("Fetch club pictures", i_ClubId);
                    result = Ok(clubPictures.Count > 0 ? clubPictures : null);
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
