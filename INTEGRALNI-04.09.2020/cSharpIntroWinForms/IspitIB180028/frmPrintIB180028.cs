using Microsoft.Reporting.WinForms;
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
    public partial class frmPrintIB180028 : Form
    {
        private List<KorisniciPorukeIB180028> list;

        public frmPrintIB180028()
        {
            InitializeComponent();
        }

        public frmPrintIB180028(List<KorisniciPorukeIB180028> list):this()
        {
            this.list = list;
        }

        private void frmPrintIB180028_Load(object sender, EventArgs e)
        {
            dsPoruke.PorukeDataTable tbl = new dsPoruke.PorukeDataTable();
            foreach (var poruku in list)
            {
                var red = tbl.NewPorukeRow();
                red.Datum = poruku.Datum.ToString();
                red.Sadrzaj = poruku.Sadrzaj;
                red.Slika = poruku.Slika;
                tbl.AddPorukeRow(red);
            }
            var rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = tbl;
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            this.reportViewer1.RefreshReport();
        }
    }
}
