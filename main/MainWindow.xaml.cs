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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace main
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }    

        private void checkBtn_Click(object sender, RoutedEventArgs e)//入房办理
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
   
    }
}
