using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Campground
    {
        private int campgroundId;
        private int parkId;
        private string name;
        private int openFrom;
        private int openTo;
        private int dailyFee;

        public int CampgroundId { get => campgroundId; set => campgroundId = value; }
        public int ParkId { get => parkId; set => parkId = value; }
        public string Name { get => name; set => name = value; }
        public int OpenFrom { get => openFrom; set => openFrom = value;}
        public int OpenTo { get => openTo; set => openTo = value; }
        public int DailyFee { get => dailyFee; set => dailyFee = value; }

        public Campground(int campgroundId, int parkId, string name, int openFrom, int openTo, int dailyFee)
        {
            this.CampgroundId = campgroundId;
            this.ParkId = parkId;
            this.Name = name;
            this.OpenFrom = openFrom;
            this.OpenTo = openTo;
            this.DailyFee = dailyFee;
        }

        public override string ToString()
        {
            return campgroundId.ToString().PadRight(6) + name.ToString().PadRight(10) + openFrom.ToString().PadRight(30) + openTo.ToString().PadRight(10) + (dailyFee / 100.0).ToString("C2").PadRight(10);
        }
    }
}
