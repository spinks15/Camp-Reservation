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

        public List<Site> GetSitesFreeInCampground(string campgroundIndex, string arrivalDate, string departDate)
        {
            List<Site> output = new List<Site>();

            try
            {
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"SELECT TOP 5 * FROM site " +
                                                    "INNER JOIN campground ON site.campground_id = campground.campground_id " +
                                                    "WHERE campground.name = 'Blackwoods' AND site_id NOT IN " +
                                                    "(SELECT site_id FROM reservation WHERE " +
                                                    "from_date BETWEEN '06/06/2017' AND '06/08/2017' OR " +
                                                    "to_date BETWEEN '06/06/2017' AND '06/08/2017' OR " +
                                                    "(from_date BETWEEN '06/06/2017' AND '06/08/2017' AND to_date BETWEEN '06/06/2017' AND '06/08/2017') " +
                                                    "OR from_date< '06/06/2017' AND to_date > '06/08/2017');", conn);

                   
                    cmd.Parameters.AddWithValue("@campgroundName", campgroundIndex);
                    cmd.Parameters.AddWithValue("@arrivalDate", arrivalDate);
                    cmd.Parameters.AddWithValue("@departDate", departDate);

                    //Execute query 
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        output.Add(PopulateSite(reader));
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
