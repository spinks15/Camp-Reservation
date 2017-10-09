using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class CampgroundDAL
    {
        private string connectionString;

        public CampgroundDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Campground> GetThisParksCampgrounds(string thisParkName)
        {
            List<Campground> output = new List<Campground>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"SELECT campground_id, campground.park_id, campground.name, open_from_mm, open_to_mm, daily_fee FROM campground join park on campground.park_id=park.park_id "+
                                                    "WHERE park.name = @thisParkName ORDER BY campground.name", conn);
                    cmd.Parameters.AddWithValue("@thisParkName", thisParkName);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int campgroundId = Convert.ToInt32(reader["campground_id"]);
                        int parkId = Convert.ToInt32(reader["park_id"]);
                        string campgroundName = Convert.ToString(reader["name"]);
                        int openFrom = Convert.ToInt32(reader["open_from_mm"]);
                        int openTo = Convert.ToInt32(reader["open_to_mm"]);
                        int dailyFee = 100 * Convert.ToInt32(reader["daily_fee"]);   //converting to pennies

                        output.Add(new Campground(campgroundId, parkId, campgroundName, openFrom, openTo, dailyFee));
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred reading the park table. " + ex.Message);
                throw ex;
            }

            return output;
        }
    }
}

