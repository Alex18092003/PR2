using PR2.Pages;
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
        Specialists sp;
        List<Entry> listFilter = new List<Entry>();

        PageChange pc = new PageChange(); // создаем объект класса для отображения страниц

        public EntryPage1()
        {
            InitializeComponent();

            listEntry.ItemsSource = BaseClass.tBE.Entry.ToList();

            

            List<Clients> clients = BaseClass.tBE.Clients.ToList();
            // программное заполнение выпадающего списка
            cbFiltr.Items.Add("Все телефоны");  // первый элемент выпадающего списка, который сбрасывает фильтрацию
            for (int i = 0; i < clients.Count; i++)  // цикл для записи в выпадающий список всех пород котов из БД
            {
                cbFiltr.Items.Add(clients[i].Telephone);
            }

            cbFiltr.SelectedIndex = 0;  // выбранное по умолчанию значение в списке с породами котов (""Все услуги")
            cbSort.SelectedIndex = 0;  // выбранное по умолчанию значение в списке с видами сортировки ("Без сортировки")

            textCount.Text = "Общее число записей: " + BaseClass.tBE.Entry.ToList().Count;
            
            pc.CountPage = BaseClass.tBE.Entry.ToList().Count;
            DataContext = pc;  // добавляем объект для отображения страниц в ресурсы страницы

        }

        private void btnBack_Click(object sender, RoutedEventArgs e) // метод для выхода в меню админа
        {
            Framec.MainFrame.Navigate(new Menu_admin(sp));
        }

        private void tbServices_Loaded(object sender, RoutedEventArgs e) // медол для вывода оказываемых услуг
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

        private void tbPrice_Loaded(object sender, RoutedEventArgs e) // метод для подсчета суммы стоимости услуг
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

        private void btnAdd_Click(object sender, RoutedEventArgs e) // метод для перехода на страницу добавления записи
        {
            Framec.MainFrame.Navigate(new AddEntryPage());
        }

        private void buttonDelet_Click(object sender, RoutedEventArgs e) // метод для удаления записи
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            Entry entry = BaseClass.tBE.Entry.FirstOrDefault(x => x.Kod_entry == index);
            BaseClass.tBE.Entry.Remove(entry);
            BaseClass.tBE.SaveChanges();
            MessageBox.Show("Запись удалена");
            Framec.MainFrame.Navigate(new EntryPage1());
        }

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            Entry entry = BaseClass.tBE.Entry.FirstOrDefault(x => x.Kod_entry == index);
            Framec.MainFrame.Navigate(new AddEntryPage(entry));
        }
        private int Clien(string Phone)
        {
           
            List<Clients> clients = BaseClass.tBE.Clients.ToList();
            foreach(Clients clients1 in clients)
            {
             if(clients1.Telephone == Phone)
                {
                    return clients1.Kod_client;
                }
            }
            return 0;
        }

        void Filter()
        {
            List<Entry> entries = BaseClass.tBE.Entry.ToList();  // пустой список, который далее будет заполнять элементами, удавлетворяющими условиям фильтрации, поиска и сортировки

            string phone = cbFiltr.SelectedValue.ToString(); // выбранное пользователем название услуги
            int index = cbFiltr.SelectedIndex;

            List<Clients> clients = BaseClass.tBE.Clients.Where(z => z.Kod_client == index).ToList();
            List<Clients> cl = BaseClass.tBE.Clients.Where(z => z.Telephone == phone).ToList();
            // MessageBox.Show($"{index}");
            // поиск значений, удовлетворяющих условия фильтра

            if (index != 0)
            {
               listFilter = new List<Entry>();
                //listFilter = BaseClass.tBE.Entry.Where(x => x.Clients.Kod_client == index).ToList();
                
                    foreach (Clients e in cl)
                    {
                    foreach (Entry et in entries)
                    {
                        if (et.Kod_client == e.Kod_client)
                        {
                           // listFilter.Add(et);
                            listFilter = BaseClass.tBE.Entry.Where(x => x.Clients.Kod_client == index).ToList();
                        }
                    }

                    }

            }
            else  // если выбран пункт "Все телефоны", то сбрасываем фильтрацию:
            {
                listFilter = BaseClass.tBE.Entry.ToList();
            }

            // поиск совпадений по фамилиям
            if (!string.IsNullOrWhiteSpace(tbFam.Text))  // если строка не пустая и если она не состоит из пробелов
            {
              
                listFilter = listFilter.Where(x => x.Clients.Surname.ToLower().Contains(tbFam.Text.ToLower())).ToList();
            }

            // выбор  Предстоящие записи
            if (checkboxData.IsChecked == true)
            {
              
                DateTime d = DateTime.Now;
                listFilter = listFilter.Where(x => x.Date > d).ToList();
            }



            //// сортировка
            switch (cbSort.SelectedIndex)
            {
                case 1:
                   
                        listFilter.Sort((x, y) => x.Sum.CompareTo(y.Sum));
      
                    break;
                case 2:
                    
                        listFilter.Sort((x, y) => x.Sum.CompareTo(y.Sum));
                        listFilter.Reverse();
                    
                    break;
                case 3:
                    
                        listFilter.Sort((x, y) => x.Clients.Name.CompareTo(y.Clients.Name));
                       
                    
                    break;
                case 4:
                    
                        listFilter.Sort((x, y) => x.Clients.Name.CompareTo(y.Clients.Name));
                        listFilter.Reverse();
                    
                    break;
                case 5:
                    
                        listFilter.Sort((x, y) => x.Clients.Surname.CompareTo(y.Clients.Surname));
                       
                    
                    break;
                case 6:
                    
                        listFilter.Sort((x, y) => x.Clients.Surname.CompareTo(y.Clients.Surname));
                        listFilter.Reverse();
                    
                    break;
            }

            listEntry.ItemsSource = listFilter;
            if (listFilter.Count == 0)
            {
                MessageBox.Show("нет записей");
            }
            textCount.Text = "Количество записей: " + listFilter.Count;

        }

        // далее во всех обработчиках событий применяем один и тот же метод Filter, который позволяет находить условия, удовлетворяющие всем сразу выбранным параметрам
     

        private void checkboxData_Checked(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void tbFam_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cbFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void textCount_TextChanged(object sender, TextChangedEventArgs e)
        {

            try
            {
                pc.CountPage = Convert.ToInt32(textCountt.Text); // если в текстовом поле есnь значение, присваиваем его свойству объекта, которое хранит количество записей на странице
            }
            catch
            {
                pc.CountPage = listFilter.Count; // если в текстовом поле значения нет, присваиваем свойству объекта, которое хранит количество записей на странице количество элементов в списке
            }
            pc.Countlist = listFilter.Count;  // присваиваем новое значение свойству, которое в объекте отвечает за общее количество записей
            listEntry.ItemsSource = listFilter.Skip(0).Take(pc.CountPage).ToList();  // отображаем первые записи в том количестве, которое равно CountPage
            pc.CurrentPage = 1; // текущая страница - это страница 1

        }

        private void GoPage_MouseDown(object sender, MouseButtonEventArgs e)  // обработка нажатия на один из Textblock в меню с номерами страниц
        {
            TextBlock tb = (TextBlock)sender;

            switch (tb.Uid)  // определяем, куда конкретно было сделано нажатие
            {
                case "prev":
                    pc.CurrentPage--;
                    break;
                case "next":
                    pc.CurrentPage++;
                    break;
                default:
                    pc.CurrentPage = Convert.ToInt32(tb.Text);
                    break;
            }
            listEntry.ItemsSource = listFilter.Skip(pc.CurrentPage * pc.CountPage - pc.CountPage).Take(pc.CountPage).ToList();  // оображение записей постранично с определенным количеством на каждой странице
           
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            pc.CurrentPage = 1;

            try
            {
                pc.CountPage = Convert.ToInt32(textCountt.Text); // если в текстовом поле есnь значение, присваиваем его свойству объекта, которое хранит количество записей на странице
            }
            catch
            {
                pc.CountPage = listFilter.Count; // если в текстовом поле значения нет, присваиваем свойству объекта, которое хранит количество записей на странице количество элементов в списке
            }
            pc.Countlist = listFilter.Count;  // присваиваем новое значение свойству, которое в объекте отвечает за общее количество записей
            listEntry.ItemsSource = listFilter.Skip(0).Take(pc.CountPage).ToList();  // отображаем первые записи в том количестве, которое равно CountPage
        }

        // запрет ввода символов
        private void textCountt_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void txtNext_MouseDown(object sender, MouseButtonEventArgs e)
        {
            pc.CurrentPage = 1;
            listEntry.ItemsSource = listFilter.Skip(pc.CurrentPage * pc.CountPage - pc.CountPage).Take(pc.CountPage).ToList();
        }

        private void txtNextt2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            pc.CurrentPage = pc.CountPages;
            listEntry.ItemsSource = listFilter.Skip(pc.CurrentPage * pc.CountPage - pc.CountPage).Take(pc.CountPage).ToList();
        }
    }
}
