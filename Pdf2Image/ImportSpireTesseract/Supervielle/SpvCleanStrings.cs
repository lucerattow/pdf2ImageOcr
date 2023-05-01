using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pdf2Image.Import.Supervielle
{
    public class SpvCleanStrings
    {
        private string _brandName;

        public SpvCleanStrings(string brandName)
        {
            _brandName = brandName;
        }

        public List<string> CleanAllText(List<string> lines)
        {
            var result = new List<string>();

            foreach (var line in lines)
            {
                if (line.Contains("PAGO MINIMO") || line.Contains("SALDO ACTUAL"))
                    break;

                result.Add(line);
            }

            return result;
        }
        
        public List<string> CleanDate(List<string> dateLines)
        {
            var result = new List<string>();

            var count = 0;
            foreach (var line in dateLines)
            {
                if (line.Contains("FECHA CI") || line.Contains("FECHA PR") || line.Contains("Movimiento"))
                    continue;

                if (line.Contains("FECHA"))
                {
                    if (count == 0)
                        result.Add("::SUMMARY::");
                    if (count == 1)
                        result.Add("::DETAILS::");
                    if (count == 2)
                        break;
                    count++;
                    continue;
                }

                //Si es menor o igual a 1, continua
                if (count <= 0) continue;

                result.Add(line);
            }

            return result;
        }

        public List<string> CleanArs(List<string> arsLines)
        {
            List<string> result = new List<string>();

            int count = 0;
            foreach (var line in arsLines)
            {
                if (line.Contains("IMPORTE EN PESOS"))
                {
                    if (count == 0)
                        result.Add("::SUMMARY::");
                    if (count == 1)
                        result.Add("::DETAILS::");
                    if (count == 2)
                        result.Add("::TAXESANDMAINTENANCE::");
                    if (count == 3)
                        break;
                    count++;
                    continue;
                }

                if (count == 0)
                    continue;

                if (Regex.IsMatch(line, @"[a-zA-Z]+"))
                    break;

                result.Add(line);
            }

            return result;
        }

        public List<string> CleanUsd(List<string> usdLines)
        {
            List<string> result = new List<string>();

            int count = 0;
            foreach (var line in usdLines)
            {
                if (line.Contains("IMPORTE EN DOLARES"))
                {
                    if (count == 0)
                        result.Add("::SUMMARY::");
                    if (count == 1)
                        result.Add("::DETAILS::");
                    if (count == 2)
                        result.Add("::TAXESANDMAINTENANCE::");
                    if (count == 3)
                        break;
                    count++;
                    continue;
                }

                if (count == 0)
                    continue;

                if (Regex.IsMatch(line, @"[a-zA-Z]+"))
                    break;

                result.Add(line);
            }

            if (_brandName == Brand.Mastercard)
                result.Add("0.00");

            return result;
        }

    }
}
