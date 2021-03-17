using DLWMS.WinForms.Helpers;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLWMS.WinForms.IspitIB180028
{
    public partial class frmPotvrdeIB180028 : Form
    {
        public frmPotvrdeIB180028()
        {
            InitializeComponent();
            dgv.AutoGenerateColumns = false;
        }

        private void frmPotvrdeIB180028_Load(object sender, EventArgs e)
        {
            UcitajPodatke();
        }

        private void UcitajPodatke()
        {
            dgv.DataSource = null;
            dgv.DataSource = DLWMSdb.Baza.StudentiPotvrde.ToList();
            label1.Text = $"Trenutno potvrda: {dgv.Rows.Count}";
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
                return;
            long n = long.Parse(textBox1.Text);
            Random rnd = new Random();
            await Task.Run(() => 
            {
                for (int i = 0; i < n; i++)
                {
                    var sp = new StudentiPotvrdeIB180028();
                    var random = rnd.Next(0, DLWMSdb.Baza.Studenti.ToList().Count);
                    sp.Student = DLWMSdb.Baza.Studenti.ToList().ElementAt(random);
                    sp.Datum = DateTime.Now.ToString("dd/MM/yyyy");
                    if (DLWMSdb.Baza.StudentiPotvrde.Count() == 0)
                        sp.Svrha = $"Regulisanje_stipendije" + i;
                    else
                        sp.Svrha = $"Regulisanje_stipendije{ DLWMSdb.Baza.StudentiPotvrde.Count() + i}";
                    sp.Izdata = rnd.Next(2) == 1;
                    DLWMSdb.Baza.StudentiPotvrde.Add(sp);
                }
                    DLWMSdb.Baza.SaveChanges();
            });
            UcitajPodatke();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            Action action = new Action(UcitajPodatke);
            await Task.Run(() => 
            {
                if (MessageBox.Show("Da li ste sigurni da želite obrisati sve podatke o potvrdama?",
                "Pitanje",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DLWMSdb.Baza.StudentiPotvrde.RemoveRange(DLWMSdb.Baza.StudentiPotvrde);
                    DLWMSdb.Baza.SaveChanges();
                }
                BeginInvoke(action);
            });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // desni klin na DLWMS.WinForms (open file in file explorer /bin/debug)
            Save(@"C:\Users\Admin\Desktop\PR3\BrojIndeksa\Ispit_Template\DLWMS.WinForms\bin\Debug\potvrdeBrojIndeksa.csv.");
        }

        private void Save(string putanja)
        {
            using (StreamWriter sw = File.AppendText(putanja))
            {
                foreach (var p in DLWMSdb.Baza.StudentiPotvrde)
                {
                    sw.WriteLine($"{p.Student}, {p.Datum},{p.Svrha}, {p.Izdata}");
                }
                sw.Close();
            }
        }

        private void btnUcitaj_Click(object sender, EventArgs e)
        {
            Ucitaj();
        }

        private void Ucitaj()
        {
            var lista = new List<string>();
            using(StreamReader sr = new StreamReader
                //provjeriti da li je putanja ispravna!
                (@"C:\Users\Admin\Desktop\PR3\BrojIndeksa\Ispit_Template\DLWMS.WinForms\bin\Debug\potvrdeBrojIndeksa.csv."))
            {
                string linija;
                while ((linija = sr.ReadLine()) != null)
                    lista.Add(linija);
            }
            lbFile.DataSource = lista;
        }
    }
}
