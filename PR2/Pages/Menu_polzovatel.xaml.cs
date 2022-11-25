
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

        void showImage(byte[] Barray, System.Windows.Controls.Image image)
        {
            BitmapImage BI = new BitmapImage();
          using (MemoryStream m = new MemoryStream(Barray))
            {
                BI.BeginInit();
                BI.StreamSource = m;
                BI.CacheOption = BitmapCacheOption.OnLoad;
                BI.EndInit();
            }
          image.Source = BI;
          image.Stretch = Stretch.Uniform;

        }
        public Menu_polzovatel(Specialists specialists)
        {
            
            InitializeComponent();
            this.specialists = specialists;
            textboxName.Text = specialists.Name;
            textboxSurname.Text = specialists.Surname;
            textboxPatronymic.Text = specialists.Patronymic;
            List<Photo> p = BaseClass.tBE.Photo.Where(x => x.Kod_specialists == specialists.Kod_specialist).ToList();
            if (p != null)
            {
                byte[] Bar = p[p.Count - 1].PhotoBinary;
                showImage(Bar, imUser);
            }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Framec.MainFrame.Navigate(new Authorizat());
        }
        // открытие окна для редактирования личных данных
        private void buttonChange_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Pages.WindowPerson windowPerson = new Pages.WindowPerson(specialists); // создание объекта окна
                windowPerson.ShowDialog();// октрытие созданного окна (дальнейший код не будет запущен, пока окно не будет закрыто)
                MessageBox.Show("Данные изменены"); // сообщение об успешном изменение данных
                Framec.MainFrame.Navigate(new Menu_polzovatel(specialists));// перезагрузка страницы
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonNewPhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Photo p = new Photo(); // создание объекта для добавления записи в таблицу, где хранится фото
                p.Kod_specialists = specialists.Kod_specialist;  // присваиваем значение полю Kod_specialist (id авторизованного пользователя)

                OpenFileDialog OFD = new OpenFileDialog();// создаем диалоговое окно
                OFD.ShowDialog();// открываем диалоговое окно 
                string path = OFD.FileName;// считываем путь выбранного изображения
                System.Drawing.Image SDI = System.Drawing.Image.FromFile(path);// создаем объект для загрузки изображения в базу
                ImageConverter IC = new ImageConverter(); // создаем конвертер для перевода картинки в двоичный формат
                byte[] Barray = (byte[])IC.ConvertTo(SDI, typeof(byte[]));// создаем байтовый массив для хранения картинки
                p.PhotoBinary = Barray;// заполяем поле photoBinary полученным байтовым массивом
                BaseClass.tBE.Photo.Add(p);// добавляем объект в таблицу БД
                BaseClass.tBE.SaveChanges();  // созраняем изменения в БД
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonFewPhotos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonOldPhoto_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
