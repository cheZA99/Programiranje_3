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
    public partial class frmPorukeIB1800228 : Form
    {
        private Korisnik korisnik;

        public frmPorukeIB1800228()
        {
            InitializeComponent();
            dgv.AutoGenerateColumns = false;
        }

        public frmPorukeIB1800228(Korisnik korisnik):this()
        {
            this.korisnik = korisnik;
        }

        private void frmPorukeIB1800228_Load(object sender, EventArgs e)
        {
            UcitajPodatke();
        }

        private void UcitajPodatke()
        {
            dgv.DataSource = null;
            dgv.DataSource = korisnik.Poruke.ToList();
            lblKorisnik.Text = $"{korisnik.Ime} {korisnik.Prezime}";
        }

        private void btnNova_Click(object sender, EventArgs e)
        {
            var np = new frmNovaPorukaIB180028(korisnik);
            np.ShowDialog();
            UcitajPodatke();
        }

        private void dgv_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var poruka = dgv.SelectedRows[0].DataBoundItem as KorisniciPorukeIB180028;
            var np = new frmNovaPorukaIB180028(korisnik, poruka);
            np.ShowDialog();
            //UcitajPodatke();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            var print = new frmPrintIB180028(dgv.DataSource as List<KorisniciPorukeIB180028>);
            print.Show();
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var kolona = (DataGridView)sender;
            var poruka = dgv.SelectedRows[0].DataBoundItem as KorisniciPorukeIB180028;
            if (kolona.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                if (MessageBox.Show("Da li zelite obrisati poruku?", "Pitanje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DLWMS.DB.KorisniciPoruke.Remove(poruka);
                    DLWMS.DB.SaveChanges();
                    UcitajPodatke();
                }
            }
        }
    }
}
