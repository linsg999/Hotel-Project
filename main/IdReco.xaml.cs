using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        private DispatcherTimer dateTimer;//获取系统时间的定时器
        private DispatcherTimer ggTimer;//广告定时器
        private int ggInterval = 3;//广告轮播时间
        private int index = 0;//轮播的index
        private string ggFolder = "../../IdReco_img/";

        private int countSecond = 10;//倒计时
        private DispatcherTimer disTimer;//定时器
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dateTimer = new DispatcherTimer();
            dateTimer.Interval = new TimeSpan(0, 0, 0, 1);
            dateTimer.Tick += new EventHandler(showTime);
            dateTimer.Start();

            disTimer = new DispatcherTimer();
            disTimer.Interval = new TimeSpan(0, 0, 0, 1);
            disTimer.Tick += new EventHandler(disTimer_Tick);
            disTimer.Start();

            ggTimer = new DispatcherTimer();
            ggTimer.Interval = new TimeSpan(0, 0, 0, ggInterval);
            ggTimer.Tick += new EventHandler(showGg);
            ggTimer.Start();
        }
        //轮播广告
        private void showGg(object sender, EventArgs e)
        {
            ArrayList imgList = new ArrayList();
            DirectoryInfo folder = new DirectoryInfo(ggFolder);
            //遍历文件
            foreach (FileInfo NextFile in folder.GetFiles())
                imgList.Add(NextFile.Name);

            if (index == imgList.Count)
                index = 0;

            string imgPath = folder.FullName + imgList[index];
            ggImg.Source = new BitmapImage(new Uri(imgPath, UriKind.Absolute));
            index++;
        }
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
        private void Window_Closed(object sender, EventArgs e)
        {
            dateTimer.Stop();
            ggTimer.Stop();
        }
    }
}
