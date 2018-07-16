using System.Text;
using System.Windows.Controls;

namespace F5EverywhereUi
{
    /// <summary>
    /// Interaction logic for SqlEditorControl.xaml
    /// </summary>
    public partial class SqlEditorControl : UserControl
    {
        public SqlEditorControl()
        {
            InitializeComponent();
        }

        public string GetSqlText()
        {
            return txtSqlText.Text;
        }

        public void DEBUG_SetSqlText(string sqlText, int virtualTables, int rowsPerVirtualTable)
        {
            var sb = new StringBuilder();

            for (int j = 0; j < rowsPerVirtualTable; j++)
            {
                sb.AppendLine(sqlText);
                sb.AppendLine(" UNION ALL ");
            }

            sb.AppendLine(sqlText).Append(";");

            string str = sb.ToString();

            sb.Clear();

            for (int i = 0; i < virtualTables; i++)
                sb.AppendLine(str);

            txtSqlText.Text = sb.ToString();
        }
    }
}
