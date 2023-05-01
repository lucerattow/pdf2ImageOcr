using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdf2Image.Import.Supervielle
{
    public class SpvStringsData
    {
        private string _brandName;

        public SpvStringsData(string brandName)
        { 
            _brandName = brandName;
        }

        public List<string> GetGeneralData(List<string> lines)
        {
            var results = new List<string>();

            for (int i = 0; i < lines.Count; i++)
            {
                if (i == 8 || i == 10 || i == 14)
                    results.Add(lines[i]);
            }

            return results;
        }

        public List<string> GetSummaryData(List<string> lines)
        {
            var results = new List<string>();

            //Obtengo las lineas
            bool copy = false;
            foreach (var line in lines)
            {
                if (line.Contains("SALDO ANTERIOR"))
                    copy = true;

                if (line.Contains("Movimientos"))
                    break;

                if (!copy)
                    continue;

                results.Add(line);
            }

            return results.Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        public List<string> GetSummaryDataSections(List<string> lines)
        {
            var results = new List<string>();

            //Obtengo las lineas
            for (int index = 0; index < lines.Count; index++)
            {
                if (lines[index].Contains("::SUMMARY::"))
                    continue;

                if (lines[index].Contains("::DETAILS::"))
                    break;

                results.Add(lines[index]);
            }

            return results.Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        public List<string> GetDetailsData(List<string> lines)
        {
            var results = new List<string>();

            string topFilter = "";
            string buttomFilter = "";

            //Segun la marca de la tarjeta seteo los filtros
            if (_brandName == Brand.Visa)
            {
                topFilter = "FECHA COMPROBANTE DETALLE DE TRANSACCION IMPORTE EN PESOS IMPORTE EN DOLARES";
                buttomFilter = "Total Consumos de LUCAS EZEQU CERATTO";
            }
            else if (_brandName == Brand.Mastercard)
            {
                topFilter = "FECHA COMPROBANTE COD. OPERACION DETALLE DE TRANSACCION IMPORTE EN PESOS IMPORTE EN DOLARES";
                buttomFilter = "TOTAL TITULAR CERATTO LUCAS EZEQUIEL";
            }

            //Obtengo las lineas
            bool copy = false;
            foreach (var line in lines)
            {
                if (line.Contains(topFilter))
                {
                    copy = true;
                    continue;
                }

                if (line.Contains(buttomFilter))
                    break;

                if (!copy)
                    continue;

                results.Add(line);
            }

            return results.Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        public List<string> GetDetailsDataSections(List<string> lines)
        {
            var results = new List<string>();

            //Obtengo las lineas
            var copy = false;
            for (int index = 0; index < lines.Count; index++)
            {
                if (lines[index].Contains("::DETAILS::"))
                {
                    copy = true;
                    continue;
                }

                if (lines[index].Contains("::TAXESANDMAINTENANCE::"))
                    break;

                if (!copy)
                    continue;

                results.Add(lines[index]);
            }

            //Elimino el ultimo valor (subtotal)
            var lastItem = results.LastOrDefault();
            if (lastItem != null && results.Count > 0)
            {
                results.Remove(lastItem);
            }

            return results.Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        public List<string> GetTaxesAndMaintenanceData(List<string> lines)
        {
            var results = new List<string>();

            //Obtengo las lineas
            bool copy = false;
            int count = 0;
            foreach (var line in lines)
            {
                if (line.Contains("FECHA DETALLE DE TRANSACCION IMPORTE EN PESOS IMPORTE EN DOLARES"))
                {
                    count++;
                    copy = true;
                    continue;
                }

                if (count <= 1)
                    continue;

                if (line.Contains("SALDO ACTUAL"))
                    break;

                if (!copy)
                    continue;

                results.Add(line);
            }

            return results.Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        public List<string> GetTaxesAndMaintenanceDataSections(List<string> lines)
        {
            var results = new List<string>();

            //Obtengo las lineas
            var copy = false;
            for (int index = 0; index < lines.Count; index++)
            {
                if (lines[index].Contains("::TAXESANDMAINTENANCE::"))
                {
                    copy = true;
                    continue;
                }

                if (!copy)
                    continue;

                results.Add(lines[index]);
            }

            //Elimino los ultimos 2 valor (total y minimo)
            results.RemoveRange(results.Count - 1, 1);

            return results.Where(x => !string.IsNullOrEmpty(x)).ToList();
        }
    }
}
