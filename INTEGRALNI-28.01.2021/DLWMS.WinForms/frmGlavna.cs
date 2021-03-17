﻿
using DLWMS.WinForms.Entiteti;
using DLWMS.WinForms.Forme;
using DLWMS.WinForms.IspitIB180028;
using System;
using System.Windows.Forms;

namespace DLWMS.WinForms
{
    public partial class frmGlavna : Form
    {
        public frmGlavna()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Text = $"Trenutno -> {DateTime.Now.ToString()}";
        }

        private void izađiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                Poruke.IzlazIzPrograma,
                Poruke.Pitanje,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
                == DialogResult.Yes)
                Application.Exit();
        }

        private void studentiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrikaziStudentskuFormu();
        }

        private void PrikaziStudentskuFormu()
        {
            frmStudenti frmStudenti = new frmStudenti();
            frmStudenti.MdiParent = this;
            frmStudenti.Show();
        }

        private void frmGlavna_Load(object sender, EventArgs e)
        {
            PrikaziStudentskuFormu();
        }

        private void potvrdeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sp = new frmPotvrdeIB180028();
            sp.MdiParent = this;
            sp.Show();
        }
    }
}
