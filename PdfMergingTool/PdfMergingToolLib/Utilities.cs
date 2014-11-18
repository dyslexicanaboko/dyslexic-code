using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PdfMergingToolLib
{
    public static class Utilities
    {
        //99% of credit goes to this person: http://stackoverflow.com/a/20491695
        public static bool Merge(List<string> fullFilePaths, string saveAs)
        {
            try
            {
                using (FileStream stream = new FileStream(saveAs, FileMode.Create))
                using (Document doc = new Document())
                using (PdfCopy pdf = new PdfCopy(doc, stream))
                {
                    doc.Open();

                    PdfReader reader = null;
                    PdfImportedPage page = null;

                    fullFilePaths.ForEach(file =>
                    {
                        using (reader = new PdfReader(file))
                        {
                            for (int i = 0; i < reader.NumberOfPages; i++)
                            {
                                page = pdf.GetImportedPage(reader, i + 1);
                                pdf.AddPage(page);
                            }

                            pdf.FreeReader(reader);
                        }
                    });
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }

            return true;
        }

        public static bool Collate(string frontSide, string backSide, string saveAs, bool backSideIsInReverseOrder = true)
        {
            try
            {
                bool backSideIsAscending = !backSideIsInReverseOrder;

                using (FileStream stream = new FileStream(saveAs, FileMode.Create))
                using (Document doc = new Document())
                using (PdfCopy pdf = new PdfCopy(doc, stream))
                {
                    doc.Open();

                    int b = 0;

                    using (PdfReader front = new PdfReader(frontSide))
                    {
                        using (PdfReader back = new PdfReader(backSide))
                        {
                            for (int p = 1; p <= front.NumberOfPages; p++)
                            {
                                pdf.AddPage(pdf.GetImportedPage(front, p));

                                pdf.FreeReader(front);

                                if (p <= back.NumberOfPages)
                                {
                                    if (backSideIsAscending)
                                        b = p;
                                    else
                                        b = back.NumberOfPages - p + 1;

                                    pdf.AddPage(pdf.GetImportedPage(back, b));

                                    pdf.FreeReader(back);
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }

            return true;
        }

        //private static void AddPage(PdfCopy pdf, PdfReader reader)
        //{
        //    pdf.AddPage(pdf.GetImportedPage(reader, p));

        //    pdf.FreeReader(reader);
        //}
    }
}
