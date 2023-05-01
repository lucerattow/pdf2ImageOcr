using MoneyAdministrator.Common.DTOs;
using Pdf2Image.ImportItext;
using System;
using System.Windows.Forms;

namespace Pdf2Image.Views
{
    public partial class ImportPdf : Form
    {
        public ImportPdf()
        {
            InitializeComponent();
        }

        private void _tsbTestOCR_Click(object sender, EventArgs e)
        {
            var testOCR = new TestImageOCR();
            testOCR.Show();
        }

        private void _tsbImportPDF_Click(object sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog()
            {
                Filter = $"Archivos PDF (*.pdf)|*.pdf",
                Title = $"Importar PDF",
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (new CursorWait())
                    {
                        var import = new ImportItext.Import(_tsbCbBank.Text, _tsbCbBrand.Text);
                        var a = import.ExtractDataFromPDF(openFileDialog.FileName);
                        return;
                        //var importSummaryPdf = new ImportSummaryPdf(_tsbCbBank.Text, _tsbCbBrand.Text);
                        //LoadData(importSummaryPdf.GetCreditCardSummary(openFileDialog.FileName));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error");
                }
            }
        }

        private void LoadData(CreditCardSummaryDto summary)
        {
            _dtpDatePeriod.Value = summary.Period;
            _dtpDate.Value = summary.Date;
            _dtpDateExpiration.Value = summary.Expiration;
            _dtpDateNext.Value = summary.NextDate;
            _dtpDateNextExpiration.Value = summary.NextExpiration;
            _txtMinimumPayment.Text = summary.MinimumPayment.ToString();

            _grd.DataSource = summary.CreditCardSummaryDetails;
        }
    }
}
