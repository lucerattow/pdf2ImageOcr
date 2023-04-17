using Pdf2Image.Import.Supervielle;
using Pdf2Image.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Pdf2Image.Import.Utilities
{
    public static class ImageOcr
    {
        public static List<string> GetTextFromImages(List<Image> pages)
        {
            return GetTextFromImages(pages, new Rectangle());
        }

        public static List<string> GetTextFromImages(List<Image> pages, Rectangle region)
        {
            List<string> result = new List<string>();
            foreach (var page in pages)
            {
                result.AddRange(GetTextFromImage(page, region));
            }
            return result;
        }

        public static List<string> GetTextFromImage(Image page, Rectangle region)
        {
            List<string> result = new List<string>();

            //Configuro Tesseract
            using var engine = new TesseractEngine("./tessdata", "spa");
            engine.DefaultPageSegMode = PageSegMode.SingleBlock;

            var image = new Bitmap(page);

            if (!region.Equals(new Rectangle()))
            {
                //Primero compruebo los valores Y, Height, si estan en 0 deben tomar el valor total de la imagen
                if (region.Y == 0 && region.Height == 0)
                    region.Height = image.Height;

                //Obtengo la region en formato BMP
                image = image.Clone(region, image.PixelFormat);
                //image.Save($"E:\\.Mega\\Desarrollo\\Repositorios\\C#\\.Windows Forms\\MoneyAdministrator_testFiles\\" +
                //    $".Test\\OcrTest\\outputOriginal,pagNum={pagNum},x={region.X},y={region.Y},width={region.Width},height={region.Height}.bmp");
            }

            using var img = Pix.LoadFromMemory(ConvertToBytes(image));

            var imageOcr = engine.Process(img);
            result = imageOcr.GetText()
                .Replace(((char)8212).ToString(), "") //Elimino el caracter "-" que es largo y aparece en los simbolos de menos
                .Split("\n").ToList()
                .Where(x => !string.IsNullOrEmpty(x.Trim())).ToList();

            image.Dispose();
            return result;
        }

        public static byte[] ConvertToBytes(Image bmp)
        {
            // Crear un MemoryStream para almacenar los bytes de la imagen
            using MemoryStream ms = new MemoryStream();
            // Guardar la imagen en el MemoryStream en formato BMP
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            // Obtener el array de bytes de la imagen
            return ms.ToArray();
        }

    }
}
