using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace scraper.Models
{
    [Serializable]
    public class Movie
    {
        private string _day;
        public string Name { get; set; }

        public string Day
        {
            get
            {
                switch (_day.ToLower())
                {
                    case "friday":
                        return "Fredag";
                    case "saturday":
                        return "Lördag";
                    case "sunday":
                        return "Söndag";
                }
                return null;
            }
            set { _day = value; }
        }
        
        public string Time { get; set; }

    }
}
