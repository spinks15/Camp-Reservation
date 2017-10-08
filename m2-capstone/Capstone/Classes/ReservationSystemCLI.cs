﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using Capstone.DAL;
using Capstone;
using Capstone.Classes.DAL;

namespace Capstone.Classes
{
    class ReservationSystemCLI
    {

        private string connectionString;

        List<Park> pk = new List<Park>();
        List<Campground> cg = new List<Campground>();

        public ReservationSystemCLI(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void Run()
        {

            while (true)
            {
                InitialMenu();
                string choice = Console.ReadLine().ToLower();

                if (choice != "q")
                {
                    int userInput = Convert.ToInt32(choice);
                    ParkInformationMenu(userInput);
                    choice = Console.ReadLine().ToLower();

                    while (choice != "q")
                    {
                        if (choice == "1")  //view campgrounds at user chosen park
                        {
                            ParkCampgroundMenu(userInput);
                            int campgroundChoice = CLIHelper.GetInteger("") - 1;  //-1 to turn choice into index
                            MakeReservationMenu(campgroundChoice);


                        }


                    }
                }
                else if (choice == "q")
                {
                    break;
                }
            }
        }
        public void InitialMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the National Park Campsite Reservation");
            Console.WriteLine("Please select a park below for more information");
            ParkDAL park = new ParkDAL(connectionString);
            pk = park.GetParks();
            foreach (Park p in pk)
            {
                Console.WriteLine("  " + p.ParkId.ToString() + ") " + p.Name);
            }
            Console.WriteLine("  q) quit");

        }
        public void ParkInformationMenu(int userInput)
        {
            Console.Clear();
            int parkIndex = userInput - 1;
            Console.WriteLine("Park Information Screen");
            Console.WriteLine(pk[parkIndex].Name);
            Console.WriteLine("Location:".PadRight(20) + pk[parkIndex].Location);
            Console.WriteLine("Established:".PadRight(20) + pk[parkIndex].EstablishedDate);
            Console.WriteLine("Area:".PadRight(20) + pk[parkIndex].Area);
            Console.WriteLine("Annual Visitors".PadRight(20) + pk[parkIndex].Visitors);
            Console.WriteLine();
            Console.WriteLine(pk[parkIndex].Descriptions);
            Console.WriteLine("Please select a command");
            Console.WriteLine();
            Console.WriteLine("  1) View Campgrounds for this Park");
            Console.WriteLine("  q) Return to previous screen");
        }
        public void ParkCampgroundMenu(int userInput)
        {
            Console.Clear();
            int parkIndex = userInput - 1;
            Console.WriteLine("Park Campgrounds");
            Console.WriteLine(pk[parkIndex].Name + " National Park Campgrounds");
            Console.WriteLine("".PadRight(5) + "Name".PadRight(35) + "Open".PadRight(12) + "Close".PadRight(12) + "Daily Fee");
            CampgroundDAL camp = new CampgroundDAL(connectionString);
            cg = camp.GetThisParksCampgrounds(userInput);
            foreach (Campground c in cg)
            {
                Console.WriteLine(c.CampgroundId.ToString().PadRight(5) + c.Name.PadRight(35) + c.OpenFrom.ToString().PadRight(12) + c.OpenTo.ToString().PadRight(12) + (c.DailyFee / 100).ToString("C2"));
            }
            Console.WriteLine();
            Console.WriteLine("  Select a Park Number to Schedule Reservations");
            Console.WriteLine("  Or Select (Q) to Quit");
        }

        public void ParkCampgroundReservationMenu(int userInput)
        {
            Console.Clear();
            int parkIndex = userInput - 1;
            Console.WriteLine("Search for Campground Reservation");
            Console.WriteLine("".PadRight(5) + "Name".PadRight(35) + "Open".PadRight(12) + "Close".PadRight(12) + "Daily Fee");
            foreach (Campground c in cg)
            {
                Console.WriteLine(c.CampgroundId.ToString().PadRight(5) + c.Name.PadRight(35) + c.OpenFrom.ToString().PadRight(12) + c.OpenTo.ToString().PadRight(12) + (c.DailyFee / 100).ToString("C2"));
            }
            Console.WriteLine();
            Console.Write("  Which campground (enter (Q) to cancel)?  ");

        }
        public void MakeReservationMenu(int campgroundChoice)
        {


            string startDate = "";
            string endDate = "";

            startDate = CLIHelper.GetString("What day do you plan to arrive?  mm/dd/yyyy ");
            if (!DateTime.TryParse(startDate, out DateTime startResult))
            {
                Console.WriteLine("\nThat is not an acceptable Date format");
            }
            endDate = CLIHelper.GetString("What day do you plan to checkout?  mm/dd/yyyy ");
            if (!DateTime.TryParse(endDate, out DateTime endResult))
            {
                Console.WriteLine("\nThat is not an acceptable Date format");
            }
            SiteDAL siteDAL = new SiteDAL(connectionString);



        }


    }

}
