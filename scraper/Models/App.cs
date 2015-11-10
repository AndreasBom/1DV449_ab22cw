using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace scraper.Models
{
    public class App
    {
        private readonly CalendarFetcher _calendarFetcher;
        private readonly MovieFetcher _movieFetcher;

        public App(string baseUrl)
        {
            var scrapeAgent = new ScrapeAgent(baseUrl);
            _calendarFetcher = new CalendarFetcher(scrapeAgent);
            _movieFetcher = new MovieFetcher(scrapeAgent);

        }

        public IEnumerable<string> GetAllAvailableDays()
        {
            //Fetch list of calendars
            var calendarList = _calendarFetcher.GetListOfCalendars();
            //Fetches list with avalible day(s)
            var avaliableList = _calendarFetcher.GetAvailableDays(calendarList);

            return avaliableList;
        }

        public IEnumerable<JToken> GetListOfMovies(IEnumerable<string> desiredDays)
        {
            return _movieFetcher.GetListOfMovies(desiredDays);
        } 


        
    }
}
