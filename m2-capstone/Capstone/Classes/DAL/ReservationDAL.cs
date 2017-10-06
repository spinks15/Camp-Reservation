using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class ReservationDAL
    {
        private string connectionString;

        public ReservationDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Reservation> GetParks()
        {
            List<Reservation> output = new List<Reservation>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM reservation JOIN site ON reservation.site_id = site.site_id", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int reservationId = Convert.ToInt32(reader["reservation_id"]);
                        int siteId = Convert.ToInt32(reader["site_id"]);
                        string name = Convert.ToString(reader["name"]);
                        DateTime fromDate = Convert.ToDateTime(reader["from_date"]);
                        DateTime toDate = Convert.ToDateTime(reader["to_date"]);
                        DateTime createDate = Convert.ToDateTime(reader["create_date"]);
                        siteId = Convert.ToInt32(reader["site_id"]);
                        int campgroundId = Convert.ToInt32(reader["campground_id"]);
                        int siteNumber = Convert.ToInt32(reader["site_number"]);
                        int maxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
                        bool accessible = Convert.ToBoolean(reader["accessible"]);
                        int maxRvLength = Convert.ToInt32(reader["max_rv_length"]);
                        bool utilities = Convert.ToBoolean(reader["utilites"]);
                        output.Add(new Reservation(reservationId, siteId, name, fromDate, toDate, createDate, campgroundId, siteNumber, maxOccupancy, accessible, maxRvLength, utilities));
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
