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
        bool YesNo = false; // для определения, на запись мы записываем нового клиента или уже записанного в базу
        public void uploadFields()  // метод для заполнения списков
        {
            try
            {
                listServ.ItemsSource = BaseClass.tBE.Services.ToList();
                //listServ.SelectedValuePath = "Kod_service";
                //listServ.DisplayMemberPath = "Service";


                cbClient.ItemsSource = BaseClass.tBE.Clients.ToList();
                cbClient.SelectedValuePath = "Kod_client";
                cbClient.DisplayMemberPath = "FIO";

                cbClient.SelectionChanged += cbClient_SelectedIndexChanged;
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так с заполнением полей");
            }
        }
        // конструктор с аргументом для редактирования записи
        public AddEntryPage(Entry entry)
        {
            InitializeComponent();
            uploadFields();
            radioButtonVisibility.Visibility = Visibility.Collapsed;
            stYes.Visibility = Visibility.Visible;
            dbEntry.SelectedDate = entry.Date;
            tbName.Text = entry.Clients.Name;
            tbPatr.Text = entry.Clients.Surname;
            tbSurname.Text = entry.Clients.Patronymic;
            tbPhone.Text = entry.Clients.Phone;
            List<Connect> CS = BaseClass.tBE.Connect.Where(x => x.Kod_entry == entry.Kod_entry).ToList();
            foreach (Services t in listServ.Items)
            {
                if(CS.FirstOrDefault(x=> x.Kod_services == t.Kod_service) != null)
                {
                    listServ.SelectedItems.Add(t);
                }
            }

        }
        // конструктор для создания новой записи (без аргументов)
        public AddEntryPage()
        {
         
            InitializeComponent();
            radioButtonVisibility.Visibility = Visibility.Visible;
            uploadFields();
        }
        // назад
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Framec.MainFrame.Navigate(new EntryPage1());
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так с навигацией");
            }
        }
        // запрет ввода чисел 
        private void tbSurname_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                Regex r1 = new Regex("^[0-9]+");
                e.Handled = r1.IsMatch(e.Text);
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так с запретом ввода чисел");
            }
        }
        // запрет ввода чисел 
        private void tbName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                Regex r1 = new Regex("^[0-9]+");
            e.Handled = r1.IsMatch(e.Text);
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так с запретом ввода чисел");
            }
        }
        // запрет ввода чисел 
        private void tbPatr_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                Regex r1 = new Regex("^[0-9]+");
            e.Handled = r1.IsMatch(e.Text);
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так с запретом ввода чисел");
            }
        }
        // ввод первой заглавной буквы
        private void tbSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (tbSurname.Text.Length == 1)
            {
                tbSurname.Text = tbSurname.Text.ToUpper();
                tbSurname.Select(tbSurname.Text.Length, 0);
            }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так с авто заглавными буквами фамилии");
            }
        }
        // ввод первой заглавной буквы
        private void tbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (tbName.Text.Length == 1)
            {
                tbName.Text = tbName.Text.ToUpper();
                tbName.Select(tbName.Text.Length, 0);
            }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так с авто заглавными буквами имени");
            }
        }
        // ввод первой заглавной буквы
        private void tbPatr_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (tbPatr.Text.Length == 1)
                {
                    tbPatr.Text = tbPatr.Text.ToUpper();
                    tbPatr.Select(tbPatr.Text.Length, 0);
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так с авто заглавными буквами отчества");
            }
        }

        // сохранение 
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                if (YesNo == false) // если клиент впервые в салоне 
                {
                    if (tbSurname.Text != "" && tbName.Text != "" && tbPatr.Text != "" && tbPhone.Text != "" && listServ.SelectedItem != null && dbEntry.SelectedDate != null)
                    {
                        // если флаг равен false, то создаем объект для добавления кота
                        if (flagUpdate == false)
                        {
                            entry = new Entry();
                        }

                        List<Connect> ES = BaseClass.tBE.Connect.Where(x => entry.Kod_entry == x.Kod_entry).ToList();
                        int sum = 0;

                        foreach (Services t in listServ.SelectedItems)
                        {
                            sum += t.Price;
                        }

                        // заполняем поля таблицы entry
                        entry.Date = Convert.ToDateTime(dbEntry.SelectedDate);
                        entry.Sum = sum;

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
                    else
                    {
                        MessageBox.Show("Не все поля заполнены");
                    }
                }
                else // если клиент уже приходит повторно на запись
                {
                    if (listServ.SelectedItem != null  && cbClient.SelectedItem != null && dbEntry.SelectedDate != null)
                    {
                        // если флаг равен false, то создаем объект для добавления кота
                        if (flagUpdate == false)
                        {
                            entry = new Entry();
                        }

                        List<Connect> ES = BaseClass.tBE.Connect.Where(x => entry.Kod_entry == x.Kod_entry).ToList();
                        int sum = 0;

                        foreach (Services t in listServ.SelectedItems)
                        {
                            sum += t.Price;
                        }

                        // заполняем поля таблицы entry
                        entry.Kod_client = cbClient.SelectedIndex + 1;
                        entry.Date = Convert.ToDateTime(dbEntry.SelectedDate);
                        entry.Sum = sum;

                        // если флаг равен false, то добавляем объект в базу
                        if (flagUpdate == false)
                        {
                            BaseClass.tBE.Entry.Add(entry);
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
                    else
                    {
                        MessageBox.Show("Не все поля заполнены");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не по плану");
            }

        }

        private void Yes_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                stYes.Visibility = Visibility.Visible;
                stNo.Visibility = Visibility.Collapsed;
                YesNo = false;
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так с отображением");
            }
        }
        
        private void No_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                YesNo = true;
                stYes.Visibility = Visibility.Collapsed;
                stNo.Visibility = Visibility.Visible;
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так с отображением");
            }
        }

        //вывод телефона при выборе клиентки
        private void cbClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = cbClient.SelectedIndex + 1;
                List<Clients> ES = BaseClass.tBE.Clients.Where(x => x.Kod_client == index).ToList();
                string str = "";
                foreach (Clients es in ES)
                {
                    str += es.Telephone + "\n";
                }
                tbPhon.Text = str.Substring(0, str.Length - 1);
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так с выводом телефона");
            }
        }

        private void tbPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так с запретом ввода символов");
            }

        }
    }
}
 

