using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LezerVagoHazszam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

            string szoveg = "";

            Aru aru = new Aru();

            richTextBoxInput.Paste();

            /*try
            {*/
                szoveg = richTextBoxInput.Text.Split(new string[] { " vásárlónak\n" }, StringSplitOptions.None)[1];
            szoveg = szoveg.Split('\n')[0];

            if (!szoveg.Contains("-én"))
            {
                aru.idopont = DateTime.Parse(szoveg);
            }
            else
            {
                szoveg = szoveg.Remove(szoveg.Length - 3, 3);
                MessageBox.Show(szoveg);
                aru.idopont = DateTime.Parse(szoveg);
                MessageBox.Show(aru.idopont + "");
            }

                szoveg = richTextBoxInput.Text.Split(new string[] { "Termék ára\n" }, StringSplitOptions.None)[1];
            szoveg = szoveg = szoveg.Split('\n')[0];
            szoveg = szoveg.Remove(szoveg.Length - 2, 2);
                szoveg = string.Concat(szoveg.Where(c => !Char.IsWhiteSpace(c)));
            aru.ar = Convert.ToInt32(szoveg);

                szoveg = richTextBoxInput.Text.Split(new string[] { "logo" }, StringSplitOptions.None)[0];
                szoveg = szoveg.Split('\n').Last<string>();
                aru.szallitasiMod = szoveg;

                szoveg = richTextBoxInput.Text.Split(new string[] { "Szállítás\n\n" }, StringSplitOptions.None)[1];
                szoveg = szoveg.Split(new string[] { "Ft" }, StringSplitOptions.None)[0];
                szoveg = szoveg.Trim();
                szoveg = string.Concat(szoveg.Where(c => !Char.IsWhiteSpace(c)));
                aru.szamlazottPostaKolts = Convert.ToInt32(szoveg);

                szoveg = richTextBoxInput.Text.Split(new string[] { "Számlaszám:\n" }, StringSplitOptions.None)[1];
                szoveg = szoveg.Split('\n')[0];
                aru.szamlaSzam = szoveg;

                szoveg = richTextBoxInput.Text.Split(new string[] { "Megrendelő\n" }, StringSplitOptions.None)[1];
                szoveg = szoveg.Split('\n')[1];
                aru.vasarloNeve = szoveg;

                if (richTextBoxInput.Text.Contains("Fizetési státusz\nFizetve"))
                {
                    aru.fizetesiMod = "barion";
                }
                else
                {
                    aru.fizetesiMod = "nincs fizetve";
                }

                if (richTextBoxInput.Text.Contains("utcanévtábla"))
                {
                    szoveg = richTextBoxInput.Text.Split(new string[] { "- méret: " }, StringSplitOptions.None)[1];
                    szoveg = szoveg.Split('\n')[0];
                    szoveg = szoveg.Remove(0, 3);
                    //méret
                    aru.termeknev = "házszám, " + szoveg;
                }
                else
                {
                    MessageBox.Show("Ismeretlen áru!");
                }

                richTextBoxOutput.Text = aru.atirCSVFormat();

                System.Windows.Forms.Clipboard.SetText(richTextBoxOutput.Text);
            /*}
            catch 
            {
                MessageBox.Show("Hibás beviteli adat!\n\n");
            }*/

        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(richTextBoxOutput.Text);
        }
    }
}
