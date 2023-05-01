using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdf2Image.ImportItext.Importers.TextFilters
{
    public static class SpvTextFilter
    {
        public static List<string> FilterText(List<string> lines, string filter)
        {
            var result = new List<string>();

            bool copy = false;
            foreach (var line in lines)
            {
                if (line.Contains(filter))
                {
                    copy = true;
                    continue;
                }

                if (!copy)
                    continue;

                if (!line.Contains(filter) && line.StartsWith("::") && line.EndsWith("::"))
                    break;

                result.Add(line);
            }

            return result;
        }
    }
}
