using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Park
    {
        private int site_id;
        private string name;
        private string location;
        private DateTime establishedDate;
        private int area;
        private int visitors;
        private string description;

        public int Site_id { get => site_id; set => site_id = value; }
        public string Name { get => name; set => name = value; }
        public string Location { get => location; set => location = value; }
        public DateTime EstablishedDate { get => establishedDate; set => establishedDate = value; }
        public int Area { get => area; set => area = value; }
        public int Visitors { get => visitors; set => visitors = value; }
        public string Descriptions { get => description; set => description = value; }

        public Park(int site_id, string name, string location, DateTime establishedDate, int area, int visitors, string description)
        {
            this.site_id = site_id;
            this.name = name;
            this.location = location;
            this.establishedDate = establishedDate;
            this.area = area;
            this .visitors = visitors;
            this.description = description;
        }

        public override string ToString()
        {
            return Site_id.ToString().PadRight(6) + Name.ToString().PadRight(15) + Location.ToString().PadRight(10) + establishedDate.ToString().PadRight(25)+ Area.ToString().PadRight(10)+visitors.ToString().PadRight(10)+description.ToString();
        }
    }
}
