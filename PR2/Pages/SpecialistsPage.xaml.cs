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
    /// Логика взаимодействия для SpecialistsPage.xaml
    /// </summary>
    public partial class SpecialistsPage : Page
    {
        public SpecialistsPage()
        {
            InitializeComponent();
            GridSpecialists.ItemsSource = BaseClass.tBE.Specialists.ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Framec.MainFrame.Navigate(new Menu_admin());
        }

        private void btnYbv_Click(object sender, RoutedEventArgs e)
        {
            GridSpecialists.ItemsSource = BaseClass.tBE.Specialists.OrderBy(z => z.Surname).ToList();
            
        }

        private void btnVozr_Click(object sender, RoutedEventArgs e)
        {
            GridSpecialists.ItemsSource = BaseClass.tBE.Specialists.OrderByDescending(z => z.Surname).ToList();
        }

        private void btnM_Click(object sender, RoutedEventArgs e)
        {
            GridSpecialists.ItemsSource = BaseClass.tBE.Specialists.Where(z => z.Kod_pola == 2).ToList();
        }

        private void btnW_Click(object sender, RoutedEventArgs e)
        {
            GridSpecialists.ItemsSource = BaseClass.tBE.Specialists.Where(z => z.Kod_pola == 1).ToList();
        }

        private void Surname_Click(object sender, RoutedEventArgs e)
        {
            spSurnname.Visibility = Visibility.Visible;
            spName.Visibility = Visibility.Collapsed;
            btnSearch.Visibility = Visibility.Visible;
        }

        private void Name_Click(object sender, RoutedEventArgs e)
        {
            spSurnname.Visibility = Visibility.Collapsed;
            spName.Visibility = Visibility.Visible;
            btnSearch.Visibility = Visibility.Visible;
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            spSurnname.Visibility = Visibility.Collapsed;
            spName.Visibility = Visibility.Collapsed;
            btnSearch.Visibility = Visibility.Collapsed;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if(tbSurnname.Text != "")
            {
                GridSpecialists.ItemsSource = BaseClass.tBE.Specialists.Where(z => z.Surname == tbSurnname.Text).ToList();
            }
            if(tbName.Text != "")
            {
                GridSpecialists.ItemsSource = BaseClass.tBE.Specialists.Where(z => z.Name == tbName.Text).ToList();
            }
        }

        private void btnКуыуе_Click(object sender, RoutedEventArgs e)
        {
            GridSpecialists.ItemsSource = BaseClass.tBE.Specialists.ToList();
            tbName.Text = "";
            tbSurnname.Text = "";
        }
    }
}

