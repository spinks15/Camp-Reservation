using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class ReservationSystem
    {
        private string connectionString;

        public string ConnectionString { get => connectionString;}

        public ReservationSystem(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}
