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

namespace main
{
    /// <summary>
    /// IdReco.xaml 的交互逻辑
    /// </summary>
    public partial class IdReco : Window
    {
        //定义跳转变量 当变量参数=某个信号时（可以为true），跳转到Verify.xaml，订单查询中。
        public bool goVerify = false;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (goVerify == true)
            {
                var newWindow = new Verify();
                newWindow.Show();
                this.Close();
            }
        }  
        public IdReco()
        {
            InitializeComponent();
        }       

        private void homeBtn_Click(object sender, RoutedEventArgs e)//回到首页
        {
            var newWindow = new MainWindow();
            newWindow.Show();
            this.Close();
        }
        private void idBtn_Click(object sender, RoutedEventArgs e)//未携带身份证
        {
            var newWindow = new PhoneReco();
            newWindow.Show();
            this.Close();
        }       

    }
}
