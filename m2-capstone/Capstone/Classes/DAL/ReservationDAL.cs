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

        internal int CreateNewReservation(string reservationName, string startDate, string endDate, int siteNumber)
        {
            int reservationId = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"INSERT into reservation (site_id, name, from_date, to_date) VALUES (@siteNumber, @reservationName, @startDate, @endDate)", conn);
                    cmd.Parameters.AddWithValue("@siteNumber", siteNumber);
                    cmd.Parameters.AddWithValue("@reservationName", reservationName);
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);

                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("SELECT MAX(reservation_id) FROM reservation;", conn);
                    reservationId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("An error occurred. " + e.Message);
                throw;
            }
            return reservationId;
        }


    }
}
