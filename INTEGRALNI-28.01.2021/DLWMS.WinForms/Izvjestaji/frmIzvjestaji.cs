using DLWMS.WinForms.Entiteti;
using Microsoft.Reporting.WinForms;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DLWMS.WinForms.Izvjestaji
{
    public partial class frmIzvjestaji : Form
    {
        private List<Student> studenti;

        public frmIzvjestaji()
        {
            InitializeComponent();
        }

        public frmIzvjestaji(List<Student> studenti) :this()
        {
            this.studenti = studenti;
        }

        private void frmIzvjestaji_Load(object sender, System.EventArgs e)
        {
            var tbl = new dsDLWMS.StudentiDataTable();
            int rb = 1;
            foreach (var s in studenti)
            {
                var red = tbl.NewStudentiRow();
                red.Rb = rb++;
                red.Indeks = s.Indeks;
                red.Ime = s.Ime;
                red.Prezime = s.Prezime;
                red.Spol = s.Spol.ToString();
                red.Godina = s.GodinaStudija;
                if (s.Aktivan == true)
                    red.Aktivan = "Da";
                else
                    red.Aktivan = "Ne";
                tbl.AddStudentiRow(red);        
            }
            var rds = new ReportDataSource();
            rds.Name="DataSet1";
            rds.Value = tbl;
            reportViewer1.LocalReport.DataSources.Add(rds);
            this.reportViewer1.RefreshReport();
        }
    }
}
