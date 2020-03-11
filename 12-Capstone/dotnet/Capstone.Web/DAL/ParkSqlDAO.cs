using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class ParkSqlDAO : IParkDAO
    {
        private string connectionString;
        public ParkSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public Park GetPark(string parkCode)
        {
            return GetAllParks().FirstOrDefault(p => p.ParkCode == parkCode);
        }

        public IList<Park> GetAllParks()
        {
            IList<Park> parks = new List<Park>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT parkCode, parkName, state, parkDescription FROM park", conn);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var park = new Park()
                        {
                            ParkCode = Convert.ToString(reader["parkCode"]),
                            ParkName = Convert.ToString(reader["parkName"]),
                            State = Convert.ToString(reader["state"]),
                            ParkDescription = Convert.ToString(reader["parkDescription"]),
                        };
                        parks.Add(park);
                    }
                }
                return parks;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Park GetParkDetails(string parkCode)
        {
            var park = new Park();


            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM park WHERE parkCode = @parkCode", conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {


                        park.ParkCode = Convert.ToString(reader["parkCode"]);
                        park.ParkName = Convert.ToString(reader["parkName"]);
                        park.State = Convert.ToString(reader["state"]);
                        park.Acreage = Convert.ToInt32(reader["acreage"]);
                        park.ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]);
                        park.MilesOfTrail = Convert.ToInt32(reader["milesOfTrail"]);
                        park.NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]);
                        park.Climate = Convert.ToString(reader["climate"]);
                        park.YearFounded = Convert.ToInt32(reader["yearFounded"]);
                        park.AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]);
                        park.InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]);
                        park.InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
                        park.ParkDescription = Convert.ToString(reader["parkDescription"]);
                        park.EntryFee = Convert.ToInt32(reader["entryFee"]);
                        park.NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);


                    }

                }

            }
            catch (Exception)
            {
                throw;
            }
            return park;
        }
        public IList<Weather> FiveDayForecast(string parkCode)
        {
            List<Weather> weathers = new List<Weather>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM weather WHERE parkCode = @parkCode", conn);
                    
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);
                    var reader = cmd.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        var weather = new Weather();
                        weather.FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]);
                        weather.LowTemp = Convert.ToInt32(reader["low"]);
                        weather.HighTemp = Convert.ToInt32(reader["high"]);
                        weather.Forecast = Convert.ToString(reader["forecast"]);
                        weathers.Add(weather);
                    }
                    

                }

            }
            catch (Exception)
            {
                throw;
            }
            return weathers;    
        }



    }

}


