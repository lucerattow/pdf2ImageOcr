using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdf2Image.Import.Regions
{
    public class GeneralRegions
    {
        //300 dpi
        public static Rectangle GetEvaluationWarning() => new(80, 70, 1520, 75);
    }
}
