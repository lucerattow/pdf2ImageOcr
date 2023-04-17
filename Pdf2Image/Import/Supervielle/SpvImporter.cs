using MoneyAdministrator.Common.DTOs;
using Spire.Pdf.Graphics;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;
using iText.IO.Image;
using System.Drawing;
using Pdf2Image.Views;
using Pdf2Image.Import.Utilities;
using Pdf2Image.Import.DTOs;
using Pdf2Image.Import.Regions;

namespace Pdf2Image.Import.Supervielle
{
    public class SpvImporter
    {
        private readonly string _pdfFilePath;

        public SpvImporter(string pdfFilePath) 
        { 
            _pdfFilePath = pdfFilePath;
        }

        public static CreditCardSummaryDto ExtractData(string pdfFilePath, string brandName)
        {
            var result = new CreditCardSummaryDto();
            var importer = new SpvImporter(pdfFilePath);

            var pages = PdfToImage.Convert(pdfFilePath);

            //Compruebo si el resumen corresponde al banco supervielle
            var bankRegion = SpvRegions.GetBankName();
            var bankName = ImageOcr.GetTextFromImage(pages[0], bankRegion).FirstOrDefault()?.Trim().ToLower();
            if (bankName is null || Compatibility.Banks.Where(x => x.Name.Contains(bankName)) == null)
                throw new Exception("El resumen importado no es del banco Supervielle");

            var table = new TransactionsTableDto
            {
                AllText = ImageOcr.GetTextFromImages(pages),
                Date = ImageOcr.GetTextFromImages(pages, SpvRegions.GetDateRegion()),
                Ars = ImageOcr.GetTextFromImages(pages, SpvRegions.GetArsRegion()),
                Usd = ImageOcr.GetTextFromImages(pages, SpvRegions.GetUsdRegion())
            };

            //Compruebo si el resumen corresponde a la marca seleccionada
            var brand = table.AllText.Where(x => x.ToLower().Contains(brandName)).FirstOrDefault()?.Trim().ToLower();
            if (brand is null)
                throw new Exception($"El resumen importado no es de una tarjeta {brandName}");

            return SpvParseData.GetDto(brandName, table);
        }
    }
}
