using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using scraper.Models;

namespace scraper.Views
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }


        protected void ButtonSubmit_OnClick(object sender, EventArgs e)
        {
            var inputUrl = TextBoxUrl.Text;

            var app = new App(inputUrl);
            var events = app.GetAllAvailableDays();
            app.GetListOfMovies(events);


            RepeaterResult.DataSource = events;
            RepeaterResult.DataBind();
        }
    }
}