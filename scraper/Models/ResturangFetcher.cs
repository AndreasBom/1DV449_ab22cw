using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;



namespace scraper.Models
{
   
    public class ResturangFetcher : Fetcher
    {
        private string _movieDay;
        private int _movieTime;
        private string _movieName;

        private readonly string _urlToResturang;
        public void FetchDayAndTime()
        {
            
            _movieName = HttpContext.Current.Request.QueryString["name"];
            _movieDay = HttpContext.Current.Request.QueryString["day"];
            var timeLong =  HttpContext.Current.Request.QueryString["time"];
            //keep only hours
            var timeString = Regex.Match(timeLong, @"[\d]{2}").Value;
            //Convert to int
            _movieTime = Convert.ToInt32(timeString);
        }

        //Get the link to resturang == _baseurl + /dinner
        public ResturangFetcher(ScrapeAgent scrape)
        {
            _scrapeAgent = scrape;

            //Get the link to /cinema
            var links = GetListOfLinks();
            foreach (var link in links)
            {
                if (link.Contains("dinner"))
                {
                    _urlToResturang = link;
                }
            }
        }


        public IEnumerable<Dinner> CheckForAvailableDinnerTime(IEnumerable<Movie> listOfAvailableMovies)
        {
            //CROSS CHECK MOVIE AND DINNER
            var availability = ScrapeForAvailableDinnerTime().ToList();
            var movie = (from m in listOfAvailableMovies
                where m.Name == _movieName
                select m.Name).FirstOrDefault();

            var list = new List<Dinner>();
            //Check all times when resturant is available after movie (movies are not longer than 2 hours).
            foreach (var dinnerTime in availability)
            {
                
                if (dinnerTime.Day == _movieDay && _movieName == movie)
                {
                    if (dinnerTime.Time != _movieTime &&
                        _movieTime + 2  <= dinnerTime.Time)
                    {
                        list.Add(dinnerTime);
                    }
                }
            }

            return list;
        }


        private IEnumerable<Dinner> ScrapeForAvailableDinnerTime()
        {
            FetchDayAndTime();
            var tag = _scrapeAgent.GetAllTags(_urlToResturang, "//p//input[@value]");
            var time = "";
            var listOfTimes = new List<Dinner>();
            var day = "";

            //Selects disired day and adds to a list
            foreach (var t in tag)
            {
                var text = t.OuterHtml;
                if (_movieDay == "Fredag")
                {
                    if (text.Contains("fre"))
                    {
                        time = Regex.Match(text, @"[\d]{2}").Value;
                        day = "Fredag";
                    }
                }
                else if (_movieDay == "Lördag")
                {
                    if (text.Contains("lor"))
                    {
                        time = Regex.Match(text, @"[\d]{2}").Value;
                        day = "Lördag";
                    }
                }
                else if (_movieDay == "Söndag")
                {
                    if (text.Contains("son"))
                    {
                        time = Regex.Match(text, @"[\d]{2}").Value;
                        day = "Söndag";
                    }
                }

                //Adds to list if day has a value
                if (day != "")
                {
                    listOfTimes.Add(
                    new Dinner
                    {
                        Day = day,
                        Time = Convert.ToInt32(time)
                    });
                    day = "";
                }
            }

            return listOfTimes;
        }
           





    }
}
