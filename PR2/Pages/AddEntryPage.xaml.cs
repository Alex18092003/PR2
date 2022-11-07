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
            Entry newE = new Entry()
            {
                Date = Convert.ToDateTime(dbEntry.SelectedDate);
            }
        }
    }
}
