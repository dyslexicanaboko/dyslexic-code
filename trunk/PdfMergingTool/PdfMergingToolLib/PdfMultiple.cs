using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;

namespace PdfMergingToolLib
{
    public class PdfMultiple : IDisposable
    {
        public List<PdfReader> Readers { get; private set; }

        public PdfMultiple(List<string> filePaths)
        {
            OpenAll(filePaths);
        }

        public int Count { get { return Readers.Count; } }

        private void OpenAll(List<string> filePaths)
        {
            PdfReader reader = null;

            foreach (string file in filePaths)
            {
                reader = new PdfReader(file);

                Readers.Add(reader);
            }

            //Readers = Readers.OrderByDescending(x => x.NumberOfPages).ToList();
        }
    
        public void Dispose()
        {
            foreach (PdfReader pr in Readers)
            {
                try
                {
                    if(pr != null)
                        pr.Close();
                }
                catch
                { 
                
                }
            }
        }
    }
}
