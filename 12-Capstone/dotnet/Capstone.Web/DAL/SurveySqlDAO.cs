using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class SurveySqlDAO : ISurveyDAO
    {
        private string connectionString;
        public SurveySqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public bool SaveNewSurvey(Survey survey)
        {
             bool isSuccessful = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO survey_result(parkCode,emailAddress,state,activityLevel) VALUES (@parkCode, @emailAddress, @state, @activityLevel)", conn);

                    cmd.Parameters.AddWithValue("@parkCode", survey.ParkCode);
                    cmd.Parameters.AddWithValue("@emailAddress", survey.EmailAddress);
                    cmd.Parameters.AddWithValue("@state", survey.State);
                    cmd.Parameters.AddWithValue("@activityLevel", survey.ActivityLevel);

                    int result = cmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        isSuccessful = true;
                    } 
                    
                }
                

            }
            catch (Exception)
            {

                throw;
            }
            return isSuccessful;
        }

        public IList<SurveyResultsModel> GetSurveys()
        {
            IList<SurveyResultsModel> surveys = new List<SurveyResultsModel>();
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(survey_result.parkCode) as surveyCount, park.parkName, park.state, park.parkDescription, park.annualVisitorCount FROM survey_result JOIN park ON park.parkCode = survey_result.parkCode GROUP BY park.parkCode, park.parkName, park.state, park.parkDescription, park.annualVisitorCount ORDER BY COUNT(survey_result.parkCode) DESC, park.parkName ASC", conn);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var survey = new SurveyResultsModel()
                        {
                            SurveyCount = Convert.ToInt32(reader["surveyCount"]),
                            ParkName = Convert.ToString(reader["parkName"]),
                            State = Convert.ToString(reader["state"]),
                            ParkDescription = Convert.ToString(reader["parkDescription"]),
                            AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]),
                            
                        };
                        surveys.Add(survey);
                    }
                }
                return surveys;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public IList<Park> GetParkNames()
        {
            IList<Park> parks = new List<Park>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT parkCode, parkName FROM park", conn);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var park = new Park()
                        {
                            ParkCode = Convert.ToString(reader["parkCode"]),
                            ParkName = Convert.ToString(reader["parkName"]),
                            
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


    }
}
