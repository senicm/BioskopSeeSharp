using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Configuration;
using System.Data.SqlClient;

public partial class Account : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        SqlConnection con = new SqlConnection(
    WebConfigurationManager.ConnectionStrings["ConnStringDb1"].ConnectionString);

        try
        {
            con.Open();

            SqlCommand upit = new SqlCommand("SELECT * FROM korisnici WHERE username=@ime AND password=@lozinka", con);

            upit.Parameters.AddWithValue("@ime", Login1.UserName);
            upit.Parameters.AddWithValue("@lozinka", Login1.Password);

            SqlDataReader podaci = upit.ExecuteReader();

            if (podaci.HasRows)
            {
                e.Authenticated = true;
            }
            else
            {
                e.Authenticated = false;
            }

        }

        catch (Exception greska)
        {
            Response.Write("<div id=\"greskaPoruka\">");

            Response.Write("<p>" + greska.Message + "</p>");

            Response.Write("<p>" + greska.StackTrace.Replace("\n", "<br>") + "</p>");

            Response.Write("</div>");
        }

        finally
        {
            con.Close();
        }

    }


    protected void RegisterButtonClick_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(
    WebConfigurationManager.ConnectionStrings["ConnStringDb1"].ConnectionString);

        try
        {
            con.Open();

            SqlCommand upit = new SqlCommand("INSERT INTO korisnici([username],[password],[useremail]) VALUES (@username,@password,@useremail)", con);

            upit.Parameters.AddWithValue("@username", UserNameTextBox.Text);
            upit.Parameters.AddWithValue("@password", UserPassTextBox.Text);
            upit.Parameters.AddWithValue("@useremail", UserEmailTextBox.Text);

            int broj = upit.ExecuteNonQuery();

            Response.Redirect("~/Default.aspx?view=5");
        }

        catch (Exception greska)
        {
            Response.Write("<div id=\"greskaPoruka\">");

            Response.Write("<p>" + greska.Message + "</p>");

            Response.Write("<p>" + greska.StackTrace.Replace("\n", "<br>") + "</p>");

            Response.Write("</div>");
        }

        finally
        {
            con.Close();
        }
    }
}