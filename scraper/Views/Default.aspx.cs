using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using scraper.Models;

namespace scraper.Views
{
    public partial class Default : System.Web.UI.Page
    {

        private IEnumerable<string> _desiredDays;

        private IEnumerable<Movie> ListOfMovies
        {
            get
            {
                return
                    (IEnumerable<Movie>)
                        (Session["listOfMovies"] ?? (Session["listOfMovies"] = new App(Session["url"].ToString()).GetListOfMovies(_desiredDays)));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (TextBoxUrl.Text != "")
            {
                var url = (string)Session["url"] ?? TextBoxUrl.Text;
                //var listOfMovies = _app.GetListOfMovies(_desiredDays);
                var app = new App(url);
                _desiredDays = app.GetAllAvailableDays();
            }

            //Reload list of movies
            if (Session["url"] != null)
            {
                RepeaterResult.DataSource = ListOfMovies;
                RepeaterResult.DataBind();
            }

            //Will run if user has pressed a link check availability in resturang
            if (HttpContext.Current.Request.QueryString.HasKeys())
            {
                var app = new App(Session["url"].ToString());
                var availableDinnerTime = app.IsDinnerTimeAvailable(ListOfMovies);
                if (availableDinnerTime)
                {
                    var timeString = Regex.Match(Request.QueryString["time"], "\\d\\d").Value;
                    var time = Convert.ToInt32(timeString)+2;
                    Session["time"] = time;
                    Session["day"] = Request.QueryString["day"];
                    Session["name"] = Request.QueryString["name"];
                    Response.Redirect("ResultAvailable.aspx");
                }
                else
                {
                    Response.Redirect("ResultNotAvailable.aspx");
                }
            }
        }

        protected void ButtonSubmit_OnClick(object sender, EventArgs e)
        {
            var app = new App(TextBoxUrl.Text);
            var disiredDays = app.GetAllAvailableDays();

            //Saves the url in session so list of movies will be listed after back click
            Session["url"] = TextBoxUrl.Text;
            
            RepeaterResult.DataSource = ListOfMovies;
            RepeaterResult.DataBind();
        }




    }
}