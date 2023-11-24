using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LezerVagoHazszam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            /*string fileNev = "Márknak.txt";

            StreamReader str = new StreamReader(fileNev);

            foreach (string megrendeles in str.ReadToEnd().Split(new string[] { "Eladás" }, StringSplitOptions.None)) 
            {
                if (megrendeles != "")
                {                    
                    richTextBoxInput.Text += feldolgozMegrendelest(megrendeles).atirCSVFormat() + "\n";
                }
            }

            //MessageBox.Show(i + "");

            str.Close();*/

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_DoubleClick(object sender, EventArgs e)
        {
            richTextBoxInput.Text = "";

            richTextBoxInput.Paste();

            richTextBoxOutput.Text = feldolgozMegrendelest(richTextBoxInput.Text).atirCSVFormat();

            System.Windows.Forms.Clipboard.SetText(richTextBoxOutput.Text);
        }

        private Aru feldolgozMegrendelest(string megrendelesSzoveg)
        {

            Aru aru = new Aru();

            string szoveg = "";

            megrendelesSzoveg = megrendelesSzoveg.Trim();

            try
            {

            szoveg = megrendelesSzoveg.Split(new string[] { " vásárlónak" }, StringSplitOptions.None)[1];
            szoveg = szoveg.Split('\n')[1];

            if (!szoveg.Contains("-én") && !szoveg.Contains("-án"))
            {
                aru.idopont = DateTime.Parse(szoveg);
            }
            else
            {
                szoveg = szoveg.Remove(szoveg.Length - 4, 4);
                aru.idopont = DateTime.Parse(szoveg);
            }

            try
            {
                szoveg = megrendelesSzoveg.Split(new string[] { "Termék ára" }, StringSplitOptions.None)[1];
                szoveg = szoveg = szoveg.Split('\n')[1];
            }
            catch 
            {
                szoveg = megrendelesSzoveg.Split(new string[] { "RENDELÉSRE" }, StringSplitOptions.None)[1];
                szoveg = szoveg = szoveg.Split('\n')[2];
            }
            szoveg = szoveg.Remove(szoveg.Length - 3, 3);
            szoveg = string.Concat(szoveg.Where(c => !Char.IsWhiteSpace(c)));
            aru.ar = Convert.ToInt32(szoveg);

            szoveg = megrendelesSzoveg.Split(new string[] { "logo" }, StringSplitOptions.None)[0];
            szoveg = szoveg.Split('\n').Last<string>();
            aru.szallitasiMod = szoveg;

            aru.szallitasiMod = aru.szallitasiMod.Trim();

            try
            {
                szoveg = megrendelesSzoveg.Split(new string[] { "Termék ára" }, StringSplitOptions.None)[1];
                szoveg = szoveg.Split(new string[] { "Szállítás" }, StringSplitOptions.None)[1];
            }
            catch
            {
                szoveg = megrendelesSzoveg.Split(new string[] { "RENDELÉSRE" }, StringSplitOptions.None)[1];
                szoveg = szoveg.Split(new string[] { "Szállítás" }, StringSplitOptions.None)[3];
            }

            szoveg = szoveg.Split(new string[] { "Ft" }, StringSplitOptions.None)[0];
            //szoveg = szoveg.Trim();
            szoveg = string.Concat(szoveg.Where(c => !Char.IsWhiteSpace(c)));
            aru.szamlazottPostaKolts = Convert.ToInt32(szoveg);

            if (megrendelesSzoveg.Split(new string[] { "Számlaszám:" }, StringSplitOptions.None).Count() > 1)
            {
                szoveg = megrendelesSzoveg.Split(new string[] { "Számlaszám:" }, StringSplitOptions.None)[1];
                szoveg = szoveg.Split('\n')[0];
                aru.szamlaSzam = szoveg;
            }
            else 
            {
                aru.szamlaSzam = "";
            }

            aru.szamlaSzam = aru.szamlaSzam.Trim();

            szoveg = megrendelesSzoveg.Split(new string[] { "Megrendelő" }, StringSplitOptions.None)[1];
            szoveg = szoveg.Split('\n')[2];
            aru.vasarloNeve = szoveg;

            aru.vasarloNeve = aru.vasarloNeve.Trim();

            if (megrendelesSzoveg.Contains("Fizetési státusz\nFizetve"))
            {
                aru.fizetesiMod = "barion";
            }
            else
            {
                aru.fizetesiMod = "nincs fizetve";
            }

            aru.fizetesiMod = aru.fizetesiMod.Trim();

            if (megrendelesSzoveg.Contains("utcanévtábla") || megrendelesSzoveg.Contains("házszámtábla"))
            {
                szoveg = megrendelesSzoveg.Split(new string[] { "- méret: " }, StringSplitOptions.None)[1];
                szoveg = szoveg.Split('\n')[0];
                szoveg = szoveg.Remove(0, 3);
                //méret
                aru.termeknev = "házszámtábla, " + szoveg;
            }
            else if (megrendelesSzoveg.Contains("5 forintos emlékérme tartó"))
            {
                aru.termeknev = "5 forintos emlékérme tartó";
            }
            else if (megrendelesSzoveg.Contains("Gyerek bejelentő ajándék"))
            {
                aru.termeknev = "Gyerek bejelentő ajándék";
            }
            else if (megrendelesSzoveg.Contains("Római számos hangtalan falióra, Hangtalan óraszerkezettel"))
            {

                szoveg = megrendelesSzoveg.Split(new string[] { "- méret: " }, StringSplitOptions.None)[1];
                szoveg = szoveg.Split('\n')[0];
                szoveg = szoveg.Remove(0, 4);
                //méret
                aru.termeknev = "Római számos falióra, " + szoveg;

            }
            else if (megrendelesSzoveg.Contains("Különleges dominó óra, Gyerekszobába hangtalan játékos falióra"))
            {

                    szoveg = megrendelesSzoveg.Split(new string[] { "- méret: " }, StringSplitOptions.None)[1];
                    szoveg = szoveg.Split('\n')[0];
                    szoveg = szoveg.Remove(szoveg.Length - 7, 7);
                    //méret
                    aru.termeknev = "Dominó óra, " + szoveg;

                }
            else
            {
                aru.termeknev = "Ismeretlen áru!";
                MessageBox.Show("Ismeretlen áru!");
            }

            aru.termeknev = aru.termeknev.Trim();
            }
            catch (Exception e)
            {
                MessageBox.Show("Hibás beviteli adat!\n\n" + e.Message);
            }


            return aru;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(richTextBoxOutput.Text);
        }
    }
}
