using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;

namespace scraper.Models
{
 
    [Serializable]
    public class MovieFetcher : Fetcher
    {
        private readonly string _urlToMovie;
        private Dictionary<string, string> _movieNameAndCode;

        public MovieFetcher(ScrapeAgent scrape)
        {
            _scrapeAgent = scrape;

            //Get the link to /cinema
            var links = GetListOfLinks();
            foreach (var link in links)
            {
                if (link.Contains("cinema"))
                {
                    _urlToMovie = link;
                }
            }
        }

        //Gets a list of movies that takes available calendar days in to account
        //Checks on cinema site witch days that has movies
        public IEnumerable<Movie> GetListOfMovies(IEnumerable<string> desiredDays)
        {
            
            //Find days on /cinema
            var daysWhenMoviesIsOn = ScrapeForMovieDays();
            //Dictionary(<string><sting>) crosscheck Days that match desired days and days that match movie found on /cinema
            var possibleDays = CheckForMatchingDays(desiredDays, daysWhenMoviesIsOn);

            //Fetch movies (gets back as IEnumerable<JToken>)
            var jsonMovies = FetchMovies(possibleDays, desiredDays);

            //Adds jsonMovies to class Movie
            var movies = from jmovie in jsonMovies
                select new Movie
                {
                    Name = (string) _movieNameAndCode[(string)jmovie["movie"]],
                    Day = (string) jmovie["day"],
                    Time = (string) jmovie["time"]
                };

            
            return movies;
        }

        private IEnumerable<JToken> FetchMovies(Dictionary<string, string> possibleDays, IEnumerable<string> desiredDays)
        {
            //Find movies on /cinema
            var movies = ScrapeForMovies();

            //List that holds the result of http request
            var requestResult = new List<JArray>();
            //Loops throw possible days and movies on these days. 
            foreach (var day in possibleDays)
            {
                foreach (var movie in movies)
                {
                    var json = JsonFetcher(_urlToMovie + "/", $"check?day={day.Key}&movie={movie.Key}");
                    //Adds day to json
                    foreach (var jtoken in json)
                    {
                        jtoken["day"] = day.Value;
                    }
                    requestResult.Add(json);
                }
            }

            //Include only movies that has free seats 
            var availableMovies = SortOutAvailableMovies(requestResult);
            return availableMovies;
        }

        private List<JToken> SortOutAvailableMovies(List<JArray> listOfMovies)
        {
            var listToReturn = new List<JToken>();
            foreach (var list in listOfMovies)
            {
                foreach (var l in list)
                {
                    if ((int)l["status"] == 1)
                    {
                        listToReturn.Add(l);
                    }
                    
                }
            }

            return listToReturn;
        }

        //http request, returns an JArray
        private JArray JsonFetcher(string baseUrl,string url)
        {
            string content;
            using (var client = new HttpClient())
            {
                //set http header
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Read http response
                var response = client.GetAsync(url).Result;

                response.EnsureSuccessStatusCode();
                content = response.Content.ReadAsStringAsync().Result;
                //content = await 
            }

            JArray jArray = JArray.Parse(content);

            return jArray;
        }


        private Dictionary<string, string> ScrapeForMovies()
        {
            var tag = _scrapeAgent.GetAllTags(_urlToMovie, "//select[@name='movie']/option");
            //extract days and 'code' for day from tag
            var movies = GetMovieDaysFromTags(tag);

            _movieNameAndCode = movies;

            return movies;
        }

        private Dictionary<string, string> ScrapeForMovieDays()
        {
            var tag = _scrapeAgent.GetAllTags(_urlToMovie, "//select[@name='day']/option");
            //extract days and 'code' for day from tag
            var daysWhenMoviesIsOn = GetMovieDaysFromTags(tag);

            return daysWhenMoviesIsOn;
        } 

        //Cross checking calendars and movies
        private Dictionary<string, string> CheckForMatchingDays(IEnumerable<string> desiredDays, Dictionary<string, string> daysWhenMoviesIsOn)
        {
            var possibleDays = new Dictionary<string, string>();
            foreach (var day in desiredDays)
            {
                foreach (var movieDay in daysWhenMoviesIsOn)
                {
                    if (movieDay.Value == day)
                    {
                        possibleDays.Add(movieDay.Key, movieDay.Value);
                    }
                }
            }

            return possibleDays;
        } 

        private Dictionary<string, string> GetMovieDaysFromTags(IEnumerable<HtmlNode> tags)
        {
            var days = new Dictionary<string, string>();

            foreach (var t in tags)
            {
                //get html
                var rawCode = t.OuterHtml;
                //selects digits (numbers) and remove all other 'junk'
                var code = Regex.Match(rawCode, "\\d\\d").Value;
                //Get the value for corresponding 'code' and translates to english
                var day = t.NextSibling.InnerText;
                var engDay = TranslateDayFromSweToEng(day);
                //Add to dictionary
                if (code != "")
                {
                    days.Add(code, engDay);
                }
                
            }

            return days;
        }

        private string TranslateDayFromSweToEng(String day)
        {
            switch (day.ToLower())
            {
                case "måndag":
                    return "Monday";
                case "tisdag":
                    return "Tuesday";
                case "onsdag":
                    return "Wednesday";
                case "torsdag":
                    return "Thursday";
                case "fredag":
                    return "Friday";
                case "lördag":
                    return "Saturday";
                case "söndag":
                    return "Sunday";
            }

            return day;
        }

    }
}
