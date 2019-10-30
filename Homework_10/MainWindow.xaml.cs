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
using Homework_10.Model;

namespace Homework_10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyBot client;

        public MainWindow()
        {
            InitializeComponent();
            client = new MyBot(this);
            buttonList.ItemsSource = client.botButtons.Where(x => x.ParentId == 0).OrderBy(x => x.Row).ThenBy(x => x.Column).GroupBy(x => x.Row).ToList(); ;
        }

        private void btnDoubleClik(object sender, MouseButtonEventArgs e)
        {          
            Debug.WriteLine($"Двойной клик: ");
        }
    }
}
