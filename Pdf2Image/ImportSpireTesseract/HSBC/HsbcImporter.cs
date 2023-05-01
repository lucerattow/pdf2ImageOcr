using MoneyAdministrator.Common.DTOs;
using Pdf2Image.Import.DTOs;
using Pdf2Image.Import.Regions;
using Pdf2Image.Import.Supervielle;
using Pdf2Image.Import.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdf2Image.Import.HSBC
{
    public class HsbcImporter
    {
        private readonly string _pdfFilePath;

        public HsbcImporter(string pdfFilePath)
        {
            _pdfFilePath = pdfFilePath;
        }

        public static CreditCardSummaryDto ExtractData(string pdfFilePath, string brandName)
        {
            var result = new CreditCardSummaryDto();
            var importer = new HsbcImporter(pdfFilePath);

            var pages = PdfToImage.Convert(pdfFilePath, Compatibility.HSBC.Name);

            var table = new TransactionsTableDto
            {
                AllText = ImageOcr.GetTextFromImages(pages),
                Date = ImageOcr.GetTextFromImages(pages, HsbcRegions.GetDateRegion()),
                Ars = ImageOcr.GetTextFromImages(pages, HsbcRegions.GetArsRegion()),
                Usd = ImageOcr.GetTextFromImages(pages, HsbcRegions.GetUsdRegion())
            };

            //Compruebo si el resumen corresponde al banco HSBC
            var bank = table.AllText.Where(x => x.ToLower().Contains(Compatibility.HSBC.Name)).FirstOrDefault()?.Trim().ToLower();
            if (bank is null || Compatibility.Banks.Where(x => x.Name.Contains(bank)) == null)
                throw new Exception("El resumen importado no es del banco HSBC");

            //Compruebo si el resumen corresponde a la marca seleccionada
            var brand = table.AllText.Where(x => x.ToLower().Contains(brandName)).FirstOrDefault()?.Trim().ToLower();
            if (brand is null)
                throw new Exception($"El resumen importado no es de una tarjeta {brandName}");

            return HsbcParseData.GetDto(brandName, table);
        }
    }
}
