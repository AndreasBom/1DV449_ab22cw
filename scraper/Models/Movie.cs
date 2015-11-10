using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace scraper.Models
{
    public class Movie
    {
        public string Name { get; set; }

        public Dictionary<string, bool> Availability { get; set; }

        public Movie()
        {
            Availability = new Dictionary<string, bool>();
        }

        public void SetAvailibility(string day, string available)
        {

            if (available.ToLower().Contains("Platser kvar"))
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
