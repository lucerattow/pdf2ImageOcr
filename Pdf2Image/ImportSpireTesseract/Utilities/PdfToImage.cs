using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spire.Pdf.Graphics;
using Spire.Pdf;
using Pdf2Image.Import.Regions;
using System.Drawing;

namespace Pdf2Image.Import.Utilities
{
    public static class PdfToImage
    {
        public static List<Image> Convert(string pdfFilePath, string bankName = "")
        {
            List<Image> images = new List<Image>();

            // Cargar el archivo PDF
            var pdfDoc = new PdfDocument(pdfFilePath);
            AddWhiteBorderToPdf(ref pdfDoc);

            for (int i = pdfDoc.Pages.Count - 1; i >= 0; i--)
            {
                Image image = pdfDoc.SaveAsImage(i, 300, 300);
                //Image image = pdfDoc.SaveAsImage(i, 600, 600);
                images.Add(image);
            }

            //Modifico el pdf quitando el warning de la libreria
            CleanImagesWarning(images);

            //Modifico el pdf quitando areas especificas segun el banco
            if (bankName == Compatibility.HSBC.Name)
                CleanImagesHsbc(images);

            //BORRAR
            for (int i = 0; i < images.Count; i++)
            {
                images[i].Save($"E:\\.Mega\\Desarrollo\\Repositorios\\C#\\.Windows Forms\\MoneyAdministrator_testFiles\\.Test\\OcrTest\\outputOriginal{i}.bmp");
            }
            pdfDoc.Close();
            return images;
        }

        public static void CleanImagesWarning(List<Image> images)
        {
            for (int i = 0; i < images.Count; i++)
            {
                using var graphics = Graphics.FromImage(images[i]);
                using var brush = new SolidBrush(Color.White);

                graphics.FillRectangle(brush, GeneralRegions.GetEvaluationWarning());
            }
        }

        public static void CleanImagesHsbc(List<Image> images)
        {
            for (int i = 0; i < images.Count; i++)
            {
                if (i == 0)
                {
                    using var graphics = Graphics.FromImage(images[i]);
                    using var brush = new SolidBrush(Color.White);

                    graphics.FillRectangle(brush, HsbcRegions.GetHeaderCleanP1());
                    graphics.FillRectangle(brush, HsbcRegions.GetHeaderCleanP2());
                    graphics.FillRectangle(brush, HsbcRegions.GetFooterClean());

                    continue;
                }

                if (i % 2 == 0)
                {
                    using var graphics = Graphics.FromImage(images[i]);
                    using var brush = new SolidBrush(Color.White);

                    graphics.FillRectangle(brush, HsbcRegions.GetHeaderCleanAll());
                    continue;
                }
            }
        }

        public static void AddWhiteBorderToPdf(ref PdfDocument pdfDoc)
        {
            // Obtener la altura original de la página
            PdfPageBase originalPage = pdfDoc.Pages[0];
            float originalHeight = originalPage.Size.Height;

            // Añadir 50 píxeles de espacio en blanco en la parte superior de cada página
            foreach (PdfPageBase page in pdfDoc.Pages)
            {
                // Crear una nueva página en blanco con el mismo tamaño que la página original
                PdfPageBase newPage = pdfDoc.Pages.Insert(0, new SizeF(page.Size.Width + 100, page.Size.Height + 100));

                // Dibujar la página original desplazada hacia abajo
                PdfTemplate template = new PdfTemplate(page.Size);
                template.Graphics.TranslateTransform(0, 0);
                template.Graphics.DrawTemplate(page.CreateTemplate(), PointF.Empty);
                newPage.Canvas.DrawTemplate(template, PointF.Empty);

                // Eliminar la página original y actualizar la altura del documento
                pdfDoc.Pages.Remove(page);

                // Actualizar la altura de la página en caso de que la página original ya tenga elementos en la parte superior
                if (originalHeight > 0)
                {
                    newPage.Canvas.TranslateTransform(0, originalHeight);
                }
            }
        }
    }
}
