﻿using MoneyAdministrator.Common.DTOs;
using MoneyAdministrator.DTOs.Enums;
using Pdf2Image.ImportItext.Importers.TextFilters;
using Pdf2Image.ImportText.DTOs;
using Pdf2Image.ImportText.Utilities;
using Pdf2Image.ImportText.Utilities.TypeTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Pdf2Image.ImportItext.Importers.ProcessTexts
{
    public static class SpvProcessText
    {
        public static CreditCardSummaryDto GetSummaryData(TransactionsTableDto table)
        {
            var summary = GetSummaryVariablesData(table.AllText);
            summary.AddDetailDto(GetAllData(table));

            return summary;
        }

        private static CreditCardSummaryDto GetSummaryVariablesData(List<string> lines)
        {
            var summary = new CreditCardSummaryDto();

            for (int i = 0; i < 5; i++)
            {
                var key = lines[i].Split(":")[0];
                var value = lines[i].Split(":")[1];

                switch (key)
                {
                    case "DATE":
                        //Fecha de resumen
                        summary.Date = DateTimeTools.Convert(value, "yyyy-MM-dd");

                        //Periodo
                        int month = summary.Date.Day >= ModuleConfigs.DayStartPeriod ? summary.Date.Month + 1 : summary.Date.Month;
                        int year = summary.Date.Year;
                        if (month == 13)
                        {
                            month = 1;
                            year++;
                        }
                        summary.Period = new DateTime(year, month, 1);
                        continue;
                    case "DATE_EXP":
                        //Fecha de vencimiento
                        summary.Expiration = DateTimeTools.Convert(value, "yyyy-MM-dd");
                        continue;
                    case "DATE_NEXT":
                        //Proximo cierre
                        summary.NextDate = DateTimeTools.Convert(value, "yyyy-MM-dd");
                        continue;
                    case "DATE_NEXT_EXP":
                        //Proximo vencimiento
                        summary.NextExpiration = DateTimeTools.Convert(value, "yyyy-MM-dd");
                        continue;
                    case "MIN_PAY":
                        //Pago minimo
                        summary.MinimumPayment = DecimalTools.Convert(value);
                        continue;
                }
            }

            return summary;
        }

        private static List<CreditCardSummaryDetailDto> GetAllData(TransactionsTableDto table)
        {
            var results = new List<CreditCardSummaryDetailDto>();

            //Variables necesarias para procesar el texto
            Regex dateRegex = new Regex(@"^(\d{4}-\d{2}-\d{2})?");
            Regex installmentRegex = new Regex(@"^\d{2}/\d{2}$");
            var detailType = CreditCardSummaryDetailType.Summary;

            var DateList = table.Date;
            var AmountArsList = table.AmountArs;
            var AmountUsdList = table.AmountUsd;

            //Analizo cada linea
            bool process = false;
            foreach (var line in table.AllText)
            {
                //Compruebo si la linea es un separador
                switch (line)
                {
                    case "::SUMMARY::":
                        detailType = CreditCardSummaryDetailType.Summary;
                        process = true;

                        DateList = SpvTextFilter.FilterText(table.Date, line);
                        AmountArsList = SpvTextFilter.FilterText(table.AmountArs, line);
                        AmountUsdList = SpvTextFilter.FilterText(table.AmountUsd, line);
                        continue;
                    case "::TAXES::":
                        detailType = CreditCardSummaryDetailType.TaxesAndMaintenance;

                        DateList = SpvTextFilter.FilterText(table.Date, line);
                        AmountArsList = SpvTextFilter.FilterText(table.AmountArs, line);
                        AmountUsdList = SpvTextFilter.FilterText(table.AmountUsd, line);
                        continue;
                    case "::DETAILS::":
                        detailType = CreditCardSummaryDetailType.Details;

                        DateList = SpvTextFilter.FilterText(table.Date, line);
                        AmountArsList = SpvTextFilter.FilterText(table.AmountArs, line);
                        AmountUsdList = SpvTextFilter.FilterText(table.AmountUsd, line);
                        continue;
                }

                if (!process)
                    continue;

                //Compruebo si el ultimo valor corresponde al total en ars y usd
                if (detailType == CreditCardSummaryDetailType.TaxesAndMaintenance)
                {
                    if (line.StartsWith(AmountArsList.Last()))
                        continue;
                }

                //Variable para almacenar el texto que va sobrando
                string restOfText = "";

                string date = "";
                string description = "";
                string installments = "";
                string amountArs = "";
                string amountUsd = "";

                //Obtengo: valor USD
                var AmountIndex = line.LastIndexOf(" ");
                var AmountValue = line.Substring(AmountIndex).Trim();
                restOfText = line.Substring(0, AmountIndex);

                //Seteo: valor USD
                if (AmountUsdList.Count > 0 && AmountUsdList[0] == AmountValue)
                {
                    amountUsd = AmountValue;
                    AmountUsdList.RemoveAt(0);

                    //Obtengo el valor de ARS
                    AmountIndex = restOfText.LastIndexOf(" ");
                    AmountValue = restOfText.Substring(AmountIndex + 1).Trim();
                }

                //Obtengo: valor ARS
                if (AmountArsList.Count > 0 && AmountArsList[0] == AmountValue)
                {
                    amountArs = AmountValue;
                    AmountArsList.RemoveAt(0);
                    restOfText = line.Substring(0, AmountIndex);
                }

                //Obtengo: Fecha
                Match dateMatch = dateRegex.Match(restOfText);
                if (DateList.Count > 0 && DateList[0] == dateMatch.Value)
                {
                    date = dateMatch.Value;
                    DateList.RemoveAt(0);
                    restOfText = restOfText.Substring(date.Length, restOfText.Length - date.Length);
                }

                //Obtengo: Cuotas
                Match installMatch = installmentRegex.Match(restOfText.Split(" ").Last());
                if (installMatch.Success && detailType == CreditCardSummaryDetailType.Details)
                {
                    installments = installMatch.Value;
                    restOfText = restOfText.Replace(installments, "");
                }

                //Obtengo: Descripcion
                description = restOfText.Trim();

                //Se installment type
                CreditCardSummaryDetailType type = detailType;
                if (detailType == CreditCardSummaryDetailType.Details && !string.IsNullOrEmpty(installments))
                    type = CreditCardSummaryDetailType.Installments;

                var summary = new CreditCardDetailDto()
                {
                    Type = type,
                    Date = date,
                    DateFormat = "yyyy-MM-dd",
                    Description = description,
                    Installments = installments,
                    AmountArs = amountArs,
                    AmountUsd = amountUsd
                };

                results.Add(ParseDto.GetCreditCardSummaryDetailDto(summary));
            }

            return results;
        }
    }
}
