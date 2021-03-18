using cSharpIntroWinForms.P10;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations.Model;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cSharpIntroWinForms.IspitIB180028
{
    public partial class frmNovaPorukaIB180028 : Form
    {
        private Korisnik korisnik;
        private KorisniciPorukeIB180028 poruka;

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            if (imageIn == null) return null;
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }
        public Image byteArrayToImage(byte[] bytesArr)
        {
            if (bytesArr == null) return null;
            using (MemoryStream memstr = new MemoryStream(bytesArr))
            {
                Image img = Image.FromStream(memstr);
                return img;
            }
        }
        public frmNovaPorukaIB180028()
        {
            InitializeComponent();
        }

        public frmNovaPorukaIB180028(Korisnik korisnik, KorisniciPorukeIB180028 poruka=null):this()
        {
            this.korisnik = korisnik;
            this.poruka = poruka;
        }

        private void frmNovaPorukaIB180028_Load(object sender, EventArgs e)
        {
            UcitajPodatke();
        }

        private void UcitajPodatke()
        {

            txtPrimalac.ReadOnly = true;
            txtPrimalac.Text = korisnik.Ime + " " + korisnik.Prezime;
            if (poruka != null)
            {
                txtSadrzaj.Text = poruka.Sadrzaj;
                txtSadrzaj.ReadOnly = true;
                pbSlika.Image = byteArrayToImage(poruka.Slika);
            }

            /*AKO NAM JE DOZVOLJENO IZVRSITI EDIT
            txtPrimalac.ReadOnly = true;
            txtPrimalac.Text = $"{korisnik.Ime} {korisnik.Prezime}";
            var edit = poruka != null;
            if (edit)
            {
                textBox2.Text = poruka.Sadrzaj;
                pbSlika.Image = byteArrayToImage(poruka.Slika);
            }*/
        }

        private void pbSlika_Click(object sender, EventArgs e)
        {
            if (poruka != null)
                return;
            var ofd = new OpenFileDialog();
            if(ofd.ShowDialog()==DialogResult.OK)
            {
                var imeFajla = ofd.FileName;
                Image slika = Image.FromFile(imeFajla);
                pbSlika.Image = slika;
            }
            DLWMS.DB.SaveChanges();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (poruka != null)
                return;
            if (string.IsNullOrEmpty(txtSadrzaj.Text))
            {
                errorProvider1.SetError(txtSadrzaj, "Obavezno polje");
                return;
            }
            var kk = new KorisniciPorukeIB180028();
            kk.Korisnik = korisnik;
            kk.Sadrzaj = txtSadrzaj.Text;
            kk.Datum = DateTime.Now.ToString("dd/MM/yyyy");
            kk.Slika = ImageToByteArray(pbSlika.Image);
            DLWMS.DB.KorisniciPoruke.Add(kk);
            DLWMS.DB.SaveChanges();
            Close();

            //AKO NAM JE DOZVOLJENO IZVRSITI EDIT
            //var edit = poruka != null;
            //if (string.IsNullOrEmpty(txtSadrzaj.Text))
            //{
            //    errorProvider1.SetError(txtSadrzaj, "Obavezno polje");
            //    return;
            //}
            //if (!edit)
            //    poruka = new KorisniciPorukeIB180028();
            //poruka.Korisnik = korisnik;
            //poruka.Datum = DateTime.Now.ToString();
            //poruka.Sadrzaj = txtSadrzaj.Text;
            //poruka.Slika = ImageToByteArray(pbSlika.Image);
            //if (edit)
            //{
            //    DLWMS.DB.Entry(poruka).State = System.Data.Entity.EntityState.Modified;
            //    DLWMS.DB.SaveChanges();
            //}
            //else
            //{
            //    DLWMS.DB.KorisniciPoruke.Add(poruka);
            //    DLWMS.DB.SaveChanges();
            //}
            //Close();
        }
    }
}
