using MyDive.Server.Log;
using MyDive.Server.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace MyDive.Server.Logic
{
    public class CityLogic
    {
        public List<CityModel> GetCities(int i_CountryId)
        {
            List<CityModel> cities = null;
            try
            {
                using (MyDiveEntities MyDiveDB = new MyDiveEntities())
                {
                    ObjectResult<stp_GetAllCitiesByCountryId_Result> citiesResult = MyDiveDB.stp_GetAllCitiesByCountryId(i_CountryId);
                    cities = new List<CityModel>();

                    foreach (stp_GetAllCitiesByCountryId_Result city in citiesResult)
                    {
                        cities.Add(new CityModel
                        {
                            CityID = city.CityID,
                            CityName = city.CityName
                        });
                    }

                    Logger.Instance.Notify("Get all cities", eLogType.Info);
                }
            }
            catch (Exception ex)
            {
                cities = null;
            }

            return cities;
        }
    }
}