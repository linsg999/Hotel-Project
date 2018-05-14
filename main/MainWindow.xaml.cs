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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer dateTimer;//获取系统时间的定时器

        private DispatcherTimer ggTimer;//广告定时器
        private int ggInterval = 3;//广告轮播时间
        private int index = 0;//轮播的index
        private string ggFolder = "../../MainWindow_img/";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void checkBtn_Click(object sender, RoutedEventArgs e)//入住办理
        {
            var newWindow = new IdReco();
            newWindow.Show();
            this.Close();
        }
        //扫码入住的事件
        private void codeBtn_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new IdReco();
            newWindow.Show();
            this.Close();
        }

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

        private void Window_Closed(object sender, EventArgs e)
        {
            dateTimer.Stop();
            ggTimer.Stop();
        }

        private void checkBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            //checkBtn.Style = checkBtn2.Style;
            checkBtn2.Visibility = Visibility.Visible;
            checkBtn.Visibility = Visibility.Collapsed;
        }
       
        private void checkBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            //checkBtn.Style = checkBtn.Style;
            checkBtn.Visibility = Visibility.Visible;
            checkBtn2.Visibility = Visibility.Collapsed;
        }

        private void codeBtn_MouseEnter(object sender, MouseEventArgs e)
        {
           // codeBtn.Style = codeBtn2.Style;
            codeBtn2.Visibility = Visibility.Visible;
            codeBtn.Visibility = Visibility.Collapsed;
        }

        private void codeBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            //codeBtn.Style = codeBtn2.Style;
            codeBtn.Visibility = Visibility.Visible;
            codeBtn2.Visibility = Visibility.Collapsed;
        }

       
    }
}
