﻿
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PR2
{
    /// <summary>
    /// Логика взаимодействия для Menu_polzovatel.xaml
    /// </summary>
    public partial class Menu_polzovatel : Page
    {
        Specialists specialists;
        public Menu_polzovatel(Specialists specialists)
        {
            
            InitializeComponent();
            this.specialists = specialists;
            textboxName.Text = specialists.Name;
            textboxSurname.Text = specialists.Surname;
            textboxPatronymic.Text = specialists.Patronymic;

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Framec.MainFrame.Navigate(new Authorizat());
        }

        private void buttonChange_Click(object sender, RoutedEventArgs e)
        {
            Pages.WindowPerson windowPerson = new Pages.WindowPerson(specialists);
            windowPerson.ShowDialog();
            Framec.MainFrame.Navigate(new Menu_polzovatel(specialists));
        }
    }
}
