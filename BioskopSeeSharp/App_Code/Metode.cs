using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Configuration;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace Klase
{
    public class Metode
    {
        public static Film dohvatiFilm(int idFilma)
        {
            Film tempFilm = null;

            SqlConnection con = new SqlConnection(
            WebConfigurationManager.ConnectionStrings["ConnStringDb1"].ConnectionString);

            con.Open();

            SqlCommand upit = new SqlCommand("SELECT * FROM filmovi WHERE [ID]=@ID;", con);

            upit.Parameters.AddWithValue("@ID", idFilma);

            SqlDataReader dataSet = upit.ExecuteReader();

            while (dataSet.Read())
            {
                tempFilm = new Film();

                tempFilm.idFilma = idFilma;

                tempFilm.nazivFilma = dataSet["NazivFilma"].ToString();

                tempFilm.opisFilma = dataSet["OpisFilma"].ToString();

                tempFilm.nazivDatoteke = dataSet["NazivDatoteke"].ToString();
            }

            dataSet.Close();

            con.Close();

            return tempFilm;
        }

        public static string napraviIspisFilma(Film tempFilm)
        {
            string tempInnerHtml = "";
            
            tempInnerHtml += "<section class=\"movieBlock\">";

            tempInnerHtml += "<div class=\"movieImage\">";

            tempInnerHtml += "<img src=\"Images/MoviePosters/" + tempFilm.nazivDatoteke + ".jpg\">";

            tempInnerHtml += "<a href=\"Galerija.aspx?movienum=" + tempFilm.idFilma + "\">(galerija slika)</a>";

            tempInnerHtml += "</div>";

            tempInnerHtml += "<div class=\"movieInfo\">";

            tempInnerHtml += "<p class=\"movieName\">";

            tempInnerHtml += tempFilm.nazivFilma;

            tempInnerHtml += "</p>";

            tempInnerHtml += "<p class=\"movieDesc\">";

            tempInnerHtml += tempFilm.opisFilma;

            tempInnerHtml += "</p>";

            tempInnerHtml += "<p class=\"movieRezerv\">";

            tempInnerHtml += "<span><b>Rezervacija:</b> </span>";

            tempInnerHtml += "<select id=\"listaDana" + tempFilm.idFilma + "\">";

            tempInnerHtml += "<option value=\"1\">Ponedeljak</option>";
            tempInnerHtml += "<option value=\"2\">Utorak</option>";
            tempInnerHtml += "<option value=\"3\">Sreda</option>";
            tempInnerHtml += "<option value=\"4\">Četvrtak</option>";
            tempInnerHtml += "<option value=\"5\">Petak</option>";
            tempInnerHtml += "<option value=\"6\">Subota</option>";
            tempInnerHtml += "<option value=\"7\">Nedelja</option>";

            tempInnerHtml += "</select>";

            tempInnerHtml += "<select id=\"listaVremena" + tempFilm.idFilma + "\">";

            tempInnerHtml += "<option value=\"15\">15:00</option>";
            tempInnerHtml += "<option value=\"18\">18:00</option>";
            tempInnerHtml += "<option value=\"21\">21:00</option>";

            tempInnerHtml += "</select>";

            tempInnerHtml += "<input type=\"button\" value=\"Rezervisi\" onclick=\"otvoriSalu(" + tempFilm.idFilma + ");\"/>";

            tempInnerHtml += "</p>";

            tempInnerHtml += "</div>";

            tempInnerHtml += "<div style=\"clear: both;\"></div>";

            tempInnerHtml += "</section>";

            return tempInnerHtml;
        }

        public static string napraviIspisGalerijeFilma(Film tempFilm, string pathSlikaFilma)
        {
            string tempInnerHtml = "";

            tempInnerHtml += "<h2>Galerija slika za film: \"" + tempFilm.nazivFilma + "\"</h2>";

            tempInnerHtml += "<div class=\"movieGallery\">";

            foreach (string slikaFilma in dohvatiGalerijuSlikaFilma(pathSlikaFilma))
            {
                tempInnerHtml += "<img src=\"Images/MovieGalleries/" + tempFilm.nazivDatoteke + "/" + slikaFilma + "\">";
            }

            tempInnerHtml += "<div style=\"clear: both;\"></div>";

            tempInnerHtml += "</div>";

            return tempInnerHtml;
        }

        public static List<string> dohvatiGalerijuSlikaFilma(string dirPath)
        {
            List<string> tempGalerijuSlikaFilma = new List<string>();

            foreach ( var file in new DirectoryInfo(dirPath).GetFiles() ) {
                if (file.Name.ToLower().EndsWith(".jpg") || file.Name.ToLower().EndsWith(".png"))
                {
                    tempGalerijuSlikaFilma.Add(file.Name);
                }
            }

            return tempGalerijuSlikaFilma;
        }

        public static List<Film> dohvatiListuFilmova()
        {
            List<Film> tempListaFilmova = new List<Film>();

            SqlConnection con = new SqlConnection(
            WebConfigurationManager.ConnectionStrings["ConnStringDb1"].ConnectionString);

            con.Open();

            SqlCommand upit = new SqlCommand("SELECT * FROM filmovi;", con);

            SqlDataReader dataSet = upit.ExecuteReader();

            while (dataSet.Read())
            {
                Film tempFilm = new Film();

                tempFilm.idFilma = Convert.ToInt32(dataSet["ID"].ToString());

                tempFilm.nazivFilma = dataSet["NazivFilma"].ToString();

                tempFilm.opisFilma = dataSet["OpisFilma"].ToString();

                tempFilm.nazivDatoteke = dataSet["NazivDatoteke"].ToString();

                tempListaFilmova.Add(tempFilm);
            }

            dataSet.Close();

            con.Close();

            return tempListaFilmova;
        }

        public static string dohvatiStringSedista(string filmDanSat)
        {
            //inicijalizacija variable

            object tempStringSedistaObject = null;

            //otvaranje konekcije

            SqlConnection con = new SqlConnection(
            WebConfigurationManager.ConnectionStrings["ConnStringDb1"].ConnectionString);

            con.Open();

            //pokušaj pronalaska elemenenta

            SqlCommand upit = new SqlCommand("SELECT StanjeSale FROM sala WHERE [FilmDanSat]=@FilmDanSat;", con);

            upit.Parameters.AddWithValue("@FilmDanSat", filmDanSat);

            tempStringSedistaObject = upit.ExecuteScalar();

            //stvaranje novog elementa

            if (tempStringSedistaObject == null)
            {
                SqlCommand upit2 = new SqlCommand("INSERT INTO sala([FilmDanSat]) VALUES(@FilmDanSat);", con);

                upit2.Parameters.AddWithValue("@FilmDanSat", filmDanSat);

                int brojRedovaUneseno = upit2.ExecuteNonQuery();

                /* ******************* */

                SqlCommand upit3 = new SqlCommand("SELECT StanjeSale FROM sala WHERE [FilmDanSat]=@FilmDanSat;", con);

                upit3.Parameters.AddWithValue("@FilmDanSat", filmDanSat);

                tempStringSedistaObject = upit3.ExecuteScalar();
            }

            //zatvaranje konekcije

            con.Close();

            //povratna vrednost

            return tempStringSedistaObject.ToString();
        }

        public static void spremiRezervaciju(string filmDanSat, string indexiRezervSedista, string novoStanjeSale, string kreatorUser)
        {
            SqlConnection con = new SqlConnection(
            WebConfigurationManager.ConnectionStrings["ConnStringDb1"].ConnectionString);

            con.Open();

            /* ********************* */

            SqlCommand upit = new SqlCommand("SELECT * FROM rezervacije WHERE [FilmDanSat]=@FilmDanSat AND [IndexiRezervSed]=@IndexiRezervSed;", con);

            upit.Parameters.AddWithValue("@FilmDanSat", filmDanSat);
            upit.Parameters.AddWithValue("@IndexiRezervSed", indexiRezervSedista);

            bool pokusajUnosaDuplikata = upit.ExecuteReader().HasRows;

            /* ********************* */

            if (!pokusajUnosaDuplikata)
            {
                SqlCommand upit2 = new SqlCommand("UPDATE sala SET [StanjeSale]=@StanjeSale WHERE [FilmDanSat]=@FilmDanSat;", con);

                upit2.Parameters.AddWithValue("@StanjeSale", novoStanjeSale);

                upit2.Parameters.AddWithValue("@FilmDanSat", filmDanSat);

                int azuriranoRedova2 = upit2.ExecuteNonQuery();

                /* ********************* */

                SqlCommand upit3 = new SqlCommand("INSERT INTO rezervacije([FilmDanSat], [IndexiRezervSed], [KreatorUser]) VALUES(@FilmDanSat, @IndexiRezervSed, @KreatorUser);", con);

                upit3.Parameters.AddWithValue("@FilmDanSat", filmDanSat);
                upit3.Parameters.AddWithValue("@IndexiRezervSed", indexiRezervSedista);
                upit3.Parameters.AddWithValue("@KreatorUser", kreatorUser);

                int azuriranoRedova3 = upit3.ExecuteNonQuery();
            }

            /* ********************* */

            con.Close();
        }

        public static void urediRezervaciju(int idUredjivaneRezervacije, string filmDanSat, string noviIndexiSedista, string novoStanjeSale)
        {
            SqlConnection con = new SqlConnection(
            WebConfigurationManager.ConnectionStrings["ConnStringDb1"].ConnectionString);

            con.Open();

            /* **************** */

            SqlCommand upit = new SqlCommand("UPDATE sala SET [StanjeSale]=@StanjeSale WHERE [FilmDanSat]=@FilmDanSat;", con);

            upit.Parameters.AddWithValue("@StanjeSale", novoStanjeSale);

            upit.Parameters.AddWithValue("@FilmDanSat", filmDanSat);

            int azuriranoRedova = upit.ExecuteNonQuery();

            /* **************** */

            SqlCommand upit2 = new SqlCommand("UPDATE rezervacije SET [IndexiRezervSed]=@IndexiRezervSed WHERE [ID]=@ID;", con);

            upit2.Parameters.AddWithValue("@IndexiRezervSed", noviIndexiSedista);

            upit2.Parameters.AddWithValue("@ID", idUredjivaneRezervacije);

            int redovaAzurirano2 = upit2.ExecuteNonQuery();

            /* **************** */

            con.Close();
        }

        public static string dohvatiFilmDanSat(int idRezervacije)
        {
            string tempFilmDanSat = "";

            SqlConnection con = new SqlConnection(
            WebConfigurationManager.ConnectionStrings["ConnStringDb1"].ConnectionString);

            con.Open();

            SqlCommand upit = new SqlCommand("SELECT [FilmDanSat] FROM rezervacije WHERE [ID]=@ID;", con);

            upit.Parameters.AddWithValue("@ID", idRezervacije);

            tempFilmDanSat = upit.ExecuteScalar().ToString();

            con.Close();

            return tempFilmDanSat;
        }

        public static List<string> dohvatiListuRezervacijaString(string userNameKorisnika = "")
        {
            List<string> tempListaRezervacijaString = new List<string>();

            SqlConnection con = new SqlConnection(
            WebConfigurationManager.ConnectionStrings["ConnStringDb1"].ConnectionString);

            con.Open();

            /* ****************** */

            SqlCommand upit = null;

            if (userNameKorisnika == "")
            {
                upit = new SqlCommand("SELECT * FROM rezervacije;", con);
            }
            else
            {
                upit = new SqlCommand("SELECT * FROM rezervacije WHERE [KreatorUser]=@KreatorUser;", con);

                upit.Parameters.AddWithValue("@KreatorUser", userNameKorisnika);
            }

            /* ****************** */

            SqlDataReader dataSet = upit.ExecuteReader();

            while (dataSet.Read())
            {
                RezervacijaKlasa tempRezervacija = new RezervacijaKlasa();

                tempRezervacija.idRezervacije = Convert.ToInt32(dataSet["ID"].ToString());

                /* ***************** */

                string[] filmDanSatPolje = dataSet["FilmDanSat"].ToString().Split('+');

                /* ***************** */

                tempRezervacija.nazivFilma = Metode.dohvatiFilm(Convert.ToInt32(filmDanSatPolje[0])).nazivFilma;

                /* ***************** */

                switch (Convert.ToInt32(filmDanSatPolje[1]))
                {
                    case 1: tempRezervacija.odabraniDan = "Pon"; break;
                    case 2: tempRezervacija.odabraniDan = "Uto"; break;
                    case 3: tempRezervacija.odabraniDan = "Sre"; break;
                    case 4: tempRezervacija.odabraniDan = "Čet"; break;
                    case 5: tempRezervacija.odabraniDan = "Pet"; break;
                    case 6: tempRezervacija.odabraniDan = "Sub"; break;
                    case 7: tempRezervacija.odabraniDan = "Ned"; break;
                }

                /* ***************** */

                switch (Convert.ToInt32(filmDanSatPolje[2]))
                {
                    case 15: tempRezervacija.odabraniSat = "15:00"; break;
                    case 18: tempRezervacija.odabraniSat = "18:00"; break;
                    case 21: tempRezervacija.odabraniSat = "21:00"; break;
                }

                /* ***************** */

                tempRezervacija.rezervSed = dataSet["IndexiRezervSed"].ToString();

                /* ***************** */

                if (tempRezervacija.rezervSed != "")
                {
                    tempListaRezervacijaString.Add(tempRezervacija.ToString());
                }
                
            }

            dataSet.Close();

            con.Close();

            return tempListaRezervacijaString;
        }

        public static List<string> dohvatiListuKorisnikaString()
        {
            List<string> tempListaKorisnikaString = new List<string>();

            SqlConnection con = new SqlConnection(
            WebConfigurationManager.ConnectionStrings["ConnStringDb1"].ConnectionString);

            con.Open();

            SqlCommand upit = new SqlCommand("SELECT * FROM korisnici;", con);

            SqlDataReader dataSet = upit.ExecuteReader();

            while (dataSet.Read())
            {
                Korisnik tempKorisnik = new Korisnik();

                tempKorisnik.userName = dataSet["username"].ToString();

                tempKorisnik.userEmail = dataSet["useremail"].ToString();

                tempListaKorisnikaString.Add(tempKorisnik.ToString());
            }

            dataSet.Close();

            con.Close();

            return tempListaKorisnikaString;
        }
    }
}
