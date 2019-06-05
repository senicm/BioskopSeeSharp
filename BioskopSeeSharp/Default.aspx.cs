using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using Klase;

    public partial class _Default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ispisiRandom();
            }

        }
    
        protected void ispisiRandom()
        {
            Random random = new Random();

            int i = random.Next(1, 4);

            ImageCurrent.ImageUrl = "~/Images/Bioskop/" + i.ToString() + ".jpg";
        }

        protected void TimerUvecaj_Tick(object sender, EventArgs e)
        {
            ispisiRandom();
        }
    }
