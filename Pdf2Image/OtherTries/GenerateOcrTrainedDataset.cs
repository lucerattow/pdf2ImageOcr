using Pdf2Image.Import.Regions;
using Pdf2Image.Import.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace Pdf2Image.OtherTries
{
    public static class GenerateOcrTrainedDataset
    {
        public static void Run(string filename)
        {
            var pages = PdfToImage.Convert(filename);

            //Dates:
            GetTextFromImages(pages, filename, "Date", HsbcRegions.GetDateRegion());
            GetTextFromImages(pages, filename, "zArs", HsbcRegions.GetArsRegion());
            GetTextFromImages(pages, filename, "zUsd", HsbcRegions.GetUsdRegion());
        }

        public static void GetTextFromImages(List<Image> pages, string filename, string folder, Rectangle region)
        {
            for (int i = 0; i < pages.Count; i++)
            {
                GetTextFromImage(pages[i], filename, folder, region, i);
            }
        }

        public static void GetTextFromImage(Image page, string filename, string folder, Rectangle region, int num)
        {
            var image = new Bitmap(page);

            if (!region.Equals(new Rectangle()))
            {
                //Primero compruebo los valores Y, Height, si estan en 0 deben tomar el valor total de la imagen
                if (region.Y == 0 && region.Height == 0)
                    region.Height = image.Height;

                //Obtengo la region en formato BMP
                image = image.Clone(region, image.PixelFormat);

                string sufix = "";
                if (folder == "Date")
                    sufix = "date";
                if (folder == "zArs")
                    sufix = "ars";
                if (folder == "zUsd")
                    sufix = "usd";

                image.Save($"E:\\.Mega\\Desarrollo\\Repositorios\\Tesseract\\spv_visa\\{folder}\\" +
                    $"{Path.GetFileNameWithoutExtension(filename)}_p{num}_{sufix}.bmp");
            }

            image.Dispose();
        }
    }
}
