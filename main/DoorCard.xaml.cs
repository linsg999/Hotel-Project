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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace main
{
    /// <summary>
    /// idWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DoorCard : Window
    {
        private DispatcherTimer dateTimer;//获取系统时间的定时器
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dateTimer = new DispatcherTimer();
            dateTimer.Interval = new TimeSpan(0, 0, 0, 1);
            dateTimer.Tick += new EventHandler(showTime);
            dateTimer.Start();
        }

        //实时显示时间
        private void showTime(object sender, EventArgs e)
        {
            weekDayLb.Content = DateTime.Now.ToString("ddd");
            timeLb.Content = DateTime.Now.ToString("HH:mm");
            dateLb.Content = DateTime.Now.ToString("yyyy/MM/dd");
        }
        public DoorCard()
        {
            InitializeComponent();
           
      
        }
        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)//gif动图
        {
            ((MediaElement)sender).Position = ((MediaElement)sender).Position.Add(TimeSpan.FromMilliseconds(1));
        }
        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new MainWindow();
            newWindow.Show();
            this.Close();
        }
    }


}
