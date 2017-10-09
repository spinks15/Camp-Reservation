using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Classes.Models;
using System.Data.SqlClient;

namespace Capstone.Classes.DAL
{
    public class SiteDAL
    {
        private string connectionString;

        public SiteDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Site> GetSitesFreeInCampground(string campgroundName, string arrivalDate, string departDate)
        {
            List<Site> output = new List<Site>();

            try
            {
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM site " +
                                                    "JOIN campground ON site.campground_id = campground.campground_id " +
                                                    "WHERE campground.name LIKE @campgroundName AND site_id NOT IN " +
                                                    "(SELECT site_id FROM reservation WHERE " +
                                                    "from_date <= @arrivalDate AND to_date >= @departDate)", conn);
                                                                                          
                    cmd.Parameters.AddWithValue("@campgroundName", campgroundName);
                    cmd.Parameters.AddWithValue("@arrivalDate", arrivalDate);
                    cmd.Parameters.AddWithValue("@departDate", departDate);

                    //Execute query 
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Site s = new Site();
                        s.SiteId = (int)reader["site_id"];
                        s.CampgroundId = (int)reader["campground_id"];
                        s.SiteNumber = (int)reader["site_number"];
                        s.MaxOccupancy = (int)reader["max_occupancy"];
                        s.HandicapAccessible = (bool)reader["accessible"];
                        s.MaxRVLength = (int)reader["max_rv_length"];
                        s.HasUtilities = (bool)reader["utilities"];
                        output.Add(s);

                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred. " + ex.Message);
                throw;
            }

            return output;
        }
    }
}
