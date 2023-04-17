using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.DTOs.Enums;
using Pdf2Image.Import.DTOs;
using Pdf2Image.Import.Supervielle;
using Pdf2Image.Import.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdf2Image.Import.HSBC
{
    public class HsbcParseData
    {
        private string _brandName;

        private HsbcParseData(string brandName)
        {
            _brandName = brandName;
        }

        public static CreditCardSummaryDto GetDto(string brandName, TransactionsTableDto table)
        {
            var hsbcCleanStrings = new HsbcCleanStrings(brandName);
            table.AllText = hsbcCleanStrings.CleanAllText(table.AllText);
            table.Date = hsbcCleanStrings.CleanDate(table.Date);
            table.Ars = hsbcCleanStrings.CleanArs(table.Ars);
            table.Usd = hsbcCleanStrings.CleanUsd(table.Usd);

            var hsbcParseData = new HsbcParseData(brandName);
            var summary = hsbcParseData.GetGeneralData(table);

            summary.AddDetailDto(hsbcParseData.GetSummaryAndTaxesMaintenanceData(table));
            summary.AddDetailDto(hsbcParseData.GetConsumptionsData(table));

            return summary;
        }

        private CreditCardSummaryDto GetGeneralData(TransactionsTableDto table)
        {
            var summary = new CreditCardSummaryDto();
            var lines = new HsbcStringsData(_brandName).GetGeneralData(table.AllText);

            var index = 0;
            var date = "";

            //Fecha de cierre
            index = lines[0].IndexOf(":");
            date = lines[0].Substring(index + 1, 10).Trim();
            summary.Date = DateTimeTools.ConvertToDateTime(date, "dd-MMM-yy");

            //Periodo
            int month = (summary.Date.Day >= ModuleConfigs.DayStartPeriod) ? summary.Date.Month + 1 : summary.Date.Month;
            int year = summary.Date.Year;
            if (month == 13)
            {
                month = 1;
                year++;
            }
            summary.Period = new DateTime(year, month, 1);

            //Fecha vencimiento
            index = lines[1].IndexOf(":");
            date = lines[1].Substring(index + 1, 10).Trim();
            summary.Expiration = DateTimeTools.ConvertToDateTime(date, "dd-MMM-yy");

            //Pago minimo
            index = lines[1].LastIndexOf(":");
            var minimumPayment = lines[1].Substring(index + 1, lines[1].Length - (index + 1)).Trim();
            summary.MinimumPayment = DecimalTools.ParseDecimal(minimumPayment);

            //Obtengo: Proximo cierre
            index = lines[2].LastIndexOf(":");
            var nextDate = lines[2].Substring(index + 1).Trim();
            summary.NextDate = DateTimeTools.ConvertToDateTime(nextDate, "dd-MMM-yy");

            //Obtengo: Proximo vencimiento
            index = lines[3].LastIndexOf(":");
            var nextExpiration = lines[3].Substring(index + 1).Trim();
            summary.NextExpiration = DateTimeTools.ConvertToDateTime(nextExpiration, "dd-MMM-yy");

            return summary;
        }

        private List<CreditCardSummaryDetailDto> GetSummaryAndTaxesMaintenanceData(TransactionsTableDto table)
        {
            var results = new List<CreditCardSummaryDetailDto>();
            var detailType = CreditCardSummaryDetailType.Summary;

            var hsbcStringsData = new HsbcStringsData(_brandName);
            var lines = hsbcStringsData.GetSummaryMaintenanceData(table.AllText);
            var dateLines = hsbcStringsData.GetSummaryMaintenanceDataSections(table.Date);
            var arsLines = hsbcStringsData.GetSummaryMaintenanceDataSections(table.Ars);
            var usdLines = hsbcStringsData.GetSummaryMaintenanceDataSections(table.Usd);

            for (int i = 0; i < lines.Count; i++)
            {
                var lastIndex = 0;
                var date = "";
                var value = "";
                var aux = "";

                var detailDto = new CreditCardSummaryDetailDto();
                detailDto.Type = detailType;

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
                if (lines[i].Contains("SALDO ANTERIOR"))
                {
                    //Obtengo: Descripcion
                    detailDto.Description = aux.Trim();

                    detailDto.Date = new DateTime(1, 1, 1);
                    detailDto.Installments = "";

                    results.Add(detailDto);
                    continue;
                }

                //Obtengo: Fecha
                if (aux.Length > 10)
                    date = aux.Substring(0, 10).Trim();

                if (dateLines.Count > 0 && dateLines[0] == date)
                {
                    detailDto.Date = DateTimeTools.ConvertToDateTime(date, "dd-MMM-yy");
                    dateLines.Remove(dateLines[0]);
                    aux = aux.Substring(10).Trim();
                }

                //Obtengo: Descripcion
                detailDto.Description = aux.Trim();

                detailDto.Installments = "";

                //Excluyo las siguientes lineas:
                if (lines[i].Contains("SALDO PENDIENTE"))
                    continue;
                if (lines[i].Contains("TOTAL CONSUMOS DEL MES"))
                    continue;
                //Detecto el inicio de Taxes and Maintenance
                if (lines[i].Contains("SUBTOTAL"))
                {
                    detailType = CreditCardSummaryDetailType.TaxesAndMaintenance;
                    continue;
                }

                results.Add(detailDto);
            }

            return results;
        }

        public List<CreditCardSummaryDetailDto> GetConsumptionsData(TransactionsTableDto table)
        {
            var results = new List<CreditCardSummaryDetailDto>();
            var detailType = CreditCardSummaryDetailType.Details;

            var hsbcStringsData = new HsbcStringsData(_brandName);
            var lines = hsbcStringsData.GetDetailsData(table.AllText);
            var dateLines = hsbcStringsData.GetDetailsDataSections(table.Date);
            var arsLines = hsbcStringsData.GetDetailsDataSections(table.Ars);
            var usdLines = hsbcStringsData.GetDetailsDataSections(table.Usd);

            for (int i = 0; i < lines.Count; i++)
            {
                var lastIndex = 0;
                var date = "";
                var value = "";
                var aux = "";

                var detailDto = new CreditCardSummaryDetailDto();
                detailDto.Type = detailType;

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

                //Obtengo: Fecha
                if (aux.Length > 10)
                    date = aux.Substring(0, 10).Trim();

                if (dateLines.Count > 0 && dateLines[0] == date)
                {
                    detailDto.Date = DateTimeTools.ConvertToDateTime(date, "dd-MMM-yy");
                    dateLines.Remove(dateLines[0]);
                    aux = aux.Substring(10).Trim();
                }

                //Obtengo: Descripcion
                detailDto.Description = aux.Trim();

                detailDto.Installments = "";

                //Detecto el inicio de Taxes and Maintenance
                if (lines[i].Contains("FECHA DEBITOS AUTOMATICOS"))
                {
                    detailType = CreditCardSummaryDetailType.AutomaticDebits;
                    continue;
                }

                results.Add(detailDto);
            }

            return results;
        }
    }
}
