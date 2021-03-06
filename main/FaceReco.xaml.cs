﻿using System;
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
    /// FaceReco.xaml 的交互逻辑
    /// </summary>
    public partial class FaceReco : Window
    {
        public string username;//定义用户名变量
        private DispatcherTimer dateTimer;//获取系统时间的定时器

        private DispatcherTimer ggTimer;//广告定时器
        private int ggInterval = 3;//广告轮播时间
        private int index = 0;//轮播的index
        private int Index = 0;//提示的index
        private string ggFolder = "../../FaceReco_img/";

        private int countSecond = 13;//倒计时
        private DispatcherTimer disTimer;//定时器
        private DispatcherTimer tipsTimer;//定时器
        private string[] tips = { "请微笑一下", "请眨下眼睛", "我们正在为您解析"};
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

            tipsTimer = new DispatcherTimer();
            tipsTimer.Interval = new TimeSpan(0, 0, 0, 3);//3秒切换提示
            tipsTimer.Tick += new EventHandler(showTips);
            tipsTimer.Start();

        }
        //倒计时
        void disTimer_Tick(object sender, EventArgs e)
        {

            if (countSecond == 0)
            {
                disTimer.Stop();
                var newWindow = new CollectMsg();
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


        private void showTips(object sender, EventArgs e)
        {
            if (Index < 3)
            {
                tipsLb.Content=tips[Index++];
            }
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

        //实时显示时间
        private void showTime(object sender, EventArgs e)
        {
            weekDayLb.Content = DateTime.Now.ToString("ddd");
            timeLb.Content = DateTime.Now.ToString("HH:mm");
            dateLb.Content = DateTime.Now.ToString("yyyy/MM/dd");
        }
        public FaceReco()
        {
            InitializeComponent();
            username = "xxx";
            nameText.Content ="尊敬的" +username+"先生";
        }
     
        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            ((MediaElement)sender).Position = ((MediaElement)sender).Position.Add(TimeSpan.FromMilliseconds(1));
        }
        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new MainWindow();
            newWindow.Show();
            this.Close();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            dateTimer.Stop();
            ggTimer.Stop();
            disTimer.Stop();
        }

     
    }
}
