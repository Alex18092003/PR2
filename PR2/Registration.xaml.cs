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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        List<Dolgnosti> DL = BaseClass.tBE.Dolgnosti.ToList();
        public Registration()
        {
            InitializeComponent();
            cbDolgn.Text = DL[0].ToString();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Framec.MainFrame.Navigate(new PageMain());
        }

        private void btnZareg_Click(object sender, RoutedEventArgs e)
        {
            int pass = tbPassword.Password.GetHashCode();

        }
    }
}
