using cSharpIntroWinForms.P10;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cSharpIntroWinForms.IspitIB180028
{
    public partial class frmPretragaIB180028 : Form
    {
        public frmPretragaIB180028()
        {
            InitializeComponent();
            dgv.AutoGenerateColumns = false;
        }

        private void txtPtreraga_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void Filter()
        {
            var lista = DLWMS.DB.KorisniciPredmeti.ToList().Where(k => PretragaPredmeta(k)).ToList();
            UcitajPodatke(lista);
        }

        private void UcitajPodatke(List<KorisniciPredmeti> lista = null)
        {
            var rez = lista ?? DLWMS.DB.KorisniciPredmeti.ToList();
            dgv.DataSource = null;
            dgv.DataSource = rez;
            lblPRosjek.Text = $"Prosjek ocjena: {Prosjek(rez)}";
        }

        private double Prosjek(List<KorisniciPredmeti> lista)
        {
            if (lista.Count == 0) return 0;
            return Math.Round(lista.Average(x => x.Ocjena),1);
        }

        private bool PretragaPredmeta(KorisniciPredmeti k)
        {
            var filter = txtPtreraga.Text.ToLower();
            return k.Predmet.Naziv.ToLower().Contains(filter);
        }

        private void frmPretragaIB180028_Load(object sender, EventArgs e)
        {
            UcitajPodatke();
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var kolona = (DataGridView)sender;
            var korisnik = dgv.SelectedRows[0].DataBoundItem as KorisniciPredmeti;
            if(kolona.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                var poruke = new frmPorukeIB1800228(korisnik.Korisnik);
                poruke.ShowDialog();
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
                return;
            long n = long.Parse(textBox2.Text);
            long suma = 0;
            await Task.Run(() => 
            {
                for (int i = 1; i <n ; i++)
                {
                    suma += i;
                }
            });
            lblSuma.Text = suma.ToString();
        }
    }
}
