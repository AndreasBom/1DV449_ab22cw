using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scraper.Models
{
   
    public class Fetcher
    {
        protected ScrapeAgent _scrapeAgent;

        protected Fetcher(ScrapeAgent scrape)
        {
            _scrapeAgent = scrape;
        }

        public Fetcher()
            {
            
            }


        public IEnumerable<String> GetListOfLinks()
        {
            var links = _scrapeAgent.GetUrls();
            return links;
        }
    }
}
