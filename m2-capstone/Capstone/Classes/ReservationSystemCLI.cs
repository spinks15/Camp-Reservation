using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using Capstone.DAL;
using Capstone;
using Capstone.Classes.DAL;
using Microsoft.VisualBasic;
using Capstone.Classes.Models;

namespace Capstone.Classes
{
    class ReservationSystemCLI
    {

        private string connectionString;
        List<Park> allParks = new List<Park>();
        // In what is no doubt poor form I am defining this list as global variable so that I can obtain it once and not have to make another call to the DAL
        int parkIndex;

        public ReservationSystemCLI(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void Run()
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n\nWelcome to the National Park Campsite Reservation");
                Console.WriteLine("Please select a park below for more information\n\n");
                GetAndWriteAllParks();
                ParkInformationMenu();

            }
        }
        public void GetAndWriteAllParks()
        {

            ParkDAL parkDAL = new ParkDAL(connectionString);
            allParks = parkDAL.GetParksSQL();
            foreach (Park p in allParks)
            {
                Console.WriteLine("  " + p.ParkId.ToString() + ") " + p.Name);
            }
            Console.WriteLine("  0) quit");

        }
        public void ParkInformationMenu()
        {

            while (true)
            {
                string parkchoice = CLIHelper.GetString("\nSELECT ONE:  ").ToLower();

                if (parkchoice == "0")
                {
                    Console.WriteLine("\t\n Thanks for using the Parks Registration System.  Please come again!\n");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                else if (parkchoice != "0")
                {
                    if (int.TryParse(parkchoice, out int result) && Convert.ToInt32(parkchoice) > 0 && Convert.ToInt32(parkchoice) <= allParks.Count)
                    {
                        parkIndex = Convert.ToInt32(parkchoice) - 1;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("That is not a valid selection.  Try again.");
                    }
                }


            }
            bool parkMenuToggle = true;
            while (parkMenuToggle)
            {
                Console.Clear();
                Console.WriteLine("\n\nPark Information Screen\n");
                Console.WriteLine(allParks[parkIndex].Name);
                Console.WriteLine("Location:".PadRight(20) + allParks[parkIndex].Location);
                Console.WriteLine("Established:".PadRight(20) + allParks[parkIndex].EstablishedDate);
                Console.WriteLine("Area:".PadRight(20) + allParks[parkIndex].Area);
                Console.WriteLine("Annual Visitors".PadRight(20) + allParks[parkIndex].Visitors);
                Console.WriteLine();
                Console.WriteLine(allParks[parkIndex].Descriptions);

                string parkName = allParks[parkIndex].Name;
                parkMenuToggle = PickThisParkOrGoBack(parkName);
            }


        }

        public bool PickThisParkOrGoBack(string parkName)
        {
            Console.WriteLine("Please select a command");
            Console.WriteLine();
            Console.WriteLine($"  1) View Campgrounds for {parkName}");
            Console.WriteLine("  2) Look for Reservations");
            Console.WriteLine("  3) Return to previous screen");
            int commandSelection = CLIHelper.GetInteger("\nSELECT ONE:  ");

            switch (commandSelection)
            {
                case 1:
                    GetThisParksCampgrounds(parkName);
                    CaseOneSubMenu(parkName);
                    return true; ;

                case 2:
                    bool searchingMenuToggle = true;
                    while (searchingMenuToggle)
                    {
                        searchingMenuToggle = SearchForAvailableReservations(parkName);
                    }
                    return true; ;  //why does this need a second semicolon?

                case 3:
                    return false;  //returning false exits the method and returns false to the parkMenuToggle which backs up a menu

                default:
                    Console.WriteLine("That is not a valid command, try again.");
                    return true;
            }

        }
        public void GetThisParksCampgrounds(string parkName)  //and write them to the screen
        {
            List<Campground> campgroundList = new List<Campground>();
            Console.Clear();
            Console.WriteLine("Park Campgrounds");
            Console.WriteLine(allParks[parkIndex].Name + " National Park Campgrounds\n");
            Console.WriteLine("".PadRight(5) + "Name".PadRight(35) + "Open".PadRight(12) + "Close".PadRight(12) + "Daily Fee");
            CampgroundDAL campDAL = new CampgroundDAL(connectionString);
            campgroundList = campDAL.GetThisParksCampgrounds(parkName);

            for (int i = 0; i < campgroundList.Count; i++)
            {
                Console.WriteLine("#" + (i + 1) + ")".PadRight(5) + campgroundList[i].Name.PadRight(35) + DateAndTime.MonthName(campgroundList[i].OpenFrom).PadRight(12)
                    + DateAndTime.MonthName(campgroundList[i].OpenTo).PadRight(12) + (campgroundList[i].DailyFee / 100).ToString("C2"));
            }

        }
        public void CaseOneSubMenu(string parkName)
        {
            Console.WriteLine("\n");
            Console.WriteLine("Select a Command");
            Console.WriteLine("".PadRight(5) + "1) Search For Available Reservations");
            Console.WriteLine("".PadRight(5) + "2) Return to Previous Screen");
            int commandSelection = CLIHelper.GetInteger("\nSELECT ONE:  ");

            switch (commandSelection)
            {
                case 1:
                    bool searchingReservations = true;
                    while (searchingReservations)
                    {
                        searchingReservations = SearchForAvailableReservations(parkName);
                    }
                    break;

                case 2:
                    return;

                default:
                    Console.WriteLine("The command provided was not a valid command, please try again.");
                    break;
            }
        }
        public bool SearchForAvailableReservations(string parkName)
        {

            GetThisParksCampgrounds(parkName);
            CampgroundDAL campDAL = new CampgroundDAL(connectionString);
            List<Campground> thisParksCampgroundList = campDAL.GetThisParksCampgrounds(parkName);

            int campgroundIndex = 0;
            string startDate = "";
            string endDate = "";

            campgroundIndex = CLIHelper.GetInteger("\nSelect a campground (enter 0 to cancel)  ___ ") - 1;
            if (campgroundIndex == -1)
            {
                return false;  //returns to previous method (and menu)
            }
            else if (campgroundIndex < 0 || campgroundIndex > thisParksCampgroundList.Count - 1)
            {
                Console.WriteLine("\nSelect a valid campground from the list.");
                Console.ReadLine();
                return true;
            }

            startDate = CLIHelper.GetString("What day do you plan to arrive?  mm/dd/yyyy ");
            if (!DateTime.TryParse(startDate, out DateTime startResult))
            {
                Console.WriteLine("\nThat is not an acceptable Date format");
                Console.ReadLine();
                return true;
            }

            endDate = CLIHelper.GetString("What day do you plan to checkout?  mm/dd/yyyy ");
            if (!DateTime.TryParse(endDate, out DateTime endResult))
            {
                Console.WriteLine("\nThat is not an acceptable Date format");
                Console.ReadLine();
            }
            else if (endResult < startResult)
            {
                Console.WriteLine("\nThat departure date is before the Arrival Date. Try again.");
                Console.ReadLine();
                return true;
            }

            SiteDAL siteDAL = new SiteDAL(connectionString);
            List<Site> availableCampsites = siteDAL.GetSitesFreeInCampground(thisParksCampgroundList[campgroundIndex].Name, startDate, endDate);

            Console.Clear();
            Console.Write("\n\nHere are the results matching your search criteria for:");
            Console.WriteLine("\n\nAvailable Camp Sites at " + parkName + " National Park, " + thisParksCampgroundList[campgroundIndex].Name + " Campground");
            Console.WriteLine("Site No.".PadRight(10) + "Max Occup.".PadRight(12) + "Accessible?".PadRight(15) + "Max RV Length".PadRight(15)
                + "Utility".PadRight(10) + "Cost");

            int numOfDaysStaying = (Convert.ToDateTime(endDate) - Convert.ToDateTime(startDate)).Days;

            for (int i = 0; i < availableCampsites.Count - 1; i++)
            {
                Console.WriteLine(availableCampsites[i].SiteNumber.ToString().PadRight(10) + availableCampsites[i].MaxOccupancy.ToString().PadRight(12) +
                    availableCampsites[i].HandicapAccessible.ToString().PadRight(15) + availableCampsites[i].MaxRVLength.ToString().PadRight(15) +
                    availableCampsites[i].HasUtilities.ToString().PadRight(10) + (numOfDaysStaying * thisParksCampgroundList[campgroundIndex].DailyFee / 100).ToString("C2"));
            }

            ReserveSelectedCampsite(availableCampsites, startDate, endDate);

            return false;
        }

        public void ReserveSelectedCampsite(List<Site> availableCampsites, string startDate, string endDate)
        {
            int siteNumber = 0;
            int reservationId = 0;
            int reservationSuccessToggle = 0;
            const int validCampSite = 1;

            while (reservationSuccessToggle != validCampSite)
            {
                siteNumber = CLIHelper.GetInteger("\nWhich site would you like to reserve? (or enter 0 to cancel)  ___ ");
                if (siteNumber == 0)
                {
                    return;
                }
                else
                {
                    for (int i = 0; i < availableCampsites.Count; i++)
                    {
                        if (availableCampsites[i].SiteNumber == siteNumber)
                        {
                            Console.WriteLine($"  --Selcted Site Number {availableCampsites[i].SiteNumber} for {startDate} till {endDate}-- \n");
                            reservationSuccessToggle = validCampSite;
                        }
                    }
                    if (reservationSuccessToggle != validCampSite)
                    {
                        Console.WriteLine("That site number is not found.  Please select one of the available camp sites");
                        Console.ReadLine();
                    }
                }

                string reservationName = CLIHelper.GetString("What name would you like to use for the reservation?  ___ ");

                ReservationDAL resDAL = new ReservationDAL(connectionString);
                reservationId = resDAL.CreateNewReservation(reservationName, startDate, endDate, siteNumber);

                Console.WriteLine($"\nThe reservation has been made under the name {reservationName} and the confirmaion id is {reservationId}");
                Console.ReadLine();


                return;
            }
        }


    }

}


