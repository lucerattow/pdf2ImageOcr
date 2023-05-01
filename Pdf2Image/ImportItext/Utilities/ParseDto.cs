using MoneyAdministrator.Common.DTOs;
using Pdf2Image.ImportText.DTOs;
using Pdf2Image.ImportText.Utilities.TypeTools;
using System;

namespace Pdf2Image.ImportText.Utilities
{
    public class ParseDto
    {
        public static CreditCardSummaryDetailDto GetCreditCardSummaryDetailDto(CreditCardDetailDto dto)
        {
            DateTime? date = null;
            decimal? amountArs = null;
            decimal? amountUsd = null;

            if (DateTimeTools.TestDate(dto.Date, dto.DateFormat))
                date = DateTimeTools.Convert(dto.Date, dto.DateFormat);

            if (!string.IsNullOrEmpty(dto.AmountArs))
                amountArs = DecimalTools.Convert(dto.AmountArs);

            if (!string.IsNullOrEmpty(dto.AmountUsd))
                amountUsd = DecimalTools.Convert(dto.AmountUsd);

            //Creo el dto y asigno los valores
            var summaryDto = new CreditCardSummaryDetailDto();
            summaryDto.Type = dto.Type;

            summaryDto.Description = dto.Description;
            summaryDto.Installments = dto.Installments;

            if (date != null)
                summaryDto.Date = (DateTime)date;

            if (amountArs != null)
                summaryDto.AmountArs = (decimal)amountArs;

            if (amountUsd != null)
                summaryDto.AmountUsd = (decimal)amountUsd;

            return summaryDto;
        }
    }
}
