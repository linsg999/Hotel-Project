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
    /// CheckMsg.xaml 的交互逻辑
    /// </summary>
    public partial class CheckMsg : Window
    {
        //订单内部类
        class order
        {

            private string room;//入住房间
            private string checkoutTime;//离开时间
            private string checkinTime;//入住时间


            public string Room
            {
                get { return room; }
                set { room = value; }
            }
            public string CheckinTime
            {
                get { return checkinTime; }
                set { checkinTime = value; }
            }
            public string CheckoutTime
            {
                get { return checkoutTime; }
                set { checkoutTime = value; }
            }

            public order(string room, string checkinTime, string checkoutTime)
            {
                this.room = room;
                this.checkinTime = checkinTime;
                this.checkoutTime = checkoutTime;
            }
        }
        public string checkroom;
        public string checktime;
        public string lefttime;
        private DispatcherTimer dateTimer;//获取系统时间的定时器

        private DispatcherTimer ggTimer;//广告定时器
        private int ggInterval = 3;//广告轮播时间
        private int index = 0;//轮播的index
        private string ggFolder = "../../CheckMsg_img/";

        ArrayList orders = new ArrayList();//订单list
        private string orderInfo;//订单提示
        private int orderIndex = 0;//订单索引


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dateTimer = new DispatcherTimer();
            dateTimer.Interval = new TimeSpan(0, 0, 0, 1);
            dateTimer.Tick += new EventHandler(showTime);
            dateTimer.Start();
            ggTimer = new DispatcherTimer();
            ggTimer.Interval = new TimeSpan(0, 0, 0, ggInterval);
            ggTimer.Tick += new EventHandler(showGg);
            ggTimer.Start();

            //模拟订单，用于测试效果
            order od1 = new order("宜必思酒店", "2018年04月26日", "2018年04月27日");
            order od2 = new order("宜必思酒店111", "2018年04月20日", "2018年04月21日");
            order od3 = new order("宜必思酒店222", "2018年04月22日", "2018年04月23日");

            orders.Add(od1);
            orders.Add(od2);
            orders.Add(od3);

            if (orders.Count == 1)
            {
                //唯一订单,不展示上一个下一个

                lastBtn.Visibility = Visibility.Collapsed;
                nextBtn.Visibility = Visibility.Collapsed;
                //订单内容
                orderInfo = "您好，您入住房间的时间在明天，领取房卡后，请妥善保管好您的房卡，在入住当天下午14:00前办理入住......";
                checkroom = (orders[0] as order).Room;
                checktime = (orders[0] as order).CheckinTime;
                lefttime = (orders[0] as order).CheckoutTime;
                orderMsg.Text = orderInfo;
                checkRoom.Content = checkroom;
                checkTime.Content = checktime;
                leftTime.Content = lefttime;

            }
            else {
                //多个订单,点击上一个下一个时可以用orderIndex来对应哪一个
                lastBtn.Visibility = Visibility.Visible;
                nextBtn.Visibility = Visibility.Visible;
                //订单内容
                orderInfo = "您好，您入住房间的时间在明天，领取房卡后，请妥善保管好您的房卡，在入住当天下午14:00前办理入住......";
                checkroom = (orders[0] as order).Room;
                checktime = (orders[0] as order).CheckinTime;
                lefttime = (orders[0] as order).CheckoutTime;
                orderMsg.Text = orderInfo;
                checkRoom.Content = checkroom;
                checkTime.Content = checktime;
                leftTime.Content = lefttime;
               
            }
        }
        //点击上一个
        private void lastBtn_Click(object sender, RoutedEventArgs e)
        {
            nextBtn.Visibility = Visibility.Visible;
            //当前为第一个订单信息  不能点击上一个
            if (orderIndex == 0)
            {
                lastBtn.Visibility = Visibility.Collapsed;
            }
            else
            {
                lastBtn.Visibility = Visibility.Visible;
                orderIndex -= 1;
                orderInfo = "您好，您入住房间的时间在明天，领取房卡后，请妥善保管好您的房卡，在入住当天下午14:00前办理入住......";
                checkroom = (orders[orderIndex] as order).Room;
                checktime = (orders[orderIndex] as order).CheckinTime;
                lefttime = (orders[orderIndex] as order).CheckoutTime;
                orderMsg.Text = orderInfo;
                checkRoom.Content = checkroom;
                checkTime.Content = checktime;
                leftTime.Content = lefttime;
            }
           
        }
        //点击下一个
        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            lastBtn.Visibility = Visibility.Visible;
            //当前为最后一个订单信息 不能点击下一个
            if (orderIndex == orders.Count-1)
            {
                nextBtn.Visibility = Visibility.Collapsed;
            }
            else
            {
                nextBtn.Visibility = Visibility.Visible;
                orderIndex+=1;
                orderInfo = "您好，您入住房间的时间在明天，领取房卡后，请妥善保管好您的房卡，在入住当天下午14:00前办理入住......";
                checkroom = (orders[orderIndex] as order).Room;
                checktime = (orders[orderIndex ] as order).CheckinTime;
                lefttime = (orders[orderIndex] as order).CheckoutTime;
                orderMsg.Text = orderInfo;
                checkRoom.Content = checkroom;
                checkTime.Content = checktime;
                leftTime.Content = lefttime;
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
        public CheckMsg()
        {
            InitializeComponent();
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
        private void Window_Closed(object sender, EventArgs e)
        {
            dateTimer.Stop();
            ggTimer.Stop();
        }

     
    }
}
