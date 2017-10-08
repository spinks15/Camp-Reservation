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
        

      
        public int ReservationId { get => reservationId; set => reservationId = value; }
        public int SiteId { get => siteId; set => siteId = value; }
        public string ReservationName { get => reservationName; set => reservationName = value; }
        public DateTime FromDate { get => fromDate; set => fromDate = value; }
        public DateTime ToDate { get => toDate; set => toDate = value; }
        public DateTime CreationDate { get => creationDate; set => creationDate = value; }
        

        public Reservation(int reservationId, int siteId, string reservationName, DateTime fromDate, DateTime toDate, DateTime creationDate)
        {
            this.ReservationId = reservationId;
            this.SiteId = siteId;
            this.ReservationName = reservationName;
            this.FromDate = fromDate;
            this.ToDate = toDate;
            this.CreationDate = creationDate;
            
        }

    }
}
