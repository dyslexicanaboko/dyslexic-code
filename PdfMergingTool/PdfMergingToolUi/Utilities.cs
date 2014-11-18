using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfMergingToolUi
{
    public static class Utilities
    {
        public static string GetRandomFileName()
        {
            return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, System.IO.Path.GetRandomFileName() + ".pdf");
        }

        public static System.Windows.Forms.OpenFileDialog GetOpenFileDialogForPdf(bool multiSelect = false)
        {
            return new System.Windows.Forms.OpenFileDialog()
                {
                    Filter = "PDF Files (*.pdf)|*.pdf",
                    Multiselect = multiSelect,
                    DefaultExt = "pdf"
                };
        }

        public static System.Windows.Forms.SaveFileDialog GetSaveFileDialogForPdf()
        {
            return new System.Windows.Forms.SaveFileDialog()
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                DefaultExt = "pdf"
            };
        }
    }
}
