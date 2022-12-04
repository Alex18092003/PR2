
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
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


        int n = 0;
        Specialists specialists;
        public object OpenFileDialoge { get; private set; }

        void showImage(byte[] Barray, System.Windows.Controls.Image img)
        {
            BitmapImage BI = new BitmapImage();  // создаем объект для загрузки изображения
            using (MemoryStream m = new MemoryStream(Barray))  // для считывания байтового потока
            {
                BI.BeginInit();  // начинаем считывание
                BI.StreamSource = m;  // задаем источник потока
                BI.CacheOption = BitmapCacheOption.OnLoad;  // переводим изображение
                BI.EndInit();  // заканчиваем считывание
            }
            img.Source = BI;  // показываем картинку на экране (imUser – имя картиник в разметке)
            img.Stretch = Stretch.Uniform;
        }

        public Menu_polzovatel(Specialists specialists)
        {
            InitializeComponent();
            this.specialists = specialists;
            textboxName.Text = specialists.Name;
            textboxSurname.Text = specialists.Surname;
            textboxPatronymic.Text = specialists.Patronymic;
            textboxData.Text = specialists.Date_of_birth.ToString("dd MMMM yyyy");
            textboxDolgnost.Text = specialists.Dolgnosti.Dolgnost;
            textboxPol.Text = specialists.Genders.Gender;

            List<Photo> p = BaseClass.tBE.Photo.Where(x => x.Kod_specialists == specialists.Kod_specialist).ToList();
            if (p.Count != 0)
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
                Gallery.Visibility = Visibility.Collapsed;
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
                MessageBox.Show("Фото добавлено");
                Framec.MainFrame.Navigate(new Menu_polzovatel(specialists));
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так с добавлением нового фото");
            }
        }

        private void buttonFewPhotos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                OpenFileDialog OFD = new OpenFileDialog();  // создаем диалоговое окно
                OFD.Multiselect = true;  // открытие диалогового окна с возможностью выбора нескольких элементов
                if (OFD.ShowDialog() == true)  // пока диалоговое окно открыто, будет в цикле записывать каждое выбранное изображение в БД
                {
                    foreach (string file in OFD.FileNames)  // цикл организован по именам выбранных файлов
                    {
                        Photo p = new Photo();  // создание объекта для добавления записи в таблицу, где хранится фото
                        p.Kod_specialists = specialists.Kod_specialist;  // присваиваем значение полю idUser (id авторизованного пользователя)
                        string path = file;  // считываем путь выбранного изображения
                        System.Drawing.Image SDI = System.Drawing.Image.FromFile(file);  // создаем объект для загрузки изображения в базу
                        ImageConverter IC = new ImageConverter();  // создаем конвертер для перевода картинки в двоичный формат
                        byte[] Barray = (byte[])IC.ConvertTo(SDI, typeof(byte[]));  // создаем байтовый массив для хранения картинки
                        p.PhotoBinary = Barray;  // заполяем поле photoBinary полученным байтовым массивом
                        BaseClass.tBE.Photo.Add(p);  // добавляем объект в таблицу БД
                       
                    }
                    BaseClass.tBE.SaveChanges();
                    MessageBox.Show("Фото добавлены");
                    Framec.MainFrame.Navigate(new Menu_polzovatel(specialists));
                  
                }
               
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так с добавлением нескольких фото");
            }
        }
   

        private void buttonOldPhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Photo> p = BaseClass.tBE.Photo.Where(x => x.Kod_specialists == specialists.Kod_specialist).ToList();
                if (p.Count>0)
                {

                    Gallery.Visibility = Visibility.Visible;
                    byte[] Bar = p[n].PhotoBinary;
                    showImage(Bar, imgUser);
                }
                else
                {
                    MessageBox.Show("В галереи нет фото!\nДобавьте фото");
                }
                //if (p.Count == 1)
                //{
                //    Next.IsEnabled = false;
                   
                //    Next.Visibility = Visibility.Collapsed;

                //}
                //Back.Visibility = Visibility.Collapsed;
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так с изменением фото на старое");
            }
        }

      
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            List<Photo> p = BaseClass.tBE.Photo.Where(x => x.Kod_specialists == specialists.Kod_specialist).ToList();
            n++;
            if (p != null)
            {
                if(p.Count == 1)
                {
                    n = p.Count;
                }
                else
                {
                    byte[] Bar = p[n].PhotoBinary;
                    showImage(Bar, imgUser);
                }

            }
            if (n == p.Count - 1)
            {
                n = -1;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            List<Photo> p = BaseClass.tBE.Photo.Where(x => x.Kod_specialists == specialists.Kod_specialist).ToList();
            if(p != null)
            {
                if(n == 0)
                {
                    n = p.Count;
                }
                if(n == -1)
                {
                    n = p.Count - 1;
                }
                n--;
                byte[] Bar = p[n].PhotoBinary;   // считываем изображение из базы (считываем байтовый массив двоичных данных)
                BitmapImage BI = new BitmapImage();                                  //    BitmapImage BI = new BitmapImage();  // создаем объект для загрузки изображения
                showImage(Bar, imgUser);                                                                //    showImage(Bar, imgUser);
            }
           
        }



        private void buttonPassword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Pages.WindowPassword windowPassword = new Pages.WindowPassword(specialists); // создание объекта окна
                windowPassword.ShowDialog();// октрытие созданного окна (дальнейший код не будет запущен, пока окно не будет закрыто)
              
                Framec.MainFrame.Navigate(new Menu_polzovatel(specialists));// перезагрузка страницы
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonSelected_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Photo> p = BaseClass.tBE.Photo.Where(x => x.Kod_specialists == specialists.Kod_specialist).ToList();
                byte[] Bar = p[n].PhotoBinary;   // считываем изображение из базы (считываем байтовый массив двоичных данных)
                showImage(Bar, imUser);
                Gallery.Visibility = Visibility.Collapsed;
                MessageBox.Show("Фото изменено");// отображаем картинку
            }
            catch
            {
                MessageBox.Show("Данное фото уже стоит");
            }

        }

        private void buttonDelet_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                List<Photo> p = BaseClass.tBE.Photo.Where(x => x.Kod_specialists == specialists.Kod_specialist).ToList();
                BaseClass.tBE.Photo.Remove(p[n]);
                BaseClass.tBE.SaveChanges();
                Gallery.Visibility = Visibility.Collapsed;
                //buttonOldPhoto_Click(sender, e);
                Framec.MainFrame.Navigate(new Menu_polzovatel(specialists));
                MessageBox.Show("Фото удалено");
            }
            catch
            {
                MessageBox.Show("Нельзя удалить данное фото");
            }
        }
    }
}
