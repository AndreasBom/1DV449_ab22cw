using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scraper.Models
{
    
    public class Calendar
    {
        public string Name { get; set; }

        public Dictionary<string, bool> Availability { get; set; }

        public Calendar()
        {
            Availability = new Dictionary<string, bool>();
        }

        public void SetAvailability(string day, string available)
        {
            
            if (available.ToLower().Contains("ok"))
            {
                Availability.Add(day, true);
            }
            else
            {
                Availability.Add(day, false);
            }
        }
    }
}
