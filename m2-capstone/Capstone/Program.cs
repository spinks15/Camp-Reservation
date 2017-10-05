using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Classes;
using Capstone.Interfaces;
using System.Data.SqlClient;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            // Sample Code to get a connection string from the
            // App.Config file
            // Use this so that you don't need to copy your connection string all over your code!
            string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
            //IDatasource campgroundDatabaseSource = new IDatasource(connectionString);
            //IDataSource datasource = new SqlServerDataSource();

            ReservationSystem rs;

            try
            {
                rs = new ReservationSystem(connectionString);
            }
            catch
            {
                Console.WriteLine();
                Console.WriteLine("An error has occurred in starting the campground reservation system.  The program will now close.");
                return;
            }

            ReservationSystemCLI cli = new ReservationSystemCLI(rs);
            cli.Run();
        }
    }
}
