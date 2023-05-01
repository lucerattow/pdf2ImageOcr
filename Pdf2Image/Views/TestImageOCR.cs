using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Pdf2Image.OtherTries;
using Spire.Pdf;
using Spire.Pdf.Graphics;

namespace Pdf2Image.Views
{
    public partial class TestImageOCR : Form
    {
        public TestImageOCR()
        {
            InitializeComponent();
        }

        private void btnLoadPdf_Click(object sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog()
            {
                Filter = $"Archivos PDF (*.pdf)|*.pdf",
                Title = $"Abrir PDF",
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtPdfFilePath.Text = openFileDialog.FileName;
            }
        }

        private void btnSearchFolder_Click(object sender, EventArgs e)
        {
            using var folderBrowseDialog = new FolderBrowserDialog();

            if (folderBrowseDialog.ShowDialog() == DialogResult.OK)
            {
                txtImageOutput.Text = folderBrowseDialog.SelectedPath;
            }
        }

        private void btnConvertToImage_Click(object sender, EventArgs e)
        {
            SpirePDF.ConvertPdfToImage(txtPdfFilePath.Text, txtImageOutput.Text);
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog()
            {
                InitialDirectory = Path.GetDirectoryName(txtImageInput.Text),
                Title = $"Abrir imagen",
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtImageInput.Text = openFileDialog.FileName;
            }
        }

        private void btnRunOcr_Click(object sender, EventArgs e)
        {
            var x = int.Parse(txtX.Text);
            var y = int.Parse(txtY.Text);
            var width = int.Parse(txtWidth.Text);
            var height = int.Parse(txtHeight.Text);
            txtOutputOcr.Text = TesseractOcrImage.GetTextFromImageRegion(txtImageInput.Text, new Rectangle(x, y, width, height));
        }
    }
}