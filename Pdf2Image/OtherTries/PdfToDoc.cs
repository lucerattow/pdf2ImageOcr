using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdf2Image.OtherTries
{
    public class PdfToDoc
    {
        public static void SavePdfToDoc(string filename)
        {
            PdfDocument doc = new PdfDocument(filename);

            int index = filename.LastIndexOf("\\");
            string path = filename.Substring(0, index);
            string name = Path.GetFileNameWithoutExtension(filename);
            string finalnamedocx = $"{path}\\{name}.docx";
            string finalnamexlsx = $"{path}\\{name}.XLSX";
            doc.SaveToFile(finalnamedocx, FileFormat.DOCX);
            doc.SaveToFile(finalnamexlsx, FileFormat.XLSX);
        }
    }
}
