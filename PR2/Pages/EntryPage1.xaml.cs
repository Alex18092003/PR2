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
    /// Логика взаимодействия для EntryPage1.xaml
    /// </summary>
    public partial class EntryPage1 : Page
    {
        public EntryPage1()
        {
            InitializeComponent();
            listEntry.ItemsSource = BaseClass.tBE.Entry.ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Framec.MainFrame.Navigate(new Menu_admin());
        }

        private void tbServices_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            List<Connect> ES = BaseClass.tBE.Connect.Where(x => x.Kod_entry == index).ToList();
            string str = "";
            foreach(Connect es in ES)
            {
                str += es.Services.Service + "\n";
            }
            tb.Text = str.Substring(0, str.Length - 2);

        }

        private void tbPrice_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            List<Connect> ES = BaseClass.tBE.Connect.Where(x => x.Kod_entry == index).ToList();
            int sum = 0;
            foreach (Connect es in ES)
            {
                sum += es.Services.Price;
            }
            tb.Text = sum.ToString();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Framec.MainFrame.Navigate(new AddEntryPage());
        }

        //private void Delete_Click(object sender, RoutedEventArgs e)
        //{
        //    Button btn = (Button)sender;
        //    int index = Convert.ToInt32(btn.Uid);
        //    Entry entry = BaseClass.tBE.Entry.FirstOrDefault(x => x.Kod_entry == index);
        //    BaseClass.tBE.Entry.Remove(entry);
        //    Framec.MainFrame.Navigate(new EntryPage1());
        //    BaseClass.tBE.SaveChanges();

        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            Entry entry = BaseClass.tBE.Entry.FirstOrDefault(x => x.Kod_entry == index);
            BaseClass.tBE.Entry.Remove(entry);
            BaseClass.tBE.SaveChanges();
            Framec.MainFrame.Navigate(new EntryPage1());
        }
    }
}
