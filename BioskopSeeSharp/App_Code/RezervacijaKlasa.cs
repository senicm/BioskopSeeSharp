using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Klase
{
    public class RezervacijaKlasa
    {
        public int idRezervacije { get; set; }
        public string nazivFilma { get; set; }
        public string odabraniDan { get; set; }
        public string odabraniSat { get; set; }
        public string rezervSed { get; set; }

        public RezervacijaKlasa()
        {
            this.idRezervacije = -1;
            this.nazivFilma = "";
            this.odabraniDan = "";
            this.odabraniSat = "";
            this.rezervSed = "";
        }

        public RezervacijaKlasa(int idRezervacije, string nazivFilma, string odabraniDan, string odabraniSat, int brojSale, string rezervSed, string potvrdjeno)
        {
            this.idRezervacije = idRezervacije;
            this.nazivFilma = nazivFilma;
            this.odabraniDan = odabraniDan;
            this.odabraniSat = odabraniSat;
            this.rezervSed = rezervSed;
        }

        public override string ToString()
        {
            return "#" + idRezervacije.ToString().PadLeft(4, '0') + " | \"" + nazivFilma + "\" | " + odabraniDan + " u " + odabraniSat + " | Sed. " + rezervSed + "\"";
        }
    }
}