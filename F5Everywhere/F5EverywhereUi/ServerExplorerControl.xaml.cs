using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using F5EverywhereLib.Domain;
using F5EverywhereLib.Repository;
using F5EverywhereLib.Service;
using System.Linq;

namespace F5EverywhereUi
{
    /// <summary>
    /// Interaction logic for ServerExplorerControl.xaml
    /// </summary>
    public partial class ServerExplorerControl : UserControl
    {
        private ObservableCollection<string> _currentServers = null;
        private ObservableCollection<TargetDatabases> _targets = null;
        private MultiplexerService _service = null;
        private DatabaseInfoRepository _databaseRepo = null;
        private ServerManager VerifiedServers { get; set; }

        public ServerManager.Connection ComboBoxCurrentServer
        {
            get 
            {
                ServerManager.Connection obj = null;

                if (cbServers.SelectedIndex > -1)
                    obj = (ServerManager.Connection)cbServers.SelectedItem;
                else
                {
                    obj = new ServerManager.Connection();
                    obj.Verified = false;
                    obj.ServerName = cbServers.Text;
                }

                return obj; 
            }
        }

        public string ListCurrentServer { get { return Convert.ToString(lstBxCurrentServers.SelectedValue); } }

        public ServerExplorerControl()
        {
            InitializeComponent();

            _currentServers = new ObservableCollection<string>();
            _targets = new ObservableCollection<TargetDatabases>();
            _service = new MultiplexerService();
            _databaseRepo = new DatabaseInfoRepository();
            
            VerifiedServers = new ServerManager();

            cbServers_Refresh();
        }

        private void cbServers_Refresh()
        {
            cbServers.ItemsSource = new ObservableCollection<ServerManager.Connection>(VerifiedServers.Servers);
        }

        public void DEBUG_SetServer(string serverName)
        {
            cbServers.Text = serverName;

            btnServerAdd_Click(this, null);

            for (int i = 0; i < 2; i++)
            {
                lstBxCurrentServers.SelectedIndex = i;

                lstBxDatabases.SelectedIndex = i;

                ListBoxItem_MouseDoubleClick(this, null);

                var obj = ((TargetDatabases)dgTargets.Items[i]);
                obj.IsSelected = true;
            }
        }

        private void cbServers_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnServerTest_Click(object sender, RoutedEventArgs e)
        {
            TestServer();
        }

        private bool TestServer(bool showMessageOnFailureOnly = false)
        {
            ServerManager.Connection con = ComboBoxCurrentServer;

            bool isSuccessful = new MultiplexerService().IsServerReachable(con.ServerName);

            con.Verified = isSuccessful;

            VerifiedServers.UpdateServers(con);

            cbServers_Refresh();

            bool showMessage = true;

            if (showMessageOnFailureOnly)
                showMessage = !isSuccessful;

            if (showMessage)
            {
                string strMessage = isSuccessful ? "Connection made successfully." : "The specified server is not reachable.\nCheck your spelling or if your machine has access outside of this program.\nThis program strictly uses windows authentication (SSPI) to make a connection.";

                MessageBox.Show(strMessage);
            }

            return isSuccessful;
        }

        private void btnServerAdd_Click(object sender, RoutedEventArgs e)
        {
            AddToServerList(cbServers.Text);
        }

        private void AddToServerList(string serverName)
        {
            if (!TestServer(true))
                return;

            _currentServers.Add(serverName);

            lstBxCurrentServers.ItemsSource = _currentServers;
        }

        private void lstBxCurrentServers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lstBxDatabases.Items.Clear();
            lstBxDatabases.ItemsSource = _databaseRepo.GetDatabases(ListCurrentServer);
        }

        private void TargetDatabasesAdd(DatabaseInfo target)
        {
            _targets.Add(new TargetDatabases(ListCurrentServer, target));

            dgTargets.ItemsSource = _targets;
        }

        private void TargetDatabasesRemove(TargetDatabases target)
        {
            _targets.Remove(target);

            dgTargets.ItemsSource = _targets;
        }

        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TargetDatabasesAdd((DatabaseInfo)lstBxDatabases.SelectedItem);
        }

        private void ListBoxItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                TargetDatabasesAdd((DatabaseInfo)lstBxDatabases.SelectedItem);
        }

        public List<DatabaseInfo> GetTargetDatabases()
        {
            TargetDatabases obj = null;

            var lst = new List<DatabaseInfo>();

            foreach (object t in dgTargets.Items)
            {
                obj = (TargetDatabases)t;

                if(obj.IsSelected)
                    lst.Add(obj.DataSource);
            }

            return lst;
        }

        private void dgTargets_btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var obj = ((FrameworkElement)sender).DataContext as TargetDatabases;

            if (obj == null)
                return;

            TargetDatabasesRemove(obj);
        }

        private void dgTargets_chkBxSelectAll_Toggle(object sender, RoutedEventArgs e)
        {
            var chkBx = sender as CheckBox;

            if (chkBx == null)
                return;

            bool isChecked = chkBx.IsChecked.GetValueOrDefault();

            foreach (TargetDatabases td in _targets)
                td.IsSelected = isChecked;

            dgTargets.ItemsSource = _targets;
            dgTargets.Items.Refresh();
        }

        private void btnAddMultipleTargets_Click(object sender, RoutedEventArgs e)
        {
            foreach (DatabaseInfo di in lstBxDatabases.SelectedItems)
                TargetDatabasesAdd(di);
        }
    }
}
