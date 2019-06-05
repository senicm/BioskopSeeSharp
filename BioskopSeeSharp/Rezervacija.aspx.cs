using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Configuration;
using System.Data.SqlClient;

using Klase;

public partial class Rezervacija : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Film tempFilm = Metode.dohvatiFilm(Convert.ToInt32(Request.QueryString["film"]));

        if (tempFilm != null)
        {
            NazivFilmaLabel.Text = tempFilm.nazivFilma;

            /* ***************** */

            int odabraniDan = Convert.ToInt32(Request.QueryString["dan"]);

            string odabraniDanString = "";

            switch (odabraniDan)
            {
                case 1: odabraniDanString = "Ponedeljak"; break;
                case 2: odabraniDanString = "Utorak"; break;
                case 3: odabraniDanString = "Sreda"; break;
                case 4: odabraniDanString = "Četvrtak"; break;
                case 5: odabraniDanString = "Petak"; break;
                case 6: odabraniDanString = "Subota"; break;
                case 7: odabraniDanString = "Nedelja"; break;
                default: return;
            }

            OdabraniDanLabel.Text = odabraniDanString;

            /* ***************** */

            int odabraniSat = Convert.ToInt32(Request.QueryString["sat"]);

            string odabraniSatString = "";

            switch (odabraniSat)
            {
                case 15: odabraniSatString = "15:00"; break;
                case 18: odabraniSatString = "18:00"; break;
                case 21: odabraniSatString = "21:00"; break;
                default: return;
            }

            OdabraniSatLabel.Text = odabraniSatString;

            /* ***************** */

            SalaLabel.Text = tempFilm.idFilma.ToString();

            /* ***************** */

            string tempFilmDanSat = tempFilm.idFilma + "+" + odabraniDan + "+" + odabraniSat;

            string tempStringSedista = Metode.dohvatiStringSedista(tempFilmDanSat);

            /* ***************** */

            bool spremanjeRezervacije = (Request.QueryString["sed"] != null);

            /* ***************** */

            if (spremanjeRezervacije)
            {
                string tempIndexiSedistaString = Request.QueryString["sed"];

                if (tempIndexiSedistaString.Length > 0)
                {
                    int tempMaxIndexSedista = tempStringSedista.Length - 1;

                    /* ******************* */

                    char[] tempNoviStringSedistaArray = tempStringSedista.ToCharArray();

                    foreach (string index in tempIndexiSedistaString.Split(','))
                    {
                        int tempIntIndex = Convert.ToInt32(index);

                        if ((tempIntIndex >= 0) && (tempIntIndex <= tempMaxIndexSedista))
                        {
                            if (tempNoviStringSedistaArray[tempIntIndex] != '|')
                            {
                                tempNoviStringSedistaArray[tempIntIndex] = 'X';
                            }
                            else
                            {
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Pogresno odabrana Sedista.');", true);

                                spremanjeRezervacije = false;

                                break;
                            }
                        }
                        else
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Pogresno odabrana Sedista.');", true);

                            spremanjeRezervacije = false;

                            break;
                        }
                    }

                    string tempNovoStanjeSale = new string(tempNoviStringSedistaArray);

                    /* ******************* */

                    string tempKreatorUser = HttpContext.Current.User.Identity.Name;

                    /* ******************* */

                    if (tempKreatorUser == string.Empty)
                    {
                        tempKreatorUser = "anonimus";
                    }

                    /* ******************* */

                    if (spremanjeRezervacije)
                    {
                        if (Request.QueryString["rez"] != null)
                        {
                            if (Session["uredjivanaRezervacijaSed"] != null)
                            {
                                int tempIdUredjivaneRezervacije = Convert.ToInt32(Request.QueryString["rez"]);

                                Metode.urediRezervaciju(tempIdUredjivaneRezervacije, tempFilmDanSat, tempIndexiSedistaString, tempNovoStanjeSale);

                                Session.Remove("uredjivanaRezervacijaSed");

                                tempStringSedista = ""; //stanje sale neće biti ispisano
                            }
                            else
                            {
                                Response.Redirect("~/Default.aspx");
                            }
                        }
                        else
                        {
                            Metode.spremiRezervaciju(tempFilmDanSat, tempIndexiSedistaString, tempNovoStanjeSale, tempKreatorUser);

                            tempStringSedista = ""; //stanje Sale neće biti ispisano
                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Niste odabrali Sedista.');", true);

                    spremanjeRezervacije = false;
                }
            }
            else
            {
                if (Session["isUredjivanjeRezervacije"] != null)
                {
                    string tempIndexiSedistaString = Session["uredjivanaRezervacijaSed"].ToString();

                    /* ******************* */

                    char[] tempNoviStringSedistaArray = tempStringSedista.ToCharArray();

                    foreach (string index in tempIndexiSedistaString.Split(','))
                    {
                        try { tempNoviStringSedistaArray[Convert.ToInt32(index)] = 'W'; }
                        catch { }
                    }

                    tempStringSedista = new string(tempNoviStringSedistaArray);

                    /* ******************* */

                    Session.Remove("isUredjivanjeRezervacije");
                }
            }

            /* ***************** */

            Label tempSedistaIzbor = new Label();

            tempSedistaIzbor.ID = "MovieGalleryLabel" + tempFilm.idFilma;

            /* ***************** */

            string tempInnerHtml = "";

            /* ***************** */

            tempInnerHtml += "<div id=\"pregledSedista\">";

            for (int i = 0; i < tempStringSedista.Length; i++)
            {
                switch (tempStringSedista[i])
                {
                    case 'W': tempInnerHtml += "<input type=\"checkbox\" class=\"freeSit\" name=\"strIndex" + i + "\" value=\"true\" checked>"; break;
                    case 'O': tempInnerHtml += "<input type=\"checkbox\" class=\"freeSit\" name=\"strIndex" + i + "\" value=\"true\">"; break;
                    case 'X': tempInnerHtml += "<input type=\"checkbox\" class=\"nonFreeSit\" name=\"strIndex" + i + "\" value=\"true\" disabled readonly checked>"; break;
                    default: tempInnerHtml += "<br>"; break;
                }
            }

            tempInnerHtml += "</div>";

            /* ************************ */

            if (Session["uredjivanaRezervacijaId"] != null)
            {
                tempInnerHtml += "<input class=\"spremiRezervButton\" type=\"button\" value=\"Spremi rezervaciju\" onclick=\"spremiRezervaciju(" + tempFilm.idFilma + ", " + odabraniDan + ", " + odabraniSat + ", " + Session["uredjivanaRezervacijaId"] + ");\">";

                Session.Remove("uredjivanaRezervacijaId");
            }
            else if (spremanjeRezervacije)
            {
                tempInnerHtml += "<input class=\"spremiRezervButton\" type=\"button\" value=\"Rezervacija spremljena\" disabled>";
            }
            else
            {
                tempInnerHtml += "<input class=\"spremiRezervButton\" type=\"button\" value=\"Spremi rezervaciju\" onclick=\"spremiRezervaciju(" + tempFilm.idFilma + ", " + odabraniDan + ", " + odabraniSat + ", " + "null" + ");\">";
            }

            /* ************************ */

            tempSedistaIzbor.Text = tempInnerHtml;

            /* ************************ */

            CheckBoxSedistaPanel.Controls.Add(tempSedistaIzbor);
        }
    }
}