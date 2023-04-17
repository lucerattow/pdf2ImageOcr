using iText.Kernel.Geom;
using Pdf2Image.Import.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdf2Image.Import.HSBC
{
    public class HsbcStringsData
    {
        private string _brandName;

        public HsbcStringsData(string brandName)
        {
            _brandName = brandName;
        }

        public List<string> GetGeneralData(List<string> lines)
        {
            var results = new List<string>();

            for (int i = 0; i < 10; i++)
            {
                if (lines[i].Contains("Estado de cuenta al"))
                    results.Add(lines[i]);

                if (lines[i].Contains("Vencimiento actual"))
                    results.Add(lines[i]);

                if (lines[i].Contains("Próximo Cierre"))
                    results.Add(lines[i]);

                if (lines[i].Contains("Próximo Vencimiento"))
                    results.Add(lines[i]);
            }

            return results;
        }

        public List<string> GetSummaryMaintenanceData(List<string> lines)
        {
            var results = new List<string>();

            //Obtengo las lineas
            bool copy = false;
            foreach (var line in lines)
            {
                if (line.Contains("RESUMEN CONSOLIDADO"))
                {
                    copy = true;
                    continue;
                }

                if (line.Contains("SALDO ACTUAL"))
                    break;

                if (!copy)
                    continue;

                results.Add(line);
            }

            return results.Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        public List<string> GetSummaryMaintenanceDataSections(List<string> lines)
        {
            var results = new List<string>();

            for (int i = 0; i < lines.Count; i++)
            {
                lines[i] = lines[i].Replace(((char)8212).ToString(), "");
            }

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

            //Obtengo las lineas
            bool copy = false;
            foreach (var line in lines)
            {
                if (line.Contains("FECHA COMPRAS DEL MES"))
                {
                    copy = true;
                    continue;
                }

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
            bool copy = false;
            foreach (var line in lines)
            {
                if (line.Contains("::DETAILS::"))
                {
                    copy = true;
                    continue;
                }

                if (!copy)
                    continue;

                results.Add(line);
            }

            return results.Where(x => !string.IsNullOrEmpty(x)).ToList();
        }
    }
}
