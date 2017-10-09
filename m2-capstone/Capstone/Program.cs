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
            
            string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;                     
            ReservationSystemCLI cli = new ReservationSystemCLI(connectionString);
            cli.Run();
        }
    }
}
