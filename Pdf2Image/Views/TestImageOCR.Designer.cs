using System.Drawing;
using System.Windows.Forms;

namespace Pdf2Image.Views
{
    partial class TestImageOCR
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtPdfFilePath = new TextBox();
            label1 = new Label();
            btnLoadPdf = new Button();
            btnSearchFolder = new Button();
            label2 = new Label();
            txtImageOutput = new TextBox();
            btnConvertToImage = new Button();
            txtOutputOcr = new TextBox();
            txtImageInput = new TextBox();
            txtX = new TextBox();
            btnRunOcr = new Button();
            label3 = new Label();
            label4 = new Label();
            groupBox1 = new GroupBox();
            label7 = new Label();
            txtHeight = new TextBox();
            label6 = new Label();
            txtWidth = new TextBox();
            label5 = new Label();
            txtY = new TextBox();
            btnLoadImage = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // txtPdfFilePath
            // 
            txtPdfFilePath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPdfFilePath.Location = new Point(12, 27);
            txtPdfFilePath.Name = "txtPdfFilePath";
            txtPdfFilePath.Size = new Size(749, 23);
            txtPdfFilePath.TabIndex = 0;
            txtPdfFilePath.Text = "E:\\.Mega\\Desarrollo\\Repositorios\\C#\\.Windows Forms\\MoneyAdministrator_testFiles\\.Test\\hsbc_mastercard_2023-03-30_4p.pdf";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(71, 15);
            label1.TabIndex = 1;
            label1.Text = "Ruta del pdf";
            // 
            // btnLoadPdf
            // 
            btnLoadPdf.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLoadPdf.Location = new Point(767, 25);
            btnLoadPdf.Name = "btnLoadPdf";
            btnLoadPdf.Size = new Size(98, 25);
            btnLoadPdf.TabIndex = 2;
            btnLoadPdf.Text = "buscar";
            btnLoadPdf.UseVisualStyleBackColor = true;
            btnLoadPdf.Click += btnLoadPdf_Click;
            // 
            // btnSearchFolder
            // 
            btnSearchFolder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSearchFolder.Location = new Point(767, 74);
            btnSearchFolder.Name = "btnSearchFolder";
            btnSearchFolder.Size = new Size(98, 25);
            btnSearchFolder.TabIndex = 5;
            btnSearchFolder.Text = "buscar";
            btnSearchFolder.UseVisualStyleBackColor = true;
            btnSearchFolder.Click += btnSearchFolder_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 58);
            label2.Name = "label2";
            label2.Size = new Size(80, 15);
            label2.TabIndex = 4;
            label2.Text = "Ruta de salida";
            // 
            // txtImageOutput
            // 
            txtImageOutput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtImageOutput.Location = new Point(12, 76);
            txtImageOutput.Name = "txtImageOutput";
            txtImageOutput.Size = new Size(749, 23);
            txtImageOutput.TabIndex = 3;
            txtImageOutput.Text = "E:\\.Mega\\Desarrollo\\Repositorios\\C#\\.Windows Forms\\MoneyAdministrator_testFiles\\.Test\\";
            // 
            // btnConvertToImage
            // 
            btnConvertToImage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnConvertToImage.Location = new Point(767, 122);
            btnConvertToImage.Name = "btnConvertToImage";
            btnConvertToImage.Size = new Size(98, 25);
            btnConvertToImage.TabIndex = 6;
            btnConvertToImage.Text = "Convertir";
            btnConvertToImage.UseVisualStyleBackColor = true;
            btnConvertToImage.Click += btnConvertToImage_Click;
            // 
            // txtOutputOcr
            // 
            txtOutputOcr.Location = new Point(12, 287);
            txtOutputOcr.Multiline = true;
            txtOutputOcr.Name = "txtOutputOcr";
            txtOutputOcr.Size = new Size(714, 277);
            txtOutputOcr.TabIndex = 7;
            // 
            // txtImageInput
            // 
            txtImageInput.Location = new Point(12, 260);
            txtImageInput.Name = "txtImageInput";
            txtImageInput.Size = new Size(749, 23);
            txtImageInput.TabIndex = 8;
            txtImageInput.Text = "E:\\.Mega\\Desarrollo\\Repositorios\\C#\\.Windows Forms\\MoneyAdministrator_testFiles\\.Test\\OcrTest\\";
            // 
            // txtX
            // 
            txtX.Location = new Point(60, 22);
            txtX.Name = "txtX";
            txtX.Size = new Size(58, 23);
            txtX.TabIndex = 9;
            // 
            // btnRunOcr
            // 
            btnRunOcr.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRunOcr.Location = new Point(732, 441);
            btnRunOcr.Name = "btnRunOcr";
            btnRunOcr.Size = new Size(127, 25);
            btnRunOcr.TabIndex = 10;
            btnRunOcr.Text = "ejecutar OCR";
            btnRunOcr.UseVisualStyleBackColor = true;
            btnRunOcr.Click += btnRunOcr_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 240);
            label3.Name = "label3";
            label3.Size = new Size(102, 15);
            label3.TabIndex = 11;
            label3.Text = "Ruta de la imagen";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(40, 25);
            label4.Name = "label4";
            label4.Size = new Size(14, 15);
            label4.TabIndex = 12;
            label4.Text = "X";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(txtHeight);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(txtWidth);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(txtY);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(txtX);
            groupBox1.Location = new Point(732, 287);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(133, 148);
            groupBox1.TabIndex = 16;
            groupBox1.TabStop = false;
            groupBox1.Text = "Region del OCR";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(14, 112);
            label7.Name = "label7";
            label7.Size = new Size(43, 15);
            label7.TabIndex = 18;
            label7.Text = "Height";
            // 
            // txtHeight
            // 
            txtHeight.Location = new Point(60, 109);
            txtHeight.Name = "txtHeight";
            txtHeight.Size = new Size(58, 23);
            txtHeight.TabIndex = 17;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(18, 83);
            label6.Name = "label6";
            label6.Size = new Size(39, 15);
            label6.TabIndex = 16;
            label6.Text = "Width";
            // 
            // txtWidth
            // 
            txtWidth.Location = new Point(60, 80);
            txtWidth.Name = "txtWidth";
            txtWidth.Size = new Size(58, 23);
            txtWidth.TabIndex = 15;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(40, 54);
            label5.Name = "label5";
            label5.Size = new Size(14, 15);
            label5.TabIndex = 14;
            label5.Text = "Y";
            // 
            // txtY
            // 
            txtY.Location = new Point(60, 51);
            txtY.Name = "txtY";
            txtY.Size = new Size(58, 23);
            txtY.TabIndex = 13;
            // 
            // btnLoadImage
            // 
            btnLoadImage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLoadImage.Location = new Point(767, 258);
            btnLoadImage.Name = "btnLoadImage";
            btnLoadImage.Size = new Size(98, 25);
            btnLoadImage.TabIndex = 17;
            btnLoadImage.Text = "buscar";
            btnLoadImage.UseVisualStyleBackColor = true;
            btnLoadImage.Click += btnLoadImage_Click;
            // 
            // TestImageOCR
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(871, 576);
            Controls.Add(btnLoadImage);
            Controls.Add(groupBox1);
            Controls.Add(label3);
            Controls.Add(btnRunOcr);
            Controls.Add(txtImageInput);
            Controls.Add(txtOutputOcr);
            Controls.Add(btnConvertToImage);
            Controls.Add(btnSearchFolder);
            Controls.Add(label2);
            Controls.Add(txtImageOutput);
            Controls.Add(btnLoadPdf);
            Controls.Add(label1);
            Controls.Add(txtPdfFilePath);
            Name = "TestImageOCR";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtPdfFilePath;
        private Label label1;
        private Button btnLoadPdf;
        private Button btnSearchFolder;
        private Label label2;
        private TextBox txtImageOutput;
        private Button btnConvertToImage;
        private TextBox txtOutputOcr;
        private TextBox txtImageInput;
        private TextBox txtX;
        private Button btnRunOcr;
        private Label label3;
        private Label label4;
        private GroupBox groupBox1;
        private Label label7;
        private TextBox txtHeight;
        private Label label6;
        private TextBox txtWidth;
        private Label label5;
        private TextBox txtY;
        private Button btnLoadImage;
    }
}