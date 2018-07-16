using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using WorkLogApp.DataAccess;
using ServerOps;

namespace WorkLogApp
{
    /// <summary>
    /// Interaction logic for NewEntry.xaml
    /// </summary>
    public partial class NewEntry : Window
    {
        private List<int> lstChanges = new List<int>();

        public NewEntry()
        {
            InitializeComponent();

            GetLastEntry();
            LoadEntries();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            WorkLog obj = new WorkLog();

            try
            {
                obj.WorkLogID = lblWorkLogID.Content.ConvertToInt32();
                obj.CreatedOn = lblCreatedOn.Content.ConvertToDateTime();
                obj.Notes = txtNotes.Text.Trim();

                CrudOperations op;

                if (obj.WorkLogID > 0)
                    op = CrudOperations.Update;
                else
                    op = CrudOperations.Insert;

                WorkLogDAO.Instance.Operation(op, obj);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally 
            {
                ReloadGrid();

                LoadForm(obj);
            }
        }

        private void LoadEntries()
        {
            ReloadGrid();
        }

        private void GetLastEntry()
        {
            LoadForm(WorkLogDAO.Instance.GetLastWorkLog());
        }

        private void LoadForm(WorkLog obj)
        {
            lblWorkLogID.Content = obj.WorkLogID.ToString();
            lblCreatedOn.Content = obj.CreatedOn.ToString();
            txtNotes.Text = obj.Notes;
        }

        private void btnNewEntry_Click(object sender, RoutedEventArgs e)
        {
            lblWorkLogID.Content = "0";
            lblCreatedOn.Content = DateTime.Now;
            txtNotes.Clear();
        }

        private void dgWorkLogEntries_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            try
            {
                WorkLog obj = (WorkLog)dgWorkLogEntries.SelectedItem;

                WorkLogDAO.Instance.Update(obj);

                LoadForm(obj);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReloadForm()
        {
            LoadForm(WorkLogDAO.Instance.GetWorkLog(lblWorkLogID.Content.ConvertToInt32()));
        }

        private void ReloadGrid()
        {
            SetDataSource(WorkLogDAO.Instance.GetWorkLogs());
        }

        private void SetDataSource(object dataSource)
        {
            System.Windows.Data.CollectionViewSource workLogViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("workLogViewSource")));
            
            workLogViewSource.Source = dataSource;
        }

        private void dgWorkLogEntries_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                int intWorkLogID = (e.Row.Item as WorkLog).WorkLogID;

                if (intWorkLogID == 0 || !lstChanges.Contains(intWorkLogID))
                    lstChanges.Add(intWorkLogID);
            }
            finally
            {
                btnSaveGrid.IsEnabled = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource workLogViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("workLogViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // workLogViewSource.Source = [generic data source]
        }

        private void btnSaveGrid_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lstChanges.Count == 0)
                    return;
                
                System.Windows.Data.CollectionViewSource workLogViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("workLogViewSource")));

                List<WorkLog> lst = (List<WorkLog>)workLogViewSource.Source;

                List<WorkLog> lstUpdate = lst.Where(x => lstChanges.Contains(x.WorkLogID)).ToList();

                WorkLogDAO.Instance.SaveChanges(lstUpdate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnSaveGrid.IsEnabled = false;

                lstChanges.Clear();
                
                ReloadForm();

                ReloadGrid();
            }
        }

        private void btnLoadRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WorkLog obj = (WorkLog)dgWorkLogEntries.SelectedItem;

                LoadForm(obj);

                tabs.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
