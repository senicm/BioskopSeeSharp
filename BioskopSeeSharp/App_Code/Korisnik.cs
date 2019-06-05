using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Klase
{
    public class Korisnik
    {
        public string userName { get; set; }
        public string userEmail { get; set; }

        public Korisnik(string userName, string userEmail)
        {
            this.userName = userName;
            this.userEmail = userEmail;
        }

        public Korisnik()
        {
        }

        public override string ToString()
        {
            return userName + " | " + userEmail;
        }
    }
}
