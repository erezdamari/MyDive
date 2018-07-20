using MyDive.Server.Log;
using MyDive.Server.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using static MyDive.Server.Enums;

namespace MyDive.Server.Logic
{
    public class SiteLogic
    {
        public List<SiteModel> GetSitesByCountryAndCityId(int i_CountryId, int i_CityId)
        {
            List<SiteModel> sites = new List<SiteModel>();
            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    
                    ObjectResult<stp_GetSitesByCountryAndCity_Result> sitesResult = MyDiveDB.stp_GetSitesByCountryAndCity(i_CountryId, i_CityId);
                    

                    foreach (stp_GetSitesByCountryAndCity_Result site in sitesResult)
                    {
                        ObjectResult<stp_GetSiteCoordinates_Result> coordinates = MyDiveDB.stp_GetSiteCoordinates(site.SiteID);
                        sites.Add(new SiteModel
                        {
                            SiteID = site.SiteID,
                            Name = site.Name,
                            Rating = site.Rating,
                            Coordinates = getCoordinates(coordinates)
                        });
                    }

                    Logger.Instance.Notify("Fetch sites", eLogType.Info, i_CityId.ToString() + " " + i_CountryId.ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Notify(ex.StackTrace, eLogType.Error, i_CityId.ToString() + " " + i_CountryId.ToString());
            }

            return sites;
        }

        public static List<LocationModel> getCoordinates(ObjectResult<stp_GetSiteCoordinates_Result> i_CoordnatesList)
        {
            return i_CoordnatesList.Select(c => new LocationModel { Lat = c.Lat, Long = c.Long, Place = c.Place }).ToList().OrderBy(c => c.Place).ToList();
        }

        public List<SiteModel> GetSitesByKeyword(string i_Keyword)
        {
            List<SiteModel> sites = new List<SiteModel>();
            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {

                    ObjectResult<stp_GetSitesByKeywors_Result> sitesResult = MyDiveDB.stp_GetSitesByKeywors(i_Keyword);

                    foreach (stp_GetSitesByKeywors_Result site in sitesResult)
                    {
                        ObjectResult<stp_GetSiteCoordinates_Result> coordinates = MyDiveDB.stp_GetSiteCoordinates(site.SiteID);
                        sites.Add(new SiteModel
                        {
                            SiteID = site.SiteID,
                            Name = site.Name,
                            Rating = site.Rating,
                            Coordinates = getCoordinates(coordinates)
                        });
                    }

                    Logger.Instance.Notify("Fetch sites", eLogType.Info, i_Keyword);
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Notify(ex.StackTrace, eLogType.Error, i_Keyword);
            }

            return sites;
        }
    }
}