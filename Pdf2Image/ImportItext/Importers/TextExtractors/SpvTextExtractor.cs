using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Filter;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using Pdf2Image.ImportItext.Importers.Regions;
using System.Collections.Generic;

namespace Pdf2Image.ImportItext.Importers.TextExtractors
{
    public static class SpvTextExtractor
    {
        public static List<string> GetTextFromPDF(string filename)
        {
            var pagesText = new List<string>();

            using (var pdfDocument = new PdfDocument(new PdfReader(filename)))
            {
                for (int i = 1; i <= pdfDocument.GetNumberOfPages(); ++i)
                {
                    PdfPage page = pdfDocument.GetPage(i);
                    pagesText.Add(PdfTextExtractor.GetTextFromPage(page));
                }
            }

            return GetPagesInLines(pagesText);
        }

        public static List<string> GetDateTextFromPDF(string filename)
        {
            var pagesText = new List<string>();

            using (var pdfDocument = new PdfDocument(new PdfReader(filename)))
            {
                for (int i = 1; i <= pdfDocument.GetNumberOfPages(); ++i)
                {
                    PdfPage page = pdfDocument.GetPage(i);
                    Rectangle region = SpvRegions.GetDateColumn(page.GetPageSize().GetHeight());

                    var filter = new IEventFilter[1];
                    filter[0] = new TextRegionEventFilter(region);
                    var filters = new FilteredTextEventListener(new LocationTextExtractionStrategy(), filter);

                    pagesText.Add(PdfTextExtractor.GetTextFromPage(page, filters));
                }
            }

            return GetPagesInLines(pagesText);
        }

        public static List<string> GetAmountArsTextFromPDF(string filename)
        {
            var pagesText = new List<string>();

            using (var pdfDocument = new PdfDocument(new PdfReader(filename)))
            {
                for (int i = 1; i <= pdfDocument.GetNumberOfPages(); ++i)
                {
                    PdfPage page = pdfDocument.GetPage(i);
                    Rectangle region = SpvRegions.GetAmountArsColumn(page.GetPageSize().GetHeight());

                    var filter = new IEventFilter[1];
                    filter[0] = new TextRegionEventFilter(region);
                    var filters = new FilteredTextEventListener(new LocationTextExtractionStrategy(), filter);

                    pagesText.Add(PdfTextExtractor.GetTextFromPage(page, filters));
                }
            }

            return GetPagesInLines(pagesText);
        }

        public static List<string> GetAmountUsdTextFromPDF(string filename)
        {
            var pagesText = new List<string>();

            using (var pdfDocument = new PdfDocument(new PdfReader(filename)))
            {
                for (int i = 1; i <= pdfDocument.GetNumberOfPages(); ++i)
                {
                    PdfPage page = pdfDocument.GetPage(i);
                    Rectangle region = SpvRegions.GetAmountUsdColumn(page.GetPageSize().GetHeight());

                    var filter = new IEventFilter[1];
                    filter[0] = new TextRegionEventFilter(region);
                    var filters = new FilteredTextEventListener(new LocationTextExtractionStrategy(), filter);

                    pagesText.Add(PdfTextExtractor.GetTextFromPage(page, filters));
                }
            }

            return GetPagesInLines(pagesText);
        }

        public static List<string> GetRegionTextFromPDF(string filename)
        {
            var pagesText = new List<string>();

            using (var pdfDocument = new PdfDocument(new PdfReader(filename)))
            {
                for (int i = 1; i <= pdfDocument.GetNumberOfPages(); ++i)
                {
                    PdfPage page = pdfDocument.GetPage(i);
                    Rectangle region = SpvRegions.GetAmountUsdColumn(page.GetPageSize().GetHeight());

                    var filter = new IEventFilter[1];
                    filter[0] = new TextRegionEventFilter(region);
                    var filters = new FilteredTextEventListener(new LocationTextExtractionStrategy(), filter);

                    pagesText.Add(PdfTextExtractor.GetTextFromPage(page, filters));
                }
            }

            return GetPagesInLines(pagesText);
        }

        private static List<string> GetPagesInLines(List<string> pages)
        {
            var result = new List<string>();

            foreach (var page in pages)
                result.AddRange(page.Split("\n"));

            return result;
        }
    }
}
