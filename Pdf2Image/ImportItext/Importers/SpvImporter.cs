using MoneyAdministrator.Common.DTOs;
using Pdf2Image.ImportItext;
using Pdf2Image.ImportItext.CleanTexts;
using Pdf2Image.ImportItext.Importers.ProcessTexts;
using Pdf2Image.ImportItext.Importers.TextExtractors;
using Pdf2Image.ImportText.DTOs;
using System;
using System.Linq;

namespace Pdf2Image.Importtext.Importers
{
    public static class SpvImporter
    {
        public static CreditCardSummaryDto ExtractData(string filename, string brandName)
        {
            var result = new CreditCardSummaryDto();
            var cleanText = new SpvCleanText(brandName);

            var AllText = SpvTextExtractor.GetTextFromPDF(filename);
            var Date = SpvTextExtractor.GetDateTextFromPDF(filename);
            var AmountArs = SpvTextExtractor.GetAmountArsTextFromPDF(filename);
            var AmountUsd = SpvTextExtractor.GetAmountUsdTextFromPDF(filename);

            //Compruebo si el resumen corresponde al banco Supervielle
            var bank = AllText.Where(x => x.ToLower().Contains(Compatibility.Supervielle.Name)).FirstOrDefault()?.Trim().ToLower();
            if (bank is null || Compatibility.Banks.Where(x => x.Name.Contains(bank)) == null)
                throw new Exception("El resumen importado no es del banco Supervielle");

            //Compruebo si el resumen corresponde a la marca seleccionada
            var brand = AllText.Where(x => x.ToLower().Contains(brandName)).FirstOrDefault()?.Trim().ToLower();
            if (brand is null)
                throw new Exception($"El resumen importado no es de una tarjeta {brandName}");

            //Formateo los datos de la tabla
            var table = new TransactionsTableDto();
            table.AllText = cleanText.CleanAllText(AllText);
            table.Date = cleanText.CleanDate(Date);
            table.AmountArs = cleanText.CleanAmountArs(AmountArs);
            table.AmountUsd = cleanText.CleanAmountUsd(AmountUsd);

            return SpvProcessText.GetSummaryData(table);
        }
    }
}
