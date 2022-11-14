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
        Entry entry; // объект, в котором будет хранится данные о новом или отредактированном коте
        bool flagUpdate = false; // для определения, создаем мы новый объект или редактируем старый

        public void uploadFields()  // метод для заполнения списков
        {
            listServ.ItemsSource = BaseClass.tBE.Services.ToList();

            cbClient.ItemsSource = BaseClass.tBE.Clients.ToList();
            cbClient.SelectedValuePath = "Kod_client";
            cbClient.DisplayMemberPath = "FIO";

            cbClient.SelectionChanged += cbClient_SelectedIndexChanged;
        }
        // конструктор для создания нового кота (без аргументов)
        public AddEntryPage()
        {
            InitializeComponent();
            uploadFields();

        }
        // назад
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Framec.MainFrame.Navigate(new EntryPage1());
        }
        // запрет ввода чисел 
        private void tbSurname_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex r1 = new Regex("^[0-9]+");
            e.Handled = r1.IsMatch(e.Text);
        }
        // запрет ввода чисел 
        private void tbName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex r1 = new Regex("^[0-9]+");
            e.Handled = r1.IsMatch(e.Text);
        }
        // запрет ввода чисел 
        private void tbPatr_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex r1 = new Regex("^[0-9]+");
            e.Handled = r1.IsMatch(e.Text);
        }
        // ввод первой заглавной буквы
        private void tbSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbSurname.Text.Length == 1)
            {
                tbSurname.Text = tbSurname.Text.ToUpper();
                tbSurname.Select(tbSurname.Text.Length, 0);
            }
        }
        // ввод первой заглавной буквы
        private void tbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbName.Text.Length == 1)
            {
                tbName.Text = tbName.Text.ToUpper();
                tbName.Select(tbName.Text.Length, 0);
            }
        }
        // ввод первой заглавной буквы
        private void tbPatr_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbPatr.Text.Length == 1)
            {
                tbPatr.Text = tbPatr.Text.ToUpper();
                tbPatr.Select(tbPatr.Text.Length, 0);
            }
        }
    
        // сохранение 
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (stYes.Visibility == Visibility.Collapsed)
                {
                    // если флаг равен false, то создаем объект для добавления кота
                    if (flagUpdate == false)
                    {
                        entry = new Entry();
                    }


                    List<Connect> ES = BaseClass.tBE.Connect.Where(x => entry.Kod_entry == x.Kod_entry).ToList();
                    int sum = 0;
                    foreach (Connect es in ES)
                    {
                        sum += es.Services.Price;
                    }


                    Entry newEntry = new Entry()
                    {
                        Date = Convert.ToDateTime(dbEntry.SelectedDate),
                        Sum = sum

                    };
                    BaseClass.tBE.Entry.Add(newEntry);
                    BaseClass.tBE.SaveChanges();
                    // если флаг равен false, то добавляем объект в базу
                    if (flagUpdate == false)
                    {
                        BaseClass.tBE.Entry.Add(entry);
                    }

                    Clients newClients = new Clients()
                    {
                        Name = tbName.Text,
                        Surname = tbSurname.Text,
                        Patronymic = tbPatr.Text,
                        Telephone = tbPhone.Text
                    };
                    //BaseClass.tBE.Clients.Add(newClients);
                    //BaseClass.tBE.SaveChanges();
                    if (flagUpdate == false)
                    {
                        BaseClass.tBE.Clients.Add(newClients);
                    }
                    // находим список услуг
                    List<Connect> connect = BaseClass.tBE.Connect.Where(x => entry.Kod_entry == x.Kod_entry).ToList();

                    // если список не пустой, удаляем из него все услуги
                    if (connect.Count > 0)
                    {
                        foreach (Connect t in connect)
                        {
                            BaseClass.tBE.Connect.Remove(t);
                        }
                    }
                    // перезаписываем услуги (или добавляем услуги)
                    foreach (Services t in listServ.SelectedItems)
                    {
                        Connect C = new Connect()  // объект для записи в таблицу 
                        {
                            Kod_entry = entry.Kod_entry,
                            Kod_services = t.Kod_service
                        };
                        BaseClass.tBE.Connect.Add(C);
                    }
                    BaseClass.tBE.SaveChanges();

                    MessageBox.Show("Информация добавлена");
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не по плану");
            }

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

        //вывод телефона при выборе клиентки
        private void cbClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cbClient.SelectedIndex +1;
            List<Clients> ES = BaseClass.tBE.Clients.Where(x => x.Kod_client == index).ToList();
            string str = "";
            foreach (Clients es in ES)
            {
                str += es.Telephone + "\n";
            }
            tbPhon.Text = str.Substring(0, str.Length - 1);
        }
    }
}
 

