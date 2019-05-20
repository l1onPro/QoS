using QoS.Class_of_Service;
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
            stackPanelSetting.IsEnabled = false;

            btnChangeForSizeQueue.IsEnabled = false;
            btnChangeForSpeed.IsEnabled = false;
            txtSizeQueue.IsEnabled = false;
            txtSpeed.IsEnabled = false;

            SetSetting();
            Start();
        }

        private int getSize()
        {
            if (rdbSizeBig.IsChecked == true) return 0;
            if (rdbSizeAverage.IsChecked == true) return 1;
            if (rdbSizeSmall.IsChecked == true) return 2;
            throw new Exception();
        }

        private int getLoading()
        {
            if (rdbLoadingBig.IsChecked == true) return 0;
            if (rdbLoadingAvarage.IsChecked == true) return 1;
            if (rdbLoadingSmall.IsChecked == true) return 2;
            throw new Exception();
        }

        private void SetSetting()
        {            
            Setting.SizePaint = getSize();
            Setting.TypeFrequencyGenPack = getLoading();
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;
            cmbNumAlgorithm.IsEnabled = true;
            stackPanelSetting.IsEnabled = true;

            btnChangeForSizeQueue.IsEnabled = true;
            btnChangeForSpeed.IsEnabled = true;
            txtSizeQueue.IsEnabled = true;
            txtSpeed.IsEnabled = true;

            Stop();
        }

        private void BtnChangeForSizeQueue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string text = txtSizeQueue.Text.ToString();
                int size = Convert.ToInt32(text);

                if (size < Setting.MinConstSizeQueuering || size > Setting.MaxConstSizeQueuering) throw new Exception("Выход за пределы диапазона: "
                    + Setting.MinConstSizeQueuering + " - " + Setting.MaxConstSizeQueuering + "байт");

                Setting.CurSizeQueuering = size;
                MessageBox.Show("Максимальная длина очереди изменена на " + size, "Изменения выполнены", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                txtSizeQueue.Text = Setting.CurSizeQueuering.ToString();
                MessageBox.Show("Не удалось выполнить: " + ex.Message, "Ошибка изменения длины очереди", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnChangeForSpeed_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string text = txtSpeed.Text.ToString();
                int size = Convert.ToInt32(text);

                if (size < Setting.MinConstSpeed || size > Setting.MaxConstSpeed) throw new Exception("Выход за пределы диапазона: "
                    + Setting.MinConstSpeed + " - " + Setting.MaxConstSpeed + "байт");

                Setting.CurSpeed = size;
                MessageBox.Show("Скорость пропускания изменена на " + size, "Изменения выполнены", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                txtSpeed.Text = Setting.CurSpeed.ToString();
                MessageBox.Show("Не удалось выполнить: " + ex.Message, "Ошибка изменения скорости пропускания", MessageBoxButton.OK, MessageBoxImage.Error);                
            }
        }
    }
}
