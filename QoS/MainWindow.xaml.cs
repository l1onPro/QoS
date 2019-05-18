using QoS.RouterApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace QoS
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        Router router;

        public MainWindow()
        {
            InitializeComponent();
            
            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;
            cmbNumAlgorithm.IsEnabled = true;
        }
       
        /// <summary>
        /// Запуск таймеров
        /// </summary>
        public void Start()
        {
            int numAlg = cmbNumAlgorithm.SelectedIndex;
            router = new Router(numAlg, paint);

            router.Start();
        }

        /// <summary>
        /// Остановка таймеров
        /// </summary>
        public void Stop()
        {
            router.Stop();
        }        

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false;
            btnStop.IsEnabled = true;
            cmbNumAlgorithm.IsEnabled = false;

            Start();
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;
            cmbNumAlgorithm.IsEnabled = true;

            Stop();
        }           
    }
}
