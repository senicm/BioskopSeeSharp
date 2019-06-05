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

public partial class Upravljac : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            string currUserStatus = Metode.dohvatiUserStatus();

            if (currUserStatus == "regular" || currUserStatus == "admin")
            {
                RegularPanel.Visible = true;

                /* ******************* */

                if (!IsPostBack)
                {
                    List<string> listaRezervacijaKorisnikaString = Metode.dohvatiListuRezervacijaString(HttpContext.Current.User.Identity.Name);

                    listaRezervacijaKorisnikaString.Sort();

                    for (int i = 0; i < listaRezervacijaKorisnikaString.Count; i++)
                    {
                        PopisKorisnikovihRezervacija.Items.Add(listaRezervacijaKorisnikaString[i]);
                    }
                }

                /* ******************* */

                int tempTrenutniCount = PopisKorisnikovihRezervacija.Items.Count;

                if (tempTrenutniCount <= 4)
                {
                    PopisKorisnikovihRezervacija.Rows = 4;
                }
                else if (tempTrenutniCount > 4 && tempTrenutniCount < 10)
                {
                    PopisKorisnikovihRezervacija.Rows = tempTrenutniCount;
                }
                else
                {
                    PopisKorisnikovihRezervacija.Rows = 10;
                }
            }

            if (currUserStatus == "admin")
            {
                AdminPanel.Visible = true;

                /* ******************* */

                if (!IsPostBack)
                {
                    List<string> listaSvihRezervacijaString = Metode.dohvatiListuRezervacijaString();

                    listaSvihRezervacijaString.Sort();

                    for (int i = 0; i < listaSvihRezervacijaString.Count; i++)
                    {
                        PopisSvihRezervacija.Items.Add(listaSvihRezervacijaString[i]);
                    }
                }

                /* ******************* */

                int tempTrenutniCount = PopisSvihRezervacija.Items.Count;

                if (tempTrenutniCount <= 4)
                {
                    PopisSvihRezervacija.Rows = 4;
                }
                else if (tempTrenutniCount > 4 && tempTrenutniCount < 10)
                {
                    PopisSvihRezervacija.Rows = tempTrenutniCount;
                }
                else
                {
                    PopisSvihRezervacija.Rows = 10;
                }
            }
        }
    }

    protected void OtkaziRezervacijuButton_Click(object sender, EventArgs e)
    {
        if (PopisKorisnikovihRezervacija.SelectedItem != null)
        {
            int tempSelectedItemIndex = PopisKorisnikovihRezervacija.SelectedIndex;
            string tempSelectedItemValue = PopisKorisnikovihRezervacija.SelectedItem.ToString();

            /* *********************** */

            string[] tempRezervacijaStringSplit = tempSelectedItemValue.Split('|');

            int tempIdRezervacije = Convert.ToInt32(tempRezervacijaStringSplit[0].Trim(' ').Substring(1));
            string tempIndexiSedista = tempRezervacijaStringSplit[4].Trim(' ').Substring(6);

            /* *********************** */

            string tempFilmDanSat = Metode.dohvatiFilmDanSat(tempIdRezervacije);

            /* *********************** */

            Metode.oslobodiSedista(tempFilmDanSat, tempIndexiSedista);

            /* *********************** */

            SqlConnection con = new SqlConnection(
            WebConfigurationManager.ConnectionStrings["ConnStringDb1"].ConnectionString);

            con.Open();

            /* ******************** */

            SqlCommand upit = new SqlCommand("DELETE FROM rezervacije WHERE [ID]=@ID;", con);

            upit.Parameters.AddWithValue("@ID", tempIdRezervacije);

            int redovaObrisano = upit.ExecuteNonQuery();

            /* ******************** */

            con.Close();

            /* ******************** */

            PopisKorisnikovihRezervacija.Items.RemoveAt(tempSelectedItemIndex);

            /* ******************** */

            for (int i = 0; i < PopisSvihRezervacija.Items.Count; i++)
            {
                if (PopisSvihRezervacija.Items[i].ToString().Equals(tempSelectedItemValue))
                {
                    PopisSvihRezervacija.Items.RemoveAt(i);
                }
            }
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Niste izabrali rezervaciju.');", true);
        }

    }

    protected void PotvrdiRezervacijuButton_Click(object sender, EventArgs e)
    {
        if (PopisSvihRezervacija.SelectedItem != null)
        {
            int tempSelectedItemIndex = PopisSvihRezervacija.SelectedIndex;
            string tempSelectedItemValue = PopisSvihRezervacija.SelectedItem.ToString();

            /* *********************** */

            string[] tempRezervacijaStringSplit = tempSelectedItemValue.Split('|');

            int tempIdRezervacije = Convert.ToInt32(tempRezervacijaStringSplit[0].Trim(' ').Substring(1));
            string tempIsPotvrdjeno = tempRezervacijaStringSplit[5].Trim(' ').Trim('"');

            /* *********************** */

            if (tempIsPotvrdjeno == "NE")
            {
                Metode.potvrdiRezervaciju(tempIdRezervacije);

                /* *********************** */

                string tempAzuriranaRezervacijaString = "";

                for (int i = 0; i < tempRezervacijaStringSplit.Length; i++)
                {
                    if (i == 5)
                    {
                        tempAzuriranaRezervacijaString += tempRezervacijaStringSplit[i].Replace("NE", "DA") + "|";
                    }
                    else
                    {
                        tempAzuriranaRezervacijaString += tempRezervacijaStringSplit[i] + "|";
                    }

                }

                tempAzuriranaRezervacijaString = tempAzuriranaRezervacijaString.Substring(0, tempAzuriranaRezervacijaString.Length - 1);

                /* *********************** */

                for (int i = 0; i < PopisKorisnikovihRezervacija.Items.Count; i++)
                {
                    if (PopisKorisnikovihRezervacija.Items[i].ToString().Equals(tempSelectedItemValue))
                    {
                        PopisKorisnikovihRezervacija.Items.RemoveAt(i);

                        PopisKorisnikovihRezervacija.Items.Insert(i, tempAzuriranaRezervacijaString);
                    }
                }

                /* *********************** */

                PopisSvihRezervacija.Items.RemoveAt(tempSelectedItemIndex);

                PopisSvihRezervacija.Items.Insert(tempSelectedItemIndex, tempAzuriranaRezervacijaString);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Rezervacija je već potvrđena.');", true);
            }

        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Niste izabrali rezervaciju.');", true);
        }

    }

    protected void UrediRezervacijuButton_Click(object sender, EventArgs e)
    {
        if (PopisSvihRezervacija.SelectedItem != null)
        {
            int tempSelectedItemIndex = PopisSvihRezervacija.SelectedIndex;
            string tempSelectedItemValue = PopisSvihRezervacija.SelectedItem.ToString();

            /* *********************** */

            string[] tempRezervacijaStringSplit = tempSelectedItemValue.Split('|');

            int tempIdRezervacije = Convert.ToInt32(tempRezervacijaStringSplit[0].Trim(' ').Substring(1));
            string tempIndexiSedista = tempRezervacijaStringSplit[4].Trim(' ').Substring(6);

            /* *********************** */

            string tempFilmDanSat = Metode.dohvatiFilmDanSat(tempIdRezervacije);

            /* *********************** */

            Metode.oslobodiSedista(tempFilmDanSat, tempIndexiSedista);

            /* *********************** */

            SqlConnection con = new SqlConnection(
            WebConfigurationManager.ConnectionStrings["ConnStringDb1"].ConnectionString);

            con.Open();

            /* ******************** */

            SqlCommand upit = new SqlCommand("UPDATE rezervacije SET [IndexiRezervSed]=@IndexiRezervSed WHERE [ID]=@ID;", con);

            upit.Parameters.AddWithValue("@IndexiRezervSed", "");

            upit.Parameters.AddWithValue("@ID", tempIdRezervacije);

            int redovaAzurirano = upit.ExecuteNonQuery();

            /* ******************** */

            con.Close();

            /* ******************** */

            Session["isUredjivanjeRezervacije"] = "true";

            Session["uredjivanaRezervacijaId"] = tempIdRezervacije;
            Session["uredjivanaRezervacijaSed"] = tempIndexiSedista;

            /* ******************** */

            string[] tempFilmDanSatSplit = tempFilmDanSat.Split('+');

            Response.Redirect("~/Rezervacija.aspx?film=" + tempFilmDanSatSplit[0] + "&dan=" + tempFilmDanSatSplit[1] + "&sat=" + tempFilmDanSatSplit[2]);
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "alert('Niste izabrali rezervaciju.');", true);
        }
    }
}