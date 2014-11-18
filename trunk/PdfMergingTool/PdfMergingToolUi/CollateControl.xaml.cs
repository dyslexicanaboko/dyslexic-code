using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PdfMergingToolUi
{
    /// <summary>
    /// Interaction logic for CollateControl.xaml
    /// </summary>
    public partial class CollateControl : UserControl
    {
        private string SaveAsFullFilePath { get; set; }

        public CollateControl()
        {
            InitializeComponent();

            SetSaveAs(Utilities.GetRandomFileName());
        }

        private void SetSaveAs(string fullFilePath)
        {
            SaveAsFullFilePath = fullFilePath;

            txtSaveAs.Text = fullFilePath;
        }

        private void txtSideSet_Drop(object sender, DragEventArgs e)
        {
            try
            {
                if (!e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
                    return;

                string[] arr = e.Data.GetData(System.Windows.DataFormats.FileDrop) as string[];

                SetSideText((TextBox)sender, arr[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSideSet_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                using (System.Windows.Forms.OpenFileDialog obj = Utilities.GetOpenFileDialogForPdf())
                {
                    System.Windows.Forms.DialogResult result = obj.ShowDialog();

                    if (result != System.Windows.Forms.DialogResult.OK)
                        return;

                    SetSideText((TextBox)sender, obj.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);            
            }
        }

        private void SetSideText(TextBox target, string fullFilePath)
        {
            target.Text = fullFilePath;
        }

        private void btnCollate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!IsValid(txtFrontSet))
                    return;

                if (!IsValid(txtBackSet))
                    return;

                if (string.IsNullOrWhiteSpace(SaveAsFullFilePath))
                    SetSaveAs(Utilities.GetRandomFileName());

                //If files should not be overwritten and the file exists
                if (System.IO.File.Exists(SaveAsFullFilePath) && cbDontOverwriteFiles.IsChecked.GetValueOrDefault())
                {
                    string file = System.IO.Path.GetFileName(SaveAsFullFilePath);

                    System.Windows.MessageBox.Show("A file with the name " + file + " exists already.");

                    return;
                }

                if (PdfMergingToolLib.Utilities.Collate(txtFrontSet.Text, txtBackSet.Text, txtSaveAs.Text, cbBackSideReversed.IsChecked.GetValueOrDefault()))
                {
                    if (cbOpenSaveAsFolder.IsChecked.GetValueOrDefault())
                    {
                        string path = System.IO.Path.GetDirectoryName(SaveAsFullFilePath);

                        ProcessStartInfo proc = new ProcessStartInfo();
                        proc.FileName = "explorer.exe";
                        proc.Arguments = path;

                        Process.Start(proc);
                    }
                    else
                        System.Windows.MessageBox.Show("File created successfully and saved to: " + SaveAsFullFilePath);
                }
                else
                    System.Windows.MessageBox.Show("An unknown error occurred during the merge operation.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool IsValid(TextBox target)
        {
            bool valid = true;

            if (string.IsNullOrWhiteSpace(txtFrontSet.Text))
            {
                MessageBox.Show("Target cannot be blank. Please select a valid file.");

                valid = false;
            }

            if (!System.IO.File.Exists(txtFrontSet.Text))
            {
                MessageBox.Show("Target file must exist. Please select a valid file.");

                valid = false;
            }

            return valid;
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (System.Windows.Forms.SaveFileDialog obj = Utilities.GetSaveFileDialogForPdf())
                {
                    System.Windows.Forms.DialogResult result = obj.ShowDialog();

                    if (result != System.Windows.Forms.DialogResult.OK)
                        return;

                    SetSaveAs(obj.FileName);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void txtSideSet_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;
            e.Handled = true;
        }
    }
}
