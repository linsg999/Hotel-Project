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
    /// PhoneReco.xaml 的交互逻辑
    /// </summary>
    public partial class PhoneReco : Window
    {
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
      
      
    
        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
    
        }

        private void retrySendBtn_Click(object sender, RoutedEventArgs e)
        {
    
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            phoneText.Focus();
            psdLabel.Visibility = Visibility.Hidden;
            psdText.Visibility = Visibility.Hidden;
            psdBlock.Visibility = Visibility.Hidden;
            retrySendBtn.Visibility = Visibility.Hidden;


        }
     
    }
}
