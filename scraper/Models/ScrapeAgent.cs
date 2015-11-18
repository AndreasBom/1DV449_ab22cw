using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;


namespace scraper.Models
{
    public class ScrapeAgent
    {
        private readonly HtmlWeb _htmlWeb = new HtmlWeb();
        private readonly string _baseUrl; 

        public ScrapeAgent(string baseUrl)
        {
            _baseUrl = baseUrl;
            _htmlWeb.UserAgent = "andreas.bom@gmail.com";
        }

        public HtmlNodeCollection Scrape(string htmlDocument, string xPathTag)
        {
            var doc = _htmlWeb.Load(htmlDocument);
            var nodeCollection = doc.DocumentNode.SelectNodes(xPathTag);
            return nodeCollection;
        }

        public IEnumerable<string> Scrape(string htmlDocument, string xPathTag, string attribute)
        {
            var doc = _htmlWeb.Load(htmlDocument);
            var listOfTags = doc.DocumentNode.SelectNodes(xPathTag);
            var listOfUrls = from a in listOfTags
                             select a.GetAttributeValue(attribute, "");
            return listOfUrls;
        }

        //Get links from a specifide web site. 
        public IEnumerable<String> GetUrls(string url)
        {
            var listOfUrls = Scrape(url, "//a", "href");
            var listToReturn = UrlFix(url, listOfUrls);

            return listToReturn;
        }

        //Get links from _baseUrl
        public IEnumerable<String> GetUrls()
        {
            //Load html document
            var doc = _htmlWeb.Load(_baseUrl);

            //Find a-tags
            var listOfLinkTags = doc.DocumentNode.SelectNodes("//a");

            //find a tags with attribute href and get their value (url)
            var listOfUrls = from a in listOfLinkTags
                             select a.GetAttributeValue("href", "");

            //combine url and links
            var listToReturn = UrlFix(_baseUrl, listOfUrls);

            return listToReturn;
        }


        private IEnumerable<string> UrlFix(string scrapeUrl, IEnumerable<string> listOfUrls)
        {
            var listToReturn = new List<string>();
            //Makes sure that url is complete with baseurl + specific url, plus removes index(.html, .php, .aspx etc)
            foreach (var link in listOfUrls)
            {
                if (scrapeUrl.EndsWith("/") && link.StartsWith("/"))
                {
                    scrapeUrl = scrapeUrl.Remove(scrapeUrl.Length - 1);
                }
                if (scrapeUrl.EndsWith("/") == false && link.StartsWith("/") == false)
                {
                    scrapeUrl = scrapeUrl + "/";
                }
                //Adds the 'parent' url with the 'child' url
                var dir = new Uri(scrapeUrl + link);

                //removes the index file from the url
                //This is done so the url can add more directories. 
                //Adds to a IEnumerable list
                if (dir.Segments.Last().Contains("index"))
                {
                    var path = dir.AbsoluteUri.Remove(dir.AbsoluteUri.Length - dir.Segments.Last().Length);
                    listToReturn.Add(path);
                }
                else
                {
                    listToReturn.Add(dir.ToString());
                }
            }
            return listToReturn;
        }

        public IEnumerable<HtmlNode> GetAllTags(string urlToScrape, string xpathTag)
        {
            var doc = _htmlWeb.Load(urlToScrape);

            var listOfTags = (doc.DocumentNode.SelectNodes(xpathTag)).ToList();

            return listOfTags;
        }

        public IEnumerable<string> GetAllTags(string urlToScrape, string xpathTag, string attribute)
        {
            var doc = _htmlWeb.Load(urlToScrape);

            var listOfTags = (doc.DocumentNode.SelectNodes(xpathTag)).ToList();

            //find a tags with attribute href and get their value (url)
            var listToReturn = from a in listOfTags
                             select a.GetAttributeValue(attribute, "");

            return listToReturn;
        }


    }
}
