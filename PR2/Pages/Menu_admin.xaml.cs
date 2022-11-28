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
    /// Логика взаимодействия для Menu_admin.xaml
    /// </summary>
    public partial class Menu_admin : Page
    {
        Specialists specialists;

        public Menu_admin(Specialists specialists)
        {
            InitializeComponent();
            this.specialists = specialists;  //  заполняем выше созданный объект информацией об авторизованном пользователе
            
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Framec.MainFrame.Navigate(new Authorizat());
        }

        private void btnSpecialists_Click(object sender, RoutedEventArgs e)
        {
            Framec.MainFrame.Navigate(new SpecialistsPage());
        }

        private void btnEntry_Click(object sender, RoutedEventArgs e)
        {
            Framec.MainFrame.Navigate(new EntryPage1());
        }

        private void buttonCabinet_Click(object sender, RoutedEventArgs e)
        {
            Framec.MainFrame.Navigate(new Menu_polzovatel(specialists));
        }
    }
}
