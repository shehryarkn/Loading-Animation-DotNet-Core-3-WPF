using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace LoadingAnimation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker worker;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            long sum = 0;
            long total = 100000;
            for (long i = 1; i <= total; i++)
            {
                sum += i;
                int percentage = Convert.ToInt32(((double)i / total) * 100);

                Dispatcher.Invoke(new System.Action(() =>
                {
                    worker.ReportProgress(percentage);
                }));
            }
            MessageBox.Show("Sum: " + sum);
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            imgCircle.Visibility = Visibility.Collapsed;
            PerformTask.IsEnabled = true;
        }

        private void PerformTask_Click(object sender, RoutedEventArgs e)
        {
            imgCircle.Visibility = Visibility.Visible; //Make Progressbar visible
            PerformTask.IsEnabled = false; //Disabling the button
            worker = new BackgroundWorker(); //Initializing the worker object
            worker.DoWork += Worker_DoWork; //Binding Worker_DoWork method
            worker.WorkerReportsProgress = true; //telling the worker that it supports reporting progress
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted; //Binding worker_RunWorkerCompleted method
            worker.RunWorkerAsync(); //Executing the worker
        }
    }
}
