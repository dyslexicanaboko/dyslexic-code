using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for MergeControl.xaml
    /// </summary>
    public partial class MergeControl : UserControl
    {
        public MergeControl()
        {
            InitializeComponent();

            FilesToMerge = new ObservableCollection<TargetFile>();

            SetSaveAs(Utilities.GetRandomFileName());
        }

        //http://www.c-sharpcorner.com/UploadFile/mahesh/datagrid-in-wpf/
        //http://www.dotnetperls.com/datagrid
        //http://stackoverflow.com/questions/19905450/how-to-refresh-datagrid-in-c-sharp-wpf
        private ObservableCollection<TargetFile> FilesToMerge { get; set; }
        private string SaveAsFullFilePath { get; set; }

        private void SetSaveAs(string fullFilePath)
        {
            SaveAsFullFilePath = fullFilePath;

            txtSaveAs.Text = fullFilePath;
        }

        private void btnAddFiles_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (System.Windows.Forms.OpenFileDialog obj = Utilities.GetOpenFileDialogForPdf(true))
                {
                    System.Windows.Forms.DialogResult result = obj.ShowDialog();

                    if (result != System.Windows.Forms.DialogResult.OK)
                        return;

                    AddToList(obj.FileNames);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void AddToList(string[] files)
        {
            foreach (string file in files)
            {
                FilesToMerge.Add(new TargetFile() 
                {
                    FullFilePath = file,
                    FileName = System.IO.Path.GetFileName(file)
                });
            }

            //dgFiles.DataContext = FilesToMerge;
            dgFiles.ItemsSource = FilesToMerge;
        }

        private void dgFiles_Loaded(object sender, RoutedEventArgs e)
        {
            dgFiles.DataContext = FilesToMerge;
            //dgFiles.ItemsSource = FilesToMerge;
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

        private void btnMerge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SaveAsFullFilePath))
                    SetSaveAs(Utilities.GetRandomFileName());

                //If files should not be overwritten and the file exists
                if (System.IO.File.Exists(SaveAsFullFilePath) && cbDontOverwriteFiles.IsChecked.GetValueOrDefault())
                {
                    string file = System.IO.Path.GetFileName(SaveAsFullFilePath);

                    System.Windows.MessageBox.Show("A file with the name " + file + " exists already.");

                    return;
                }

                //If there are not enough files selected to continue
                if (FilesToMerge.Count < 2)
                {
                    System.Windows.MessageBox.Show("Not enough files have been selected. Select at least 2 files to continue.");

                    return;
                }

                List<string> lst = FilesToMerge.Select(x => x.FullFilePath).ToList();


                if (PdfMergingToolLib.Utilities.Merge(lst, SaveAsFullFilePath))
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
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void dgFiles_Drop(object sender, System.Windows.DragEventArgs e)
        {
            try
            {
                if (!e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
                    return;

                string[] arr = e.Data.GetData(System.Windows.DataFormats.FileDrop) as string[];

                AddToList(arr);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
    }
}
