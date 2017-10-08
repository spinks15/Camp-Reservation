using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes.Models
{
    public class Site
    {
        private int siteId;
        private int campgroundId;
        private int siteNumber;
        private int maxOccupancy;
        private bool handicapAccessible;
        private int maxRVLength;
        private bool hasUtilities;

        public Site(int siteId, int campgroundId, int siteNumber, int maxOccupancy, bool handicapAccessible, int maxRVLength, bool hasUtilities)
        {
            this.siteId = siteId;
            this.campgroundId = campgroundId;
            this.siteNumber = siteNumber;
            this.maxOccupancy = maxOccupancy;
            this.handicapAccessible = handicapAccessible;
            this.maxRVLength = maxRVLength;
            this.hasUtilities = hasUtilities;
        }

        public int SiteId { get => siteId; set => siteId = value; }
        public int CampgroundId { get => campgroundId; set => campgroundId = value; }
        public int SiteNumber { get => siteNumber; set => siteNumber = value; }
        public int MaxOccupancy { get => maxOccupancy; set => maxOccupancy = value; }
        public bool HandicapAccessible { get => handicapAccessible; set => handicapAccessible = value; }
        public int MaxRVLength { get => maxRVLength; set => maxRVLength = value; }
        public bool HasUtilities { get => hasUtilities; set => hasUtilities = value; }
    }
}
