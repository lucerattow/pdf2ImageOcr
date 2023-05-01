﻿using iText.Kernel.Geom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdf2Image.ImportItext.Importers.Regions
{
    public static class SpvRegions
    {
        public static Rectangle GetDateColumn(float heigth) => new(35, 0, 35, heigth);

        public static Rectangle GetAmountArsColumn(float heigth) => new(400, 0, 80, heigth);

        public static Rectangle GetAmountUsdColumn(float heigth) => new(480, 0, 80, heigth);
    }
}
