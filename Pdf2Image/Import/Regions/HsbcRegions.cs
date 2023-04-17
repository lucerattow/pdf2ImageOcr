using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdf2Image.Import.Regions
{
    public class HsbcRegions
    {
        public static Rectangle GetBankName() => new(350, 260, 350, 130);

        public static Rectangle GetBrandName() => new(1900, 650, 360, 35);

        public static Rectangle GetDateRegion() => new(280, 0, 220, 0);

        public static Rectangle GetArsRegion() => new(1920, 0, 280, 0);

        public static Rectangle GetUsdRegion() => new(2200, 0, 280, 0);

        //regiones para pintar imagenes

        public static Rectangle GetHeaderCleanP1() => new(220, 400, 2430, 450);

        public static Rectangle GetHeaderCleanP2() => new(2080, 170, 590, 330);

        public static Rectangle GetHeaderCleanAll() => new(150, 150, 2580, 730);

        public static Rectangle GetFooterClean() => new(190, 2930, 2510, 590);
    }
}
