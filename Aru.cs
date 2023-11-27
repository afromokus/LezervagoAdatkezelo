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

            szoveg += "Termék neve : " + termeknev + "\nVásárló neve: " + vasarloNeve + "\n" + 
                                "Rendelési ideje: " + idopont.ToString("yy") + ". " + idopont.Month + ". "+ idopont.Day + "\n" + 
                                                                        "Felület: " + hol + "\n" + "Fizetési mód: " + fizetesiMod + "\n" + 
                                                                                    "Szállítási mód: " + szallitasiMod + "\nTermék ára: " +
                                                                                             ar + "\nSzámlázott postaköltség: " + szamlazottPostaKolts + 
                                                                                                        "\nSzámlaszám:" + szamlaSzam;

            return szoveg;
        }

        public string atirCSVFormat()
        {
            string szoveg = "";

            char elvalasztoKarakter = '\t';

            szoveg += termeknev + elvalasztoKarakter + vasarloNeve + elvalasztoKarakter + idopont.ToString("yy") + ". " + idopont.Month + ". " + idopont.Day +
                elvalasztoKarakter + hol + elvalasztoKarakter + fizetesiMod + elvalasztoKarakter + szallitasiMod + elvalasztoKarakter +
                ar + elvalasztoKarakter + szamlazottPostaKolts + elvalasztoKarakter;

            if (szamlaSzam != "")
            {
                szoveg += szamlaSzam;
            }
            else 
            {
                szoveg += "=HAHIBA(KEREKÍTÉS(FKERES(Z1386;Postaktsg!$A$1:$B$101;2;HAMIS)+ HA((AA1386 + AB1386) < FKERES(Z1386; Postaktsg!$A$1:$C$101; 3; HAMIS);FKERES(Z1386; Postaktsg!$A$1:$D$101; 4; HAMIS); KEREKÍTÉS((AA1386 + AB1386) * FKERES(Z1386; Postaktsg!$A$1:$E$101; 5; HAMIS); 0))+((AA1386 + AB1386) - HAHIBA(FKERES(Z1386; Postaktsg!$A$1:$B$101; 2; HAMIS)+HA((AA1386 + AB1386) < FKERES(Z1386; Postaktsg!$A$1:$C$101; 3; HAMIS);FKERES(Z1386; Postaktsg!$A$1:$D$24; 4; HAMIS); KEREKÍTÉS((AA1386 + AB1386) * FKERES(Z1386; Postaktsg!$A$1:$E$101; 5; HAMIS); 0)); \"\"))*FKERES(Z1386; Postaktsg!$A$1:$F$101; 6; HAMIS)*1,27; 0); \"\")";
            }

            szoveg += elvalasztoKarakter;

            return szoveg;
        }

    }
}
