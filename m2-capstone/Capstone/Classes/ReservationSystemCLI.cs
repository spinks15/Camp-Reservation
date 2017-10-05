using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using Capstone.DAL;
using Capstone;

namespace Capstone.Classes
{
    class ReservationSystemCLI
    {
        private const string Option_DisplayAllParks = "1";
        private const string Option_DisplayAllCampgrounds = "2";

        private const string Option_Quit = "q";
        private ReservationSystem rs;

        public ReservationSystemCLI(ReservationSystem rs)
        {
            this.rs = rs;
        }
        public void Run()
        {
            while (true)
            {
                Console.WriteLine("(1) Display all Parks");
                Console.WriteLine("(2) Display all Campgrounds");
                Console.WriteLine("(Q) Quit");
                Console.Write("Please make a choice: ");

                string choice = Console.ReadLine().ToLower();

                if (choice == Option_DisplayAllParks)
                {
                    ParkDAL camp = new ParkDAL(rs.ConnectionString);
                    List<Park> pk = camp.GetParks();
                    foreach (Park p in pk)
                    {
                        Console.WriteLine(p.ToString());
                    }

                    Console.ReadKey();
                }
                else if (choice == Option_DisplayAllCampgrounds)
                {

                }
            }
        }


    }
}
