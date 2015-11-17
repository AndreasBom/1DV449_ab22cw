using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Newtonsoft.Json.Linq;

namespace scraper.Models
{
    public class App
    {
        private readonly CalendarFetcher _calendarFetcher;
        private readonly MovieFetcher _movieFetcher;
        private readonly ResturangFetcher _resturangFetcher;
        private IEnumerable<Movie> _listOfAvailableMovies; 

        public App(string baseUrl)
        {
            var scrapeAgent = new ScrapeAgent(baseUrl);
            _calendarFetcher = new CalendarFetcher(scrapeAgent);
            _movieFetcher = new MovieFetcher(scrapeAgent);
            _resturangFetcher = new ResturangFetcher(scrapeAgent);

        }

        //Get available calendar days
        public IEnumerable<string> GetAllAvailableDays()
        {
            //Fetch list of calendars
            var calendarList = _calendarFetcher.GetListOfCalendars();
            //Fetches list with avalible day(s)
            var avaliableList = _calendarFetcher.GetAvailableDays(calendarList);

            return avaliableList;
        }

        //Get List of movies based on available calendar days
        public IEnumerable<Movie> GetListOfMovies(IEnumerable<string> desiredDays)
        {
            //Gets a list of available movies
            _listOfAvailableMovies = _movieFetcher.GetListOfMovies(desiredDays);
            return _listOfAvailableMovies;
        }

        
        public IEnumerable<Dinner> IsDinnerTimeAvailable(IEnumerable<Movie> listOfAvailableMovies)
        {
            
            return _resturangFetcher.CheckForAvailableDinnerTime(listOfAvailableMovies);
        }



    }
}
