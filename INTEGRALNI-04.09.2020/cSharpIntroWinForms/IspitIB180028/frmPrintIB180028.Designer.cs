namespace cSharpIntroWinForms.IspitIB180028
{
    partial class frmPrintIB180028
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dsPoruke = new cSharpIntroWinForms.IspitIB180028.dsPoruke();
            this.PorukeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dsPoruke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PorukeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "dsPoruke";
            reportDataSource1.Value = this.PorukeBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "cSharpIntroWinForms.IspitIB180028.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(718, 802);
            this.reportViewer1.TabIndex = 0;
            // 
            // dsPoruke
            // 
            this.dsPoruke.DataSetName = "dsPoruke";
            this.dsPoruke.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // PorukeBindingSource
            // 
            this.PorukeBindingSource.DataMember = "Poruke";
            this.PorukeBindingSource.DataSource = this.dsPoruke;
            // 
            // frmPrintIB180028
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 802);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmPrintIB180028";
            this.Text = "frmPrintIB180028";
            this.Load += new System.EventHandler(this.frmPrintIB180028_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dsPoruke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PorukeBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource PorukeBindingSource;
        private dsPoruke dsPoruke;
    }
}