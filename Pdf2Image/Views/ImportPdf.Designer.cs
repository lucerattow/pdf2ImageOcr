namespace Pdf2Image.Views
{
    partial class ImportPdf
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportPdf));
            toolStrip1 = new ToolStrip();
            _tsbImportPDF = new ToolStripButton();
            _tsbCbBank = new ToolStripComboBox();
            _tsbCbBrand = new ToolStripComboBox();
            toolStripSeparator1 = new ToolStripSeparator();
            _tsbTestOCR = new ToolStripButton();
            _grd = new DataGridView();
            label1 = new Label();
            _dtpDatePeriod = new DateTimePicker();
            _txtTotalUsd = new TextBox();
            _dtpDate = new DateTimePicker();
            label2 = new Label();
            _dtpDateNext = new DateTimePicker();
            label3 = new Label();
            _dtpDateNextExpiration = new DateTimePicker();
            label4 = new Label();
            _dtpDateExpiration = new DateTimePicker();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            _txtTotalArs = new TextBox();
            label8 = new Label();
            _txtMinimumPayment = new TextBox();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_grd).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { _tsbImportPDF, _tsbCbBank, _tsbCbBrand, toolStripSeparator1, _tsbTestOCR });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(787, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // _tsbImportPDF
            // 
            _tsbImportPDF.Image = (Image)resources.GetObject("_tsbImportPDF.Image");
            _tsbImportPDF.ImageTransparentColor = Color.Magenta;
            _tsbImportPDF.Name = "_tsbImportPDF";
            _tsbImportPDF.Size = new Size(97, 22);
            _tsbImportPDF.Text = "Importar PDF";
            _tsbImportPDF.Click += _tsbImportPDF_Click;
            // 
            // _tsbCbBank
            // 
            _tsbCbBank.Items.AddRange(new object[] { "HSBC", "Supervielle" });
            _tsbCbBank.Name = "_tsbCbBank";
            _tsbCbBank.Size = new Size(121, 25);
            _tsbCbBank.Text = "Supervielle";
            // 
            // _tsbCbBrand
            // 
            _tsbCbBrand.Items.AddRange(new object[] { "Visa", "MasterCard", "American Express" });
            _tsbCbBrand.Name = "_tsbCbBrand";
            _tsbCbBrand.Size = new Size(121, 25);
            _tsbCbBrand.Text = "MasterCard";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // _tsbTestOCR
            // 
            _tsbTestOCR.Image = (Image)resources.GetObject("_tsbTestOCR.Image");
            _tsbTestOCR.ImageTransparentColor = Color.Magenta;
            _tsbTestOCR.Name = "_tsbTestOCR";
            _tsbTestOCR.Size = new Size(74, 22);
            _tsbTestOCR.Text = "Test OCR";
            _tsbTestOCR.Click += _tsbTestOCR_Click;
            // 
            // _grd
            // 
            _grd.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _grd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _grd.Location = new Point(12, 116);
            _grd.Name = "_grd";
            _grd.RowTemplate.Height = 25;
            _grd.Size = new Size(763, 310);
            _grd.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 25);
            label1.Name = "label1";
            label1.Size = new Size(48, 15);
            label1.TabIndex = 2;
            label1.Text = "Periodo";
            // 
            // _dtpDatePeriod
            // 
            _dtpDatePeriod.CustomFormat = "yyyy-MM-dd";
            _dtpDatePeriod.Format = DateTimePickerFormat.Custom;
            _dtpDatePeriod.Location = new Point(12, 43);
            _dtpDatePeriod.Name = "_dtpDatePeriod";
            _dtpDatePeriod.Size = new Size(103, 23);
            _dtpDatePeriod.TabIndex = 3;
            // 
            // _txtTotalUsd
            // 
            _txtTotalUsd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _txtTotalUsd.Location = new Point(643, 43);
            _txtTotalUsd.Name = "_txtTotalUsd";
            _txtTotalUsd.Size = new Size(132, 23);
            _txtTotalUsd.TabIndex = 4;
            // 
            // _dtpDate
            // 
            _dtpDate.CustomFormat = "yyyy-MM-dd";
            _dtpDate.Format = DateTimePickerFormat.Custom;
            _dtpDate.Location = new Point(121, 43);
            _dtpDate.Name = "_dtpDate";
            _dtpDate.Size = new Size(103, 23);
            _dtpDate.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(121, 25);
            label2.Name = "label2";
            label2.Size = new Size(87, 15);
            label2.TabIndex = 5;
            label2.Text = "Fecha resumen";
            // 
            // _dtpDateNext
            // 
            _dtpDateNext.CustomFormat = "yyyy-MM-dd";
            _dtpDateNext.Format = DateTimePickerFormat.Custom;
            _dtpDateNext.Location = new Point(121, 87);
            _dtpDateNext.Name = "_dtpDateNext";
            _dtpDateNext.Size = new Size(103, 23);
            _dtpDateNext.TabIndex = 8;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(121, 69);
            label3.Name = "label3";
            label3.Size = new Size(101, 15);
            label3.TabIndex = 7;
            label3.Text = "Proximo resumen";
            // 
            // _dtpDateNextExpiration
            // 
            _dtpDateNextExpiration.CustomFormat = "yyyy-MM-dd";
            _dtpDateNextExpiration.Format = DateTimePickerFormat.Custom;
            _dtpDateNextExpiration.Location = new Point(230, 87);
            _dtpDateNextExpiration.Name = "_dtpDateNextExpiration";
            _dtpDateNextExpiration.Size = new Size(103, 23);
            _dtpDateNextExpiration.TabIndex = 12;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(230, 69);
            label4.Name = "label4";
            label4.Size = new Size(121, 15);
            label4.TabIndex = 11;
            label4.Text = "Proximo Vencimiento";
            // 
            // _dtpDateExpiration
            // 
            _dtpDateExpiration.CustomFormat = "yyyy-MM-dd";
            _dtpDateExpiration.Format = DateTimePickerFormat.Custom;
            _dtpDateExpiration.Location = new Point(230, 43);
            _dtpDateExpiration.Name = "_dtpDateExpiration";
            _dtpDateExpiration.Size = new Size(103, 23);
            _dtpDateExpiration.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(230, 25);
            label5.Name = "label5";
            label5.Size = new Size(73, 15);
            label5.TabIndex = 9;
            label5.Text = "Vencimiento";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Location = new Point(643, 25);
            label6.Name = "label6";
            label6.Size = new Size(57, 15);
            label6.TabIndex = 13;
            label6.Text = "Total USD";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Location = new Point(505, 25);
            label7.Name = "label7";
            label7.Size = new Size(56, 15);
            label7.TabIndex = 15;
            label7.Text = "Total ARS";
            // 
            // _txtTotalArs
            // 
            _txtTotalArs.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _txtTotalArs.Location = new Point(505, 43);
            _txtTotalArs.Name = "_txtTotalArs";
            _txtTotalArs.Size = new Size(132, 23);
            _txtTotalArs.TabIndex = 14;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Location = new Point(505, 69);
            label8.Name = "label8";
            label8.Size = new Size(79, 15);
            label8.TabIndex = 17;
            label8.Text = "Pago minimo";
            // 
            // _txtMinimumPayment
            // 
            _txtMinimumPayment.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _txtMinimumPayment.Location = new Point(505, 87);
            _txtMinimumPayment.Name = "_txtMinimumPayment";
            _txtMinimumPayment.Size = new Size(132, 23);
            _txtMinimumPayment.TabIndex = 16;
            // 
            // ImportPdf
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(787, 438);
            Controls.Add(label8);
            Controls.Add(_txtMinimumPayment);
            Controls.Add(label7);
            Controls.Add(_txtTotalArs);
            Controls.Add(label6);
            Controls.Add(_dtpDateNextExpiration);
            Controls.Add(label4);
            Controls.Add(_dtpDateExpiration);
            Controls.Add(label5);
            Controls.Add(_dtpDateNext);
            Controls.Add(label3);
            Controls.Add(_dtpDate);
            Controls.Add(label2);
            Controls.Add(_txtTotalUsd);
            Controls.Add(_dtpDatePeriod);
            Controls.Add(label1);
            Controls.Add(_grd);
            Controls.Add(toolStrip1);
            Name = "ImportPdf";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ImportPdf";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_grd).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton _tsbImportPDF;
        private ToolStripButton _tsbTestOCR;
        private DataGridView _grd;
        private Label label1;
        private DateTimePicker _dtpDatePeriod;
        private TextBox _txtTotalUsd;
        private DateTimePicker _dtpDate;
        private Label label2;
        private DateTimePicker _dtpDateNext;
        private Label label3;
        private DateTimePicker _dtpDateNextExpiration;
        private Label label4;
        private DateTimePicker _dtpDateExpiration;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox _txtTotalArs;
        private Label label8;
        private TextBox _txtMinimumPayment;
        private ToolStripComboBox _tsbCbBank;
        private ToolStripComboBox _tsbCbBrand;
        private ToolStripSeparator toolStripSeparator1;
    }
}