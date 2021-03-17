using DLWMS.WinForms.Entiteti;
using DLWMS.WinForms.Helpers;
using DLWMS.WinForms.Izvjestaji;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DLWMS.WinForms.Forme
{
    public partial class frmStudenti : Form
    {
        KonekcijaNaBazu _baza = DLWMSdb.Baza; 

        public frmStudenti()
        {
            InitializeComponent();
            dgvStudenti.AutoGenerateColumns = false;
        }

        private void frmStudenti_Load(object sender, EventArgs e)
        {
            UcitajPodatkeOStudentima();
            UcitajGodineStudija();
            UcitajAktivnosti(); 
        }

        private void UcitajAktivnosti()
        {
            List<string> lista = new List<string>()
            {
                "Svi","Aktivni","Neaktivni"
            };
            comboBox2.DataSource = lista;
        }

        private void UcitajGodineStudija()
        {
            List<string> lista = new List<string>()
            {
                "Sve","1","2","3"
            };
            comboBox1.DataSource = lista;
        }

        private void btnNoviStudent_Click(object sender, EventArgs e)
        {          
            PrikaziFormu(new frmNoviStudent());
            UcitajPodatkeOStudentima();
        }

        private void UcitajPodatkeOStudentima(List<Student> studenti = null)
        {
            var rez = new List<Student>();
            rez = studenti ?? _baza.Studenti.ToList();
            dgvStudenti.DataSource = null;
            dgvStudenti.DataSource = rez;
            label4.Text = $"Broj studenata: {dgvStudenti.Rows.Count}";
            label5.Text = $"Prosjek ocjena: {Prosjek(rez)}";
        }

        private double Prosjek(List<Student> rez)
        {
            var lista = new List<Student>();
            foreach (var s in rez)
            {
                if (s.Prosjek() != 0)
                    lista.Add(s);
            }
            if (lista.Count == 0) return 0;
            return Math.Round(lista.Average(s => s.Prosjek()), 1);
        }

        private void PrikaziFormu(Form form)
        {
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        private void dgvStudenti_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var student = dgvStudenti.SelectedRows[0].DataBoundItem as Student;
            Form form = null;
            if (student != null)
            {
                if (e.ColumnIndex == 6) 
                    form = new frmStudentiPredmeti(student);
                else
                    form = new frmNoviStudent(student);
                PrikaziFormu(form);

                UcitajPodatkeOStudentima();
            }
        }
        private bool PretragaStudenata(Student s)
        {
            return s.Ime.ToLower().Contains(txtPretraga.Text.ToLower())
                    || s.Prezime.ToLower().Contains(txtPretraga.Text.ToLower());
        }
        private void txtPretraga_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void Filter()
        {
            if (string.IsNullOrEmpty(txtPretraga.Text))
            {
                errorProvider1.SetError(txtPretraga, "Obavezno polje ");
                return;
            }
            errorProvider1.Clear();
            var lista = _baza.Studenti.ToList()
              .Where(s => PretragaStudenata(s) && PretragaAktivnih(s) && PretragaGodinaStudija(s)).ToList();
            UcitajPodatkeOStudentima(lista);
        }

        private bool PretragaGodinaStudija(Student s)
        {
            return $"{s.GodinaStudija}" == comboBox1.Text || comboBox1.Text=="Sve";
        }

        private bool PretragaAktivnih(Student s)
        {
            return s.Aktivan == true && comboBox2.Text == "Aktivni" ||
                s.Aktivan == false && comboBox2.Text == "Neaktivni" ||
                comboBox2.Text == "Svi";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var print = new frmIzvjestaji(dgvStudenti.DataSource as List<Student>);
            print.Show();
        }
    }
}
