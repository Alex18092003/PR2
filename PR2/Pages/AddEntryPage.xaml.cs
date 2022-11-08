using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для AddEntryPage.xaml
    /// </summary>
    public partial class AddEntryPage : Page
    {
        public AddEntryPage()
        {
            InitializeComponent();

            listServ.ItemsSource = BaseClass.tBE.Services.ToList();

            cbClient.ItemsSource = BaseClass.tBE.Clients.ToList();
            cbClient.SelectedValuePath = "Kod_client";
            cbClient.DisplayMemberPath = "FIO";

            cbClient.SelectionChanged += cbClient_SelectedIndexChanged;

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Framec.MainFrame.Navigate(new EntryPage1());
        }

        private void tbSurname_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex r1 = new Regex("^[0-9]+");
            e.Handled = r1.IsMatch(e.Text);
        }

        private void tbName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex r1 = new Regex("^[0-9]+");
            e.Handled = r1.IsMatch(e.Text);
        }

        private void tbPatr_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex r1 = new Regex("^[0-9]+");
            e.Handled = r1.IsMatch(e.Text);
        }

        private void tbSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbSurname.Text.Length == 1)
            {
                tbSurname.Text = tbSurname.Text.ToUpper();
                tbSurname.Select(tbSurname.Text.Length, 0);

            }
        }

        private void tbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbName.Text.Length == 1)
            {
                tbName.Text = tbName.Text.ToUpper();
                tbName.Select(tbName.Text.Length, 0);

            }
        }

        private void tbPatr_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbPatr.Text.Length == 1)
            {
                tbPatr.Text = tbPatr.Text.ToUpper();
                tbPatr.Select(tbPatr.Text.Length, 0);

            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Yes_Checked(object sender, RoutedEventArgs e)
        {
            stYes.Visibility = Visibility.Collapsed;
            stNo.Visibility = Visibility.Visible;
        }

        private void No_Checked(object sender, RoutedEventArgs e)
        {
            stYes.Visibility = Visibility.Visible;
            stNo.Visibility = Visibility.Collapsed;

        }
        private int liv_num;
        private void cbClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            var value = (sender as ComboBox);
            liv_num = (int)value.SelectedItem;
            tbPhon.Text = liv_num.ToString();
        }
    }
}
 

