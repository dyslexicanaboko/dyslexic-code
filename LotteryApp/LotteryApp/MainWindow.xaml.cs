using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LotteryApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            lblHello.Content = "Hello World it is: " + DateTime.Now.ToString();
        }

        private void btnAddNumbers_Click(object sender, RoutedEventArgs e)
        {
            txtResult.Text = (Convert.ToInt32(txtXValue.Text) + Convert.ToInt32(txtYValue.Text)).ToString();
        }
    }
}
