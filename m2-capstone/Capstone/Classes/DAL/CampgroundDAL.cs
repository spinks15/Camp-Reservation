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

        public List<Campground> GetThisParksCampgrounds(int thisPark)
        {
            List<Campground> output = new List<Campground>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"SELECT campground_id, name, open_from_mm, open_to_mm, daily_fee FROM campground WHERE(park_id = @thisPark) ORDER BY name", conn);
                    cmd.Parameters.AddWithValue("@thisPark", thisPark);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int slotid = Convert.ToInt32(reader["campground_id"]);
                        string name = Convert.ToString(reader["name"]);
                        int openFrom = Convert.ToInt32(reader["open_from_mm"]);
                        int openTo = Convert.ToInt32(reader["open_to_mm"]);
                        int dailyFee = 100 * Convert.ToInt32(reader["daily_fee"]); 

                        output.Add(new Campground(slotid, thisPark, name, openFrom, openTo, dailyFee));
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

