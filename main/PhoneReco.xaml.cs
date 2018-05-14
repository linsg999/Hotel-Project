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
    /// PhoneReco.xaml 的交互逻辑
    /// </summary>
    public partial class PhoneReco : Window
    {
        private static int initTime = 120;//倒计时初始时间
        private int countSecond = initTime;//倒计时时间
        private DispatcherTimer disTimer;//定时器

        private static int initTime2 = 60;//验证码倒计时初始时间
        private int countSecond2 = initTime2;//倒计时时间
        private DispatcherTimer disTimer2;//定时器


        private DispatcherTimer dateTimer;//获取系统时间的定时器
        private string phoneNum = "";
        private string VerifiCode = "";
        private string VerifiCodeT = "123456";//测试的验证码
        private int psdMsg = 01;//验证码编号(变量)
        private string message = "";//提示信息

        private DispatcherTimer ggTimer;//广告定时器
        private int ggInterval = 3;//广告轮播时间
        private int index = 0;//轮播的index
        private string ggFolder = "../../PhoneReco_img/";
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

            disTimer = new DispatcherTimer();
            disTimer.Interval = new TimeSpan(0, 0, 0, 1);
            disTimer.Tick += new EventHandler(disTimer_Tick);
            disTimer.Start();
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
        public PhoneReco()
        {
            InitializeComponent();
            phoneText.Focus();    
            msg.Visibility = Visibility.Hidden;
            psdLabel.Visibility = Visibility.Hidden;
            psdText.Visibility = Visibility.Hidden;
            psdBlock.Visibility = Visibility.Hidden;
           // retrySendBtn.Visibility = Visibility.Hidden;
           // editBtn.Visibility = Visibility.Hidden;

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


        //点击修改号码 恢复到默认
        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            phoneNumLb.Text = "     请输入手机号";
            phoneNumLb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#bfbebe")); 
            phoneNum = "";
            phoneText.Text = phoneNum;

            disTimer2.Stop();
            time.Content = "";

            psdBlock.Text = "     请输入【" + psdMsg + "】编号的验证码";
            VerifiCode = "";
            psdText.Text = VerifiCode;
            phoneText.Focusable = true;
            phoneText.IsReadOnly = false;
            phoneText.Focus();

            msg.Visibility = Visibility.Hidden;
            psdLabel.Visibility = Visibility.Hidden;
            psdText.Visibility = Visibility.Hidden;
            psdMsg2.Visibility = Visibility.Hidden;
            psdBlock.Visibility = Visibility.Hidden;
            retrySendBtn.Visibility = Visibility.Collapsed;
            retrySendBtn2.Visibility = Visibility.Collapsed;
            editBtn.Visibility = Visibility.Collapsed;
        }
        //点击重新发送
        private void retrySendBtn_Click(object sender, RoutedEventArgs e)
        {

            countSecond2 = initTime2;
            disTimer2 = new DispatcherTimer();
            disTimer2.Interval = new TimeSpan(0, 0, 0, 1);
            disTimer2.Tick += new EventHandler(disTimer2_Tick);
            disTimer2.Start();

            retrySendBtn.Visibility = Visibility.Collapsed;
            retrySendBtn2.Visibility = Visibility.Visible;

            psdBlock.Text = "     请输入【" + psdMsg + "】编号的验证码";
            psdMsg2.Visibility = Visibility.Hidden;
            VerifiCode = "";
            psdText.Text = VerifiCode;
            phoneText.IsReadOnly = false;
            psdBlock.Focus();
            psdText.Focus();

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
        //倒计时2
        void disTimer2_Tick(object sender, EventArgs e)
        {

            if (countSecond2 == 0)
            {
                disTimer2.Stop();
                time.Content = "";
          
                //retrySendBtn.IsEnabled = true;
                retrySendBtn.Visibility = Visibility.Visible;
                retrySendBtn2.Visibility = Visibility.Collapsed;
            }
            else
            {
                //判断lblSecond是否处于UI线程上
                if (time.Dispatcher.CheckAccess())
                {
                    time.Content = countSecond2.ToString();
                }
                else
                {
                    time.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(() =>
                    {
                        time.Content = countSecond2.ToString();
                    }));
                }
                countSecond2--;
            }
        }
        //键盘点击
        private void button_Clicked(object sender, RoutedEventArgs e)
        {
           
            //countSecond = initTime;
            string buttonName = ((Button)e.OriginalSource).Name;
            if (phoneText.IsFocused)
            {
                if (buttonName.Equals("clearBtn"))
                {
                    phoneNum = "";
                }
                else if (buttonName.Equals("backBtn"))
                {
                    if (phoneNum.Equals(""))
                    {
                        return;
                    }
                    phoneNum = phoneNum.Substring(0, phoneNum.Length - 1);
                }
                else if(phoneNum.Length<11)
                {
                    string num = buttonName.Substring(buttonName.Length - 1, 1);
                    phoneNum = phoneNum + num;
                }
                this.phoneNumLb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#bfbebe"));
                phoneText.Text = phoneNum;
                phoneText.SelectionStart = phoneText.Text.Length;
                //判断手机号是否为11位 输入验证码
                if (phoneNum.Length == 11)
                {
                    msg.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
                    if (phoneNum.Substring(1, 1).Equals("2") || !phoneNum.Substring(0, 1).Equals("1"))
                    {
                        message = "请输入有效的手机号码！";
                        msg.Content = message;
                        msg.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
                        msg.Visibility = Visibility.Visible;             
                        return;
                    }
                    //输入手机号码正确  发送验证码  重新发送不可点  倒计时
                    countSecond2 = initTime2;
                    disTimer2 = new DispatcherTimer();
                    disTimer2.Interval = new TimeSpan(0, 0, 0, 1);
                    disTimer2.Tick += new EventHandler(disTimer2_Tick);
                    disTimer2.Start();

                    //retrySendBtn.IsEnabled = false;
                    //retrySendBtn.Visibility = Visibility.Visible;
                    retrySendBtn2.Visibility = Visibility.Visible;

                    editBtn.Visibility = Visibility.Visible;
                    psdBlock.Focus();
                    phoneText.IsReadOnly = true;
                    message = "请输入验证码";
                    msg.Content = message;
                    msg.Visibility = Visibility.Visible;
                    psdLabel.Visibility = Visibility.Visible;
                    psdText.Visibility = Visibility.Visible;
                    psdBlock.Text = "     请输入【" + psdMsg + "】编号的验证码";
                    psdBlock.Visibility = Visibility.Visible;
                    phoneText.Focusable = false;
                    psdText.Focus();
                }
            }
            else if (psdText.IsFocused)
            {
                psdBlock.Text = "";
                if (buttonName.Equals("clearBtn"))
                {
                    VerifiCode = "";
                }
                else if (buttonName.Equals("backBtn"))
                {
                    if (VerifiCode.Equals(""))
                        return;
                    VerifiCode = VerifiCode.Substring(0, VerifiCode.Length - 1);
                }
                else if (VerifiCode.Length < 6)
                {
                    string num = buttonName.Substring(buttonName.Length - 1, 1);
                    VerifiCode = VerifiCode + num;
                }

                if (VerifiCode.Length == 6)
                {
                    if (!VerifiCode.Equals(VerifiCodeT))
                    {
                        psdMsg2.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        //验证码输入成功操作
                    }
                }

                this.psdText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
                psdText.Text = VerifiCode;
                psdText.SelectionStart = psdText.Text.Length;
            }

        }


       // 重置时间
       private void refreshTimer(object sender, RoutedEventArgs e)
        {
           countSecond = initTime;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            disTimer.Stop();
            disTimer2.Stop();
           ggTimer.Stop();
        }
       
    }
}
