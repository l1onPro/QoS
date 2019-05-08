using System;
using System.Collections.Generic;
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

namespace QoS
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Test test = new Test();

        public MainWindow()
        {
            InitializeComponent();

            Testtxt.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
        }
              

        private void Testtxt_MouseMove(object sender, MouseEventArgs e)
        {
            Testtxt.Text = test.getResult();
        }
    }
}
