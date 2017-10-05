using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Reservation
    {
        private int reservation_id;
        private int site_id;
        private string reservieName;
        private DateTime fromDate;
        private DateTime toDate;
        private DateTime creationDate;
        private string reservationCode;

        public int Reservation_id { get => reservation_id; set => reservation_id = value; }
        public int Site_id { get => site_id; set => site_id = value; }
        public string ReservieName { get => reservieName; set => reservieName = value; }
        public DateTime FromDate { get => fromDate; set => fromDate = value; }
        public DateTime ToDate { get => toDate; set => toDate = value; }
        public DateTime CreationDate { get => creationDate; set => creationDate = value; }
        public string ReservationCode { get => reservationCode; set => reservationCode = value; }
    }

    ///  Needs  a ToString() override
}
