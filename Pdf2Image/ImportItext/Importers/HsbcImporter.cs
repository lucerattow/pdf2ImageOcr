using MoneyAdministrator.Common.DTOs;
using Pdf2Image.ImportItext;
using Pdf2Image.ImportItext.Importers.CleanTexts;
using Pdf2Image.ImportItext.Importers.ProcessTexts;
using Pdf2Image.ImportItext.Importers.TextExtractors;
using Pdf2Image.ImportText.DTOs;
using System;
using System.Linq;

namespace Pdf2Image.Importtext.Importers
{
    public static class HsbcImporter
    {
        public static CreditCardSummaryDto ExtractData(string filename, string brandName)
        {
            var result = new CreditCardSummaryDto();

            var AllText = HsbcTextExtractor.GetTextFromPDF(filename);

            //Compruebo si el resumen corresponde al banco HSBC
            var bank = AllText.Where(x => x.ToLower().Contains(Compatibility.HSBC.Name)).FirstOrDefault()?.Trim().ToLower();
            if (bank is null || Compatibility.Banks.Where(x => x.Name.Contains(bank)) == null)
                throw new Exception("El resumen importado no es del banco HSBC");

            //Compruebo si el resumen corresponde a la marca seleccionada
            var brand = AllText.Where(x => x.ToLower().Contains(brandName)).FirstOrDefault()?.Trim().ToLower();
            if (brand is null)
                throw new Exception($"El resumen importado no es de una tarjeta {brandName}");

            //Obtengo los textos con los datos necesarios
            var table = new TransactionsTableDto();
            table.AllText = HsbcCleanText.CleanAllText(AllText);

            return HsbcProcessText.GetSummaryData(table);
        }
    }
}
