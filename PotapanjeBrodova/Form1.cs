using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PotapanjeBrodova
{
    public partial class Form1 : Form
    {
        int brodovi_igraca = 5;
        int brodovi_bota = 5;
        List<Button> dugmici_igraca;
        List<Button> dugmici_bota;
        int poeni_igraca = 0;
        int poeni_bota = 0;
        Random r = new Random();
        
        public Form1()
        {
            InitializeComponent();
            ucitavanje_dugmadi();
            comboBox1.Text = null;
            napad.Enabled = false;

        }



        private void label1_Click(object sender, EventArgs e)
        {
            //slucajno
        }

        private void label14_Click(object sender, EventArgs e)
        {
            //slucajno
        }

        private void igrac_bira(object sender, EventArgs e)
        {
            if (brodovi_igraca > 0)
            {
                var dugmad = (Button)sender;
                dugmad.BackColor = System.Drawing.Color.Yellow;
                dugmad.Enabled = false;
                dugmad.Tag = "brod_igraca";
                brodovi_igraca--;
            }

            if (brodovi_igraca == 0)
            {
                broj_brodova_igraca.Text = "5";
                napad.Enabled = true;
            }
        }

        private void ucitavanje_dugmadi()
        {
            dugmici_igraca = new List<Button> { A0, A1, A2, A3, A4, B0, B1, B2, B3, B4, C0, C1, C2, C3, C4, D0, D1, D2, D3, D4, E0, E1, E2, E3, E4 };
            dugmici_bota = new List<Button> { F0, F1, F2, F3, F4, G0, G1, G2, G3, G4, H0, H1, H2, H3, H4, I0, I1, I2, I3, I4, J0, J1, J2, J3, J4 };

            for (int i = 0; i < dugmici_bota.Count; i++)
            {
                comboBox1.Items.Add(dugmici_bota[i].Text);
                dugmici_bota[i].Tag = null;
            }
        }

        private void ucitavanje_igre_opet()
        {
            comboBox1.Items.Clear();

            for (int i = 0; i < dugmici_bota.Count; i++)
            {
                comboBox1.Items.Add(dugmici_bota[i].Text);
                dugmici_bota[i].Tag = null;
                dugmici_bota[i].BackColor = System.Drawing.Color.DodgerBlue;

                if(dugmici_bota[i].Enabled == false)
                {
                    dugmici_bota[i].Enabled = true;
                }
            }
            for (int i = 0; i < dugmici_igraca.Count; i++)
            {
                dugmici_igraca[i].Tag = null;
                dugmici_igraca[i].BackColor = System.Drawing.Color.DodgerBlue;

                if (dugmici_igraca[i].Enabled == false)
                {
                    dugmici_igraca[i].Enabled = true;
                }
            }
            broj_brodova_bota.Text = "x";
            broj_brodova_igraca.Text = "x";
            napad_bota.Text = "x";
            napad_igraca.Text = "x";
            brodovi_igraca = 5;
            brodovi_bota = 5;
            poeni_bota = 0;
            poeni_igraca = 0;
            napad.Enabled = false;
            bot_bira.Start();
        }

        private void bot_bira1(object sender, EventArgs e)
        { 
            int broj = r.Next(dugmici_bota.Count);

            if (dugmici_bota[broj].Tag == null && dugmici_bota[broj].Enabled == true)
            {
                dugmici_bota[broj].Tag = "brod_bota";
                brodovi_bota--;
            }


            if(brodovi_bota == 0)
            {
                broj_brodova_bota.Text = "5";
                bot_bira.Stop();
            }
        }

        private void bot_napada1(object sender, EventArgs e)
        {
            int j = r.Next(dugmici_igraca.Count);

            while(dugmici_igraca[j].Tag == "pogodjeno")
            {
                j = r.Next(dugmici_igraca.Count);
            }

            if(dugmici_igraca[j].Tag == "brod_igraca")
            {
                dugmici_igraca[j].BackColor = System.Drawing.Color.Red;
                dugmici_igraca[j].Tag = "pogodjeno";
                poeni_bota++;
                int k = int.Parse(broj_brodova_igraca.Text);
                k--;
                broj_brodova_igraca.Text = k.ToString();
                napad_bota.Text = dugmici_igraca[j].Text;
                napad.Enabled = true;
                provera.Start();
                bot_napada.Stop();
            }
            else
            {
                dugmici_igraca[j].BackColor = System.Drawing.Color.White;
                dugmici_igraca[j].Tag = "pogodjeno";
                napad_bota.Text = dugmici_igraca[j].Text;
                napad.Enabled = true;
                provera.Start();
                bot_napada.Stop();
            }
        }

        private void igrac_napada(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                var napadna_pozicija = comboBox1.Text;
                int j = dugmici_bota.FindIndex(x => x.Name == napadna_pozicija);
                
                if(dugmici_bota[j].Tag == "brod_bota")
                {
                    dugmici_bota[j].BackColor = System.Drawing.Color.Red;
                    poeni_igraca++;
                    dugmici_bota[j].Enabled = false;
                    comboBox1.Items.Remove(dugmici_bota[j].Text);
                    int k = int.Parse(broj_brodova_bota.Text);
                    k--;
                    broj_brodova_bota.Text = k.ToString();
                    napad.Enabled = false;
                    napad_igraca.Text = dugmici_bota[j].Text;
                    comboBox1.Text = null;
                    provera.Start();
                    bot_napada.Start();
                }
                else
                {
                    dugmici_bota[j].BackColor = System.Drawing.Color.White;
                    comboBox1.Items.Remove(dugmici_bota[j].Text);
                    dugmici_bota[j].Enabled = false;
                    napad.Enabled = false;
                    napad_igraca.Text = dugmici_bota[j].Text;
                    comboBox1.Text = null;
                    provera.Start();
                    bot_napada.Start();
                }

            }
            else
            {
                MessageBox.Show("Morate da izaberete polje");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //slucajno
        }

        private void da_li_je_neko_pobedio(object sender, EventArgs e)
        {
            if(poeni_igraca == 5)
            {
                provera.Interval = 99999999;
                string naslov1 = "POBEDA!!!";
                string tekst1 = "Potopili ste sve protivnicke brodove, svaka cast! Da li zelite opet da odigrate?";
                MessageBoxButtons dugmici1 = MessageBoxButtons.YesNo;
                DialogResult rezultat = MessageBox.Show(tekst1, naslov1, dugmici1);
                
                
                if (rezultat == DialogResult.Yes)
                {
                    ucitavanje_igre_opet();
                    provera.Stop();
                }
                else
                {
                    this.Close();
                    provera.Stop();
                }
                
            }
            if(poeni_bota == 5)
            {
                provera.Interval = 99999999;
                string naslov2 = "Poraz";
                string tekst2 = "Protivnik Vam je potopio sve brodove, izgubili ste. Da li zelite opet da odigrate?";
                MessageBoxButtons dugmici2 = MessageBoxButtons.YesNo;
                DialogResult rezultat = MessageBox.Show(tekst2, naslov2, dugmici2);
                
                
                if (rezultat == DialogResult.Yes)
                {
                    ucitavanje_igre_opet();
                    provera.Stop();
                }
                else
                {
                    this.Close();
                    provera.Stop();
                }
                
            }

            provera.Stop();
        }

        private void prikazi_pravila(object sender, EventArgs e)
        {
            string naslov = "Pravila";
            string tekst = "Na pocetku birate pozicije brodova tako sto kliknete na polje na koje zelite da postavite brod. Jedan brod zauzima jedno dugme. Brodova ima pet komada. Nakon toga krece faza napadanja. Iz comboboksa birate poziciju koju zelite da napadnete. Kada ste izabrali kliknete dugme \"napad\". Ko prvi potopi sve protivnicke brodove, taj je pobedio.";
            MessageBox.Show(tekst, naslov);
        }
    }
}
