using MoneyAdministrator.Common.DTOs;
using Pdf2Image.Import.HSBC;
using Pdf2Image.Import.Supervielle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdf2Image.Import
{
    public class ImportSummaryPdf
    {
        private string _bankName;
        private string _brandName;

        public ImportSummaryPdf(string bankName, string brandName)
        { 
            _bankName = bankName.ToLower();
            _brandName = brandName.ToLower();
        }

        public CreditCardSummaryDto GetCreditCardSummary(string pdfFilePath)
        {
            var bank = Compatibility.Banks.Where(x => x.Name == _bankName).FirstOrDefault();
            if (bank is null)
                throw new Exception($"No es posible importar tarjetas del banco {_bankName}");

            if (!bank.Brands.Contains(_brandName))
                throw new Exception($"El banco {_brandName} no soporta tarjetas {_brandName}");

            if (_bankName == Compatibility.HSBC.Name)
                return HsbcImporter.ExtractData(pdfFilePath, _brandName);
            if (_bankName == Compatibility.Supervielle.Name)
                return SpvImporter.ExtractData(pdfFilePath, _brandName);
            else
                throw new Exception($"No es posible importar resumenes de tarjetas del banco {_bankName}");
        }
    }
}
