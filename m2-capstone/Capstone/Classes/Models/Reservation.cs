using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Reservation
    {
        private int reservationId;
        private int siteId;
        private string reservationName;
        private DateTime fromDate;
        private DateTime toDate;
        private DateTime creationDate;
        private int campgroundId;
        private int siteNumber;
        private int maxOccupancy;
        private bool accessible;
        private int maxRvLength;
        private bool utiliiesAvailable;

      
        public int ReservationId { get => reservationId; set => reservationId = value; }
        public int SiteId { get => siteId; set => siteId = value; }
        public string ReservationName { get => reservationName; set => reservationName = value; }
        public DateTime FromDate { get => fromDate; set => fromDate = value; }
        public DateTime ToDate { get => toDate; set => toDate = value; }
        public DateTime CreationDate { get => creationDate; set => creationDate = value; }
        public int CampgroundId { get => campgroundId; set => campgroundId = value; }
        public int SiteNumber { get => siteNumber; set => siteNumber = value; }
        public int MaxOccupancy { get => maxOccupancy; set => maxOccupancy = value; }
        public bool Accessible { get => accessible; set => accessible = value; }
        public int MaxRvLength { get => maxRvLength; set => maxRvLength = value; }
        public bool UtiliiesAvailable { get => utiliiesAvailable; set => utiliiesAvailable = value; }

        public Reservation(int reservationId, int siteId, string reservationName, DateTime fromDate, DateTime toDate, DateTime creationDate, int campgroundId, int siteNumber, int maxOccupancy, bool accessible, int maxRvLength, bool utiliies)
        {
            this.ReservationId = reservationId;
            this.SiteId = siteId;
            this.ReservationName = reservationName;
            this.FromDate = fromDate;
            this.ToDate = toDate;
            this.CreationDate = creationDate;
            this.CampgroundId = campgroundId;
            this.SiteNumber = siteNumber;
            this.MaxOccupancy = maxOccupancy;
            this.Accessible = accessible;
            this.MaxRvLength = maxRvLength;
            this.UtiliiesAvailable = UtiliiesAvailable;
        }

    }
}
