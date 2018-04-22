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
    /// IdReco.xaml 的交互逻辑
    /// </summary>
    public partial class IdReco : Window
    {

        //定义跳转变量 当变量参数=某个信号时（可以为true），跳转到Verify.xaml，订单查询中。
        public bool goVerify = false;
        private int countSecond = 10;//倒计时
        private DispatcherTimer disTimer;//定时器
        private DispatcherTimer dateTimer;//获取系统时间的定时器
        public IdReco()
        {
            InitializeComponent();
        }

  
        private void homeBtn_Click(object sender, RoutedEventArgs e)//回到首页
        {
            disTimer.Stop();
            var newWindow = new MainWindow();
            newWindow.Show();
            this.Close();
        }
        private void idBtn_Click(object sender, RoutedEventArgs e)//未携带身份证
        {
            disTimer.Stop();
            var newWindow = new PhoneReco();
            newWindow.Show();
            this.Close();

        }       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            disTimer = new DispatcherTimer();
            disTimer.Interval = new TimeSpan(0, 0, 0, 1);
            disTimer.Tick += new EventHandler(disTimer_Tick);
            disTimer.Start();

            dateTimer = new DispatcherTimer();
            dateTimer.Interval = new TimeSpan(0, 0, 0, 1);
            dateTimer.Tick += new EventHandler(showTime);
            dateTimer.Start();
        }
        private void showTime(object sender, EventArgs e)
        {
            weekDayLb.Content = DateTime.Now.ToString("ddd");
            timeLb.Content = DateTime.Now.ToString("HH:mm");
            dateLb.Content = DateTime.Now.ToString("yyyy/MM/dd");
        }
        //倒计时
        void disTimer_Tick(object sender, EventArgs e)
        {

            if (countSecond == 0)
            {
                disTimer.Stop();
                var newWindow = new MainWindow();
                newWindow.Show();
                this.Close();
            }
            else
            {
                //判断lblSecond是否处于UI线程上
                if (countDownLb.Dispatcher.CheckAccess())
                {
                    countDownLb.Content = countSecond.ToString();
                }
                else
                {
                    countDownLb.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(() =>
                    {
                        countDownLb.Content = countSecond.ToString();
                    }));
                }
                countSecond--;
            }
        }
    }
}
