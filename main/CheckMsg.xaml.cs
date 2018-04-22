﻿using System;
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
    /// CheckMsg.xaml 的交互逻辑
    /// </summary>
    public partial class CheckMsg : Window
    {
        public string ordermsg;
        public string checkroom;
        public string checktime;
        public string lefttime;
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
        public CheckMsg()
        {
            InitializeComponent();
            ordermsg = "您好，您入住房间的时间在明天，领取房卡后，请妥善保管好您的房卡，在入住当天下午14:00前办理入住......";
            checkroom = "xxxx酒店-21栋-308房间";
            checktime = "2018年4月22日";
            lefttime = "2018年4月23日";
            orderMsg.Text = ordermsg;
            checkRoom.Content = checkroom;
            checkTime.Content = checktime;
            leftTime.Content = lefttime;
        }
        private void homeBtn_Click(object sender, RoutedEventArgs e)//回到首页
        {
            var newWindow = new MainWindow();
            newWindow.Show();
            this.Close();
        }

        private void enterBtn_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new DoorCard();
            newWindow.Show();
            this.Close();
        }

      

    }
}
