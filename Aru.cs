using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LezerVagoHazszam
{
    class Aru
    {
        public string termeknev = "";
        public string vasarloNeve = "";
        public DateTime idopont;
        public string hol = "meska";
        public string fizetesiMod = "";
        public string szallitasiMod = "";
        public int ar = 0;
        public int szamlazottPostaKolts = 0;
        public string szamlaSzam = "";

        public Aru() 
        {

        }
        public Aru(string termekNev, string vasarloNeve, DateTime idopont, string fizetesiMod, string szallitasiMod, int ar, 
                                                                                            int szamlazottPostaKolts, string szamlaSzam)
        {
            this.termeknev = termekNev;
            this.vasarloNeve = vasarloNeve;
            this.idopont = idopont;
            this.fizetesiMod = fizetesiMod;
            this.szallitasiMod = szallitasiMod;
            this.ar = ar;
            this.szamlazottPostaKolts = szamlazottPostaKolts;
            this.szamlaSzam = szamlaSzam;
        }

        public override string ToString()
        {
            string szoveg = "";

            szoveg += termeknev + "\t\t" + vasarloNeve + "\t" + idopont.ToString("yy") + ". " + idopont.Month + ". "+ idopont.Day + "\t" +hol + "\t" + 
                fizetesiMod + "\t" + szallitasiMod + "\t" +
                ar + "\t" + szamlazottPostaKolts + "\t" + szamlaSzam + "\t";

            return szoveg;
        }

        public string atirCSVFormat()
        {
            string szoveg = "";

            char elvalasztoKarakter = ';';

            szoveg += termeknev + elvalasztoKarakter + vasarloNeve + elvalasztoKarakter + idopont.ToString("yy") + ". " + idopont.Month + ". " + idopont.Day +
                elvalasztoKarakter + hol + elvalasztoKarakter + fizetesiMod + elvalasztoKarakter + szallitasiMod + elvalasztoKarakter +
                ar + elvalasztoKarakter + szamlazottPostaKolts + elvalasztoKarakter + elvalasztoKarakter + szamlaSzam + elvalasztoKarakter;

            return szoveg;
        }

    }
}
