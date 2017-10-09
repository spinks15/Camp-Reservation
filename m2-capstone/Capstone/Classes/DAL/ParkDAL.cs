using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class ParkDAL
    {
        private string connectionString;

        public ParkDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Park> GetParksSQL()
        {
            List<Park> output = new List <Park>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM park ORDER BY name", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int slotid = Convert.ToInt32(reader["park_id"]);
                        string name = Convert.ToString(reader["name"]);
                        string location = Convert.ToString(reader["location"]);
                        DateTime establishDate = Convert.ToDateTime(reader["establish_date"]);
                        int area = Convert.ToInt32(reader["area"]);
                        int visitors = Convert.ToInt32(reader["visitors"]);
                        string description = Convert.ToString(reader["description"]);
                        output.Add(new Park(slotid, name, location, establishDate, area, visitors, description));
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
