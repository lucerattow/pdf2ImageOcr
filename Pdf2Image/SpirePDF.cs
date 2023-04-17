using Spire.Pdf;
using Spire.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Pdf2Image
{
    public class SpirePDF
    {
        public static void ConvertPdfToImage(string input, string output)
        {
            // Cargar el archivo PDF
            var pdfDoc = new PdfDocument(input);
            AddHeaderToPdf(ref pdfDoc);
            for (int i = 0; i < pdfDoc.Pages.Count; i++)
            {
                using (Image image = pdfDoc.SaveAsImage(i, 150, 150))
                {
                    image.Save(output + "pdfPage" + i + ".png", ImageFormat.Png);
                }
            }
            pdfDoc.Close();
        }

        public static void AddHeaderToPdf(ref PdfDocument pdfDoc)
        {
            // Obtener la altura original de la página
            PdfPageBase originalPage = pdfDoc.Pages[0];
            float originalHeight = originalPage.Size.Height;

            // Añadir 50 píxeles de espacio en blanco en la parte superior de cada página
            foreach (PdfPageBase page in pdfDoc.Pages)
            {
                // Crear una nueva página en blanco con el mismo tamaño que la página original
                PdfPageBase newPage = pdfDoc.Pages.Insert(0, new SizeF(page.Size.Width + 50, page.Size.Height + 50));

                // Dibujar la página original desplazada hacia abajo
                PdfTemplate template = new PdfTemplate(page.Size);
                template.Graphics.TranslateTransform(0, 0);
                template.Graphics.DrawTemplate(page.CreateTemplate(), PointF.Empty);
                newPage.Canvas.DrawTemplate(template, PointF.Empty);

                // Eliminar la página original y actualizar la altura del documento
                pdfDoc.Pages.Remove(page);
                pdfDoc.PageSettings.Height += 50;

                // Actualizar la altura de la página en caso de que la página original ya tenga elementos en la parte superior
                if (originalHeight > 0)
                {
                    newPage.Canvas.TranslateTransform(0, originalHeight);
                }
            }
        }
    }
}
