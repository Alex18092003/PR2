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

namespace PR2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BaseClass.tBE = new Entities();
            Framec.MainFrame = fMain;
            Framec.MainFrame.Navigate(new Authorizat());
        }
        //админ = логин - admin, пароль - admin
        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            Framec.MainFrame.Navigate(new Registration());

        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            Framec.MainFrame.Navigate(new Authorizat());
        }
    }
}
