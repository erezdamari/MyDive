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
    public class ClubController : ApiController
    {
        [HttpGet]
        [Route("Club/GetClubs/{i_CuntryID, i_CityID}")]
        public IHttpActionResult GetClubsByCuntryIDAndCityID(int i_CuntryID, int i_CityID)
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                ObjectResult<stp_GetAllClubsByCountryANDCityId_Result> clubs =
                    MyDiveDB.stp_GetAllClubsByCountryANDCityId(i_CuntryID, i_CityID);

                return Ok(clubs);
            }
        }

        [HttpGet]
        [Route("Club/GetClubs/{i_CuntryID}")]
        public IHttpActionResult GetClubsByCuntryID(int i_CuntryID)
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                ObjectResult<stp_GetAllClubsByCountryId_Result> clubs =
                    MyDiveDB.stp_GetAllClubsByCountryId(i_CuntryID);

                return Ok(clubs);
            }
        }

        [HttpGet]
        [Route("Club/GetClubs/{i_Keyword}")]
        public IHttpActionResult GetClubsByKeyword(string i_Keyword)
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                ObjectResult<stp_GetAllClubsBySearch_Result> clubs =
                    MyDiveDB.stp_GetAllClubsBySearch(i_Keyword);

                return Ok(clubs);
            }
        }

        [HttpGet]
        [Route("Club/GetClub/{i_ClubID}")]
        public IHttpActionResult GetClubsByKeyword(int i_ClubID)
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                ObjectResult<stp_GetClubInfo_Result> club =
                    MyDiveDB.stp_GetClubInfo(i_ClubID);

                return Ok(club);
            }
        }

        [HttpPost]
        [Route("Club/Rate")]
        public IHttpActionResult RateClub(Rating i_Rate)
        {
            using (MyDiveEntities MyDiveDB = new MyDiveEntities())
            {
                int rateID = MyDiveDB.stp_RateClub(i_Rate.EntityID, i_Rate.Rate, i_Rate.Comment);

                return Ok(rateID);
            }
        }
    }
}
