using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace Pdf2Image.OtherTries
{
    public class TesseractOcrImage
    {
        public static string GetTextFromImageRegion(string imagePath, Rectangle region)
        {
            // Crear una instancia del motor OCR de Tesseract
            using var engine = new TesseractEngine("./tessdata", "spa");

            // Establecer la imagen de entrada
            using var image = new Bitmap(imagePath);
            var croppedImg = image.Clone(region, image.PixelFormat);
            croppedImg.Save(imagePath + ".bmp");

            using var img = Pix.LoadFromFile(imagePath + ".bmp");

            var page = engine.Process(img);
            var text = page.GetText();

            return text.Replace("\n", "\r\n"); ;
        }
    }
}
