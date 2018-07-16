using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using F5EverywhereLib.Domain;
using F5EverywhereLib.Service;

namespace F5EverywhereUi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MultiplexerService _service = null;

        public Dictionary<DatabaseInfo, ExecutionResult> CurrentResult { get; set; }

        public string SqlText { get { return ctrlEditor.GetSqlText(); } }

        public List<DatabaseInfo> Databases { get { return ctrlServers.GetTargetDatabases(); } }

        public MainWindow()
        {
            InitializeComponent();

            _service = new MultiplexerService();

            ctrlServers.DEBUG_SetServer(".\\SQLEXPRESS");
            ctrlEditor.DEBUG_SetSqlText("SELECT Col1 = 1, C2 = NULL, Col3 = 'This is a string', Col4 = GETDATE(), Col5 = RAND()", 10, 10);
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
                ExecuteSql();
        }

        private void btnExecuteSql_Click(object sender, RoutedEventArgs e)
        {
            ExecuteSql();
        }

        private void btnParseSql_Click(object sender, RoutedEventArgs e)
        {
            DisplayResults(_service.ParseSql(Databases, SqlText));
        }

        private void ExecuteSql()
        {
            DisplayResults(_service.ExecuteSql(Databases, SqlText));
        }

        private void DisplayResults(Dictionary<DatabaseInfo, ExecutionResult> results)
        {
            ctrlResults.PresentResults(results, chkBxUnionResults.IsChecked.GetValueOrDefault());
        }
    }
}
