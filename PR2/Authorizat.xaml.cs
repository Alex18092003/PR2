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
    /// Логика взаимодействия для Authorizat.xaml
    /// </summary>
    public partial class Authorizat : Page
    {
        public Authorizat()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Framec.MainFrame.Navigate(new PageMain());
        }

        private void btnAvtoriz_Click(object sender, RoutedEventArgs e)
        {
            int p = tbPassword.Password.GetHashCode();
            Specialists specialists = BaseClass.tBE.Specialists.FirstOrDefault(x=> x.Login == tbLogin.Text && x.Password == p);
            if (specialists != null)
            {
                MessageBox.Show("Есть пользователь");
            }
            else
            {
                MessageBox.Show("Нет пользователя");
            }
        }
    }
}