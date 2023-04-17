using Pdf2Image.Import.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pdf2Image.Import.HSBC
{
    public class HsbcCleanStrings
    {
        private string _brandName;

        public HsbcCleanStrings(string brandName)
        {
            _brandName = brandName;
        }

        public List<string> CleanAllText(List<string> lines)
        {
            var result = new List<string>();

            foreach (var line in lines)
            {
                if (line.Contains("TOTAL TITULAR CERATTO LUCAS EZEQUIEL"))
                    break;

                result.Add(line);
            }

            return result;
        }

        public List<string> CleanDate(List<string> dateLines)
        {
            var result = new List<string>();

            bool copy = false;
            foreach (var line in dateLines)
            {
                if (line.Contains("RESUMEN CC"))
                {
                    copy = true;
                    result.Add("::SUMMARY::");
                    continue;
                }

                if (line.Contains("DETALLE DE"))
                {
                    result.Add("::DETAILS::");
                    continue;
                }

                if (line.Contains("FECHA"))
                    continue;

                if (line.Contains("OPCIONES E"))
                    break;

                if (!copy)
                    continue;

                result.Add(line);
            }

            return CheckDate(result);
        }

        public List<string> CheckDate(List<string> dateLines)
        {
            var result = new List<string>();

            foreach (var line in dateLines)
            {
                if (line.Contains("::SUMMARY::"))
                {
                    result.Add(line);
                    continue;
                }
                if (line.Contains("::DETAILS::"))
                {
                    result.Add(line);
                    continue;
                }

                if (DateTimeTools.TestDate(line, "dd-MMM-yy"))
                    result.Add(line);
            }

            return result;
        }

        public List<string> CleanArs(List<string> arsLines)
        {
            var result = new List<string>();

            int count = 0;
            foreach (var line in arsLines)
            {
                if (line.Contains("PESOS"))
                {
                    if (count == 0)
                        result.Add("::SUMMARY::");
                    if (count == 1)
                        result.Add("::DETAILS::");
                    count++;
                    continue;
                }

                if (count < 1)
                    continue;

                if (Regex.IsMatch(line, @"[a-zA-Z]+"))
                    break;

                result.Add(line);
            }

            return CheckMoney(result);
        }

        public List<string> CleanUsd(List<string> usdLines)
        {
            List<string> result = new List<string>();

            int count = 0;
            foreach (var line in usdLines)
            {
                if (line.Contains("DOLARES"))
                {
                    if (count == 0)
                        result.Add("::SUMMARY::");
                    if (count == 1)
                        result.Add("::DETAILS::");

                    count++;
                    continue;
                }

                if (count == 0)
                    continue;

                if (line.Contains("5%."))
                    break;

                if (Regex.IsMatch(line, @"[a-zA-Z]+"))
                    break;

                result.Add(line);
            }

            if (_brandName == Brand.Mastercard)
                result.Add("0.00");

            return CheckMoney(result);
        }

        public List<string> CheckMoney(List<string> usdLines)
        {
            List<string> result = new List<string>();

            foreach (var line in usdLines)
            {
                if (line.Contains("::SUMMARY::"))
                {
                    result.Add(line);
                    continue;
                }
                if (line.Contains("::DETAILS::"))
                {
                    result.Add(line);
                    continue;
                }

                var value = StringTools.GetNumbersFromString(line);

                result.Add(value);
            }

            if (_brandName == Brand.Mastercard)
                result.Add("0.00");

            return result;
        }
    }
}
