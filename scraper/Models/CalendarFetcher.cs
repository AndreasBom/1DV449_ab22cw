using System;
using System.Collections.Generic;
using System.Linq;

namespace scraper.Models
{
   
    public class CalendarFetcher : Fetcher
    {
        public CalendarFetcher(ScrapeAgent scrape)
        {
            _scrapeAgent = scrape;
        }

        //Fetches all links to calendars and returns a List<string>
        public IEnumerable<String> GetListOfCalendars()
        {
            string urlToCalendars = "";
            var links = GetListOfLinks();

            foreach (var link in links)
            {
                if (link.Contains("calendar"))
                {
                    urlToCalendars = link;
                }
            }

            var listOfCalendars = _scrapeAgent.GetUrls(urlToCalendars);
            return listOfCalendars;
        }

        public IEnumerable<string> GetAvailableDays(IEnumerable<string> urlToCalendars)
        {
            var calendars = GetCalendarsEvents(urlToCalendars);

            var compareCal = new Dictionary<string, bool>();
            var unavailable = new List<string>(3);

            //Iterates throw all calendars
            foreach (var cal in calendars)
            {
                //Iterates throw the calendars avaliability list
                foreach (var availability in cal.Availability)
                {
                    //If available, add to compareCal
                    if (availability.Value)
                    {
                        compareCal[availability.Key] = availability.Value;
                    }

                    //Add to list if not available
                    if (availability.Value == false)
                    {
                        unavailable.Add(availability.Key);
                    }
                }
            }
            //Removes all posts in compareCal that is included in unavailable list
            foreach (var u in unavailable)
            {
                if (compareCal.ContainsKey(u))
                {
                    compareCal.Remove(u);
                }
            }
            return (from c in compareCal select c.Key).ToList();
        }

        private IEnumerable<Calendar> GetCalendarsEvents(IEnumerable<string> urlToCalendars)
        {
            var listOfCalendarEvents = new List<Calendar>();

            foreach (var calendar in urlToCalendars)
            {
                //Fetch name of calendar owner
                var name = _scrapeAgent.Scrape(calendar, "//h2").First();
                //Fetch calendar
                var table = _scrapeAgent.Scrape(calendar, "//table");
                //Fetch day
                var th = table[0].SelectNodes("//th");
                var td = table[0].SelectNodes("//td");

                var calObj = new Calendar { Name = name.InnerHtml };

                for (int i = 0; i < th.Count; i++)
                {
                    calObj.SetAvailability(th[i].InnerHtml, td[i].InnerHtml);
                }

                listOfCalendarEvents.Add(calObj);
            }
            return listOfCalendarEvents;
        }



    }
}
