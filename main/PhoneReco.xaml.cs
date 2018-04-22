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
    /// PhoneReco.xaml 的交互逻辑
    /// </summary>
    public partial class PhoneReco : Window
    {
        private static int initTime=120;//倒计时初始时间
        private int countSecond = initTime;//倒计时时间
        private DispatcherTimer disTimer;//定时器
        public PhoneReco()
        {
            InitializeComponent();
        }
        //按Esc键退出全屏  
        private void PhoneReco_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)//Esc键  
            {
                this.WindowState = System.Windows.WindowState.Normal;
                this.WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
                this.Close();
            }
        }  

        private void homeBtn_Click(object sender, RoutedEventArgs e)//回到首页
        {
            var newWindow = new MainWindow();
            newWindow.Show();
            this.Close();
        }
        private void phoneText_Enter(object sender, EventArgs e)//用户名文本框焦点获得事件
        {
            if (phoneText.Text == "请输入您的手机号")
            {
                phoneText.Text = "";
            }
        }

      
        private void codeText_Enter(object sender, EventArgs e) //密码框焦点获得
        {
            if (codeText.Text == "请输入验证码")
            {
                codeText.Text = "";
            }
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            phoneText.Text = "请输入您的手机号";
        }

        private void retrySendBtn_Click(object sender, RoutedEventArgs e)
        {
            codeText.Text = "请输入验证码";
        }

        private void Viewbox_Loaded(object sender, RoutedEventArgs e)
        {
            disTimer = new DispatcherTimer();
            disTimer.Interval = new TimeSpan(0, 0, 0, 1);
            disTimer.Tick += new EventHandler(disTimer_Tick);
            disTimer.Start();
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

        //重置时间
        private void refreshTimer(object sender, RoutedEventArgs e)
        {
            countSecond = initTime;
        }
    }
}
