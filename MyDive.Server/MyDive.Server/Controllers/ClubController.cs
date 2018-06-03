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
    [RoutePrefix("club")]
    public class ClubController : ApiController
    {
        [HttpGet]
        [Route("getclubs/{i_CuntryID, i_CityID}")]
        public IHttpActionResult GetClubsByCuntryIDAndCityID(int i_CuntryID, int i_CityID)
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                ObjectResult<stp_GetAllClubsByCountryANDCityId_Result> clubsResult =
                    MyDiveDB.stp_GetAllClubsByCountryANDCityId(i_CuntryID, i_CityID);
                List<Club> clubs = new List<Club>();

                foreach(stp_GetAllClubsByCountryANDCityId_Result club in clubsResult)
                {
                    clubs.Add(new Club
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

                return Ok(clubs);
            }
        }

        [HttpGet]
        [Route("getclubs/{i_CuntryID}")]
        public IHttpActionResult GetClubsByCuntryID(int i_CuntryID)
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                ObjectResult<stp_GetAllClubsByCountryId_Result> clubsResult =
                    MyDiveDB.stp_GetAllClubsByCountryId(i_CuntryID);
                List<Club> clubs = new List<Club>();

                foreach (stp_GetAllClubsByCountryId_Result club in clubsResult)
                {
                    clubs.Add(new Club
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

                return Ok(clubs);
            }
        }

        [HttpGet]
        [Route("getclubs/{i_Keyword}")]
        public IHttpActionResult GetClubsByKeyword(string i_Keyword)
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                ObjectResult<stp_GetAllClubsBySearch_Result> clubsResult =
                    MyDiveDB.stp_GetAllClubsBySearch(i_Keyword);
                List<Club> clubs = new List<Club>();

                foreach (stp_GetAllClubsBySearch_Result club in clubsResult)
                {
                    clubs.Add(new Club
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

                return Ok(clubs);
            }
        }

        [HttpGet]
        [Route("getclub/{i_ClubID}")]
        public IHttpActionResult GetClubsByID(int i_ClubID)
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                ObjectResult<stp_GetClubInfo_Result> clubsResult =
                    MyDiveDB.stp_GetClubInfo(i_ClubID);
                List<Club> clubs = new List<Club>();

                foreach (stp_GetClubInfo_Result club in clubsResult)
                {
                    clubs.Add(new Club
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

                return Ok(clubs);
            }
        }

        [HttpPost]
        [Route("rate")]
        public IHttpActionResult RateClub([FromBody] Rating i_Rate)
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                int rateID = MyDiveDB.stp_RateClub(i_Rate.EntityID, i_Rate.Rate, i_Rate.Comment);

                return Ok(rateID);
            }
        }
    }
}
