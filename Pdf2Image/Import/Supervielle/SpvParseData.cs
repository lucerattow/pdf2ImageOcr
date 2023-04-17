using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.DTOs.Enums;
using Pdf2Image.Import.DTOs;
using Pdf2Image.Import.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pdf2Image.Import.Supervielle
{
    public class SpvParseData
    {
        private string _brandName;

        private SpvParseData(string brandName) 
        { 
            _brandName = brandName;
        }

        public static CreditCardSummaryDto GetDto(string brandName, TransactionsTableDto table)
        {
            var spvCleanStrings = new SpvCleanStrings(brandName);
            table.AllText = spvCleanStrings.CleanAllText(table.AllText);
            table.Date = spvCleanStrings.CleanDate(table.Date);
            table.Ars = spvCleanStrings.CleanArs(table.Ars);
            table.Usd = spvCleanStrings.CleanUsd(table.Usd);

            var spvParseData = new SpvParseData(brandName);
            var summary = spvParseData.GetGeneralData(table);

            summary.AddDetailDto(spvParseData.GetSummaryData(table));
            summary.AddDetailDto(spvParseData.GetDetailsData(table));
            summary.AddDetailDto(spvParseData.GetTaxesAndMaintenanceData(table));

            return summary;
        }

        private CreditCardSummaryDto GetGeneralData(TransactionsTableDto table)
        {
            var summary = new CreditCardSummaryDto();
            var lines = new SpvStringsData(_brandName).GetGeneralData(table.AllText);

            var index = 0;
            var date = "";

            #region Obtengo: Fecha cierre, Periodo

            //Fecha de cierre
            index = lines[0].IndexOf(":");
            date = lines[0].Substring(index + 1).Trim();
            summary.Date = DateTimeTools.ConvertToDateTime(date, "yyyy-MM-dd");

            //Periodo
            int month = (summary.Date.Day >= ModuleConfigs.DayStartPeriod) ? summary.Date.Month + 1 : summary.Date.Month;
            int year = summary.Date.Year;

            if (month == 13)
            {
                month = 1;
                year++;
            }

            summary.Period = new DateTime(year, month, 1);

            #endregion

            #region Obtengo: Fecha vencimiento, Pago minimo

            //Fecha vencimiento
            date = lines[1].Split(" ").ToList().Where(x => !string.IsNullOrEmpty(x)).ToList()[0].Trim();
            summary.Expiration = DateTimeTools.ConvertToDateTime(date, "yyyy-MM-dd");

            //Pago minimo
            var minimumPayment = lines[1].Split(" ").ToList().Where(x => !string.IsNullOrEmpty(x)).ToList()[3].Trim();
            summary.MinimumPayment = DecimalTools.ParseDecimal(minimumPayment);

            #endregion

            #region Obtengo: Proximo cierre, Proximo vencimiento

            index = lines[2].IndexOf(":");
            var nextDate = lines[2].Substring(index + 1, 11).Trim();
            summary.NextDate = DateTimeTools.ConvertToDateTime(nextDate, "yyyy-MM-dd");

            index = lines[2].LastIndexOf(":");
            var nextExpiration = lines[2].Substring(index + 1).Trim();
            summary.NextExpiration = DateTimeTools.ConvertToDateTime(nextExpiration, "yyyy-MM-dd");

            #endregion

            return summary;
        }

        private List<CreditCardSummaryDetailDto> GetSummaryData(TransactionsTableDto table)
        {
            var results = new List<CreditCardSummaryDetailDto>();

            var spvStringsData = new SpvStringsData(_brandName);
            var lines = spvStringsData.GetSummaryData(table.AllText);
            var dateLines = spvStringsData.GetSummaryDataSections(table.Date);
            var arsLines = spvStringsData.GetSummaryDataSections(table.Ars);
            var usdLines = spvStringsData.GetSummaryDataSections(table.Usd);

            for (int i = 0; i < lines.Count; i++)
            {
                var lastIndex = 0;
                var date = "";
                var value = "";
                var aux = "";

                var detailDto = new CreditCardSummaryDetailDto();
                detailDto.Type = CreditCardSummaryDetailType.Summary;

                //Obtengo: valor USD
                lastIndex = lines[i].LastIndexOf(" ");
                value = lines[i].Substring(lastIndex).Trim();
                aux = lines[i].Substring(0, lastIndex);

                //Seteo: valor USD
                if (usdLines.Count > 0 && usdLines[0] == value)
                {
                    detailDto.AmountUsd = DecimalTools.ParseDecimal(value);
                    usdLines.Remove(usdLines[0]);

                    //Obtengo el valor de ARS
                    lastIndex = aux.LastIndexOf(" ");
                    value = aux.Substring(lastIndex + 1).Trim();
                }

                //Obtengo: valor ARS
                if (arsLines.Count > 0 && arsLines[0] == value)
                {
                    detailDto.AmountArs = DecimalTools.ParseDecimal(value);
                    arsLines.Remove(arsLines[0]);
                    aux = lines[i].Substring(0, lastIndex);
                }

                //saldo anterior
                if (i == 0)
                {
                    //Obtengo: Descripcion
                    detailDto.Description = aux.Trim();

                    detailDto.Date = new DateTime(1, 1, 1);
                    detailDto.Installments = "";

                    results.Add(detailDto);
                    continue;
                }

                //Obtengo: Fecha
                date = aux.Substring(0, 10).Trim();
                aux = aux.Substring(10).Trim();
                if (dateLines.Count > 0 && dateLines[0] == date)
                {
                    detailDto.Date = DateTimeTools.ConvertToDateTime(date, "yyyy-MM-dd");
                    dateLines.Remove(dateLines[0]);
                }

                //Obtengo: Descripcion
                detailDto.Description = aux.Trim();

                detailDto.Installments = "";

                results.Add(detailDto);
            }

            return results;
        }

        private List<CreditCardSummaryDetailDto> GetDetailsData(TransactionsTableDto table)
        {
            var results = new List<CreditCardSummaryDetailDto>();

            var spvStringsData = new SpvStringsData(_brandName);
            var lines = spvStringsData.GetDetailsData(table.AllText);
            var dateLines = spvStringsData.GetDetailsDataSections(table.Date);
            var arsLines = spvStringsData.GetDetailsDataSections(table.Ars);
            var usdLines = spvStringsData.GetDetailsDataSections(table.Usd);

            for (int i = 0; i < lines.Count; i++)
            {
                var lastIndex = 0;
                var date = "";
                var value = "";
                var aux = "";

                var detailDto = new CreditCardSummaryDetailDto();
                detailDto.Type = CreditCardSummaryDetailType.Details;

                //Obtengo: valor USD
                lastIndex = lines[i].LastIndexOf(" ");
                value = lines[i].Substring(lastIndex).Trim();
                aux = lines[i].Substring(0, lastIndex);

                if (usdLines.Count > 0 && usdLines[0] == value)
                {
                    detailDto.AmountUsd = DecimalTools.ParseDecimal(value);
                    usdLines.Remove(usdLines[0]);

                    //Obtengo el valor de ARS
                    lastIndex = aux.LastIndexOf(" ");
                    value = aux.Substring(lastIndex + 1).Trim();
                }

                //Obtengo: valor ARS
                if (arsLines.Count > 0 && arsLines[0] == value)
                {
                    detailDto.AmountArs = DecimalTools.ParseDecimal(value);
                    arsLines.Remove(arsLines[0]);
                }

                //Obtengo: Fecha
                date = aux.Substring(0, 10).Trim();
                aux = aux.Substring(10).Trim();
                if (dateLines.Count > 0 && dateLines[0] == date)
                {
                    detailDto.Date = DateTimeTools.ConvertToDateTime(date, "yyyy-MM-dd");
                    dateLines.Remove(dateLines[0]);
                }

                //Obtengo: Descripcion
                detailDto.Description = aux.Trim();

                detailDto.Installments = "";

                results.Add(detailDto);
            }

            return results;
        }

        private List<CreditCardSummaryDetailDto> GetTaxesAndMaintenanceData(TransactionsTableDto table)
        {
            var results = new List<CreditCardSummaryDetailDto>();

            var spvStringsData = new SpvStringsData(_brandName);
            var lines = spvStringsData.GetTaxesAndMaintenanceData(table.AllText);
            var arsLines = spvStringsData.GetTaxesAndMaintenanceDataSections(table.Ars);
            var usdLines = spvStringsData.GetTaxesAndMaintenanceDataSections(table.Usd);

            for (int i = 0; i < lines.Count; i++)
            {
                var lastIndex = 0;
                var value = "";
                var aux = "";

                var detailDto = new CreditCardSummaryDetailDto();
                detailDto.Type = CreditCardSummaryDetailType.TaxesAndMaintenance;

                //Compruebo si la linea final es la linea de SALDO TOTAL
                if (i + 1 == lines.Count)
                {
                    if (lines[i].Contains(arsLines.Last()))
                        continue;
                }

                //Obtengo: valor USD
                lastIndex = lines[i].LastIndexOf(" ");
                value = lines[i].Substring(lastIndex).Trim();
                aux = lines[i].Substring(0, lastIndex);

                if (usdLines.Count > 0 && usdLines[0] == value)
                {
                    detailDto.AmountUsd = DecimalTools.ParseDecimal(value);
                    usdLines.Remove(usdLines[0]);

                    //Obtengo el valor de ARS
                    lastIndex = aux.LastIndexOf(" ");
                    value = aux.Substring(lastIndex + 1).Trim();
                }

                //Obtengo: valor ARS
                if (arsLines.Count > 0 && arsLines[0] == value)
                {
                    detailDto.AmountArs = DecimalTools.ParseDecimal(value);
                    arsLines.Remove(arsLines[0]);
                }

                //Obtengo: Descripcion
                detailDto.Description = aux.Trim();

                detailDto.Date = new DateTime(1, 1, 1);
                detailDto.Installments = "";

                results.Add(detailDto);
            }

            return results;
        }
    }
}
