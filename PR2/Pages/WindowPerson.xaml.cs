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
using System.Windows.Shapes;

namespace PR2.Pages
{
    /// <summary>
    /// Логика взаимодействия для WindowPerson.xaml
    /// </summary>
    public partial class WindowPerson : Window
    {
        List<Dolgnosti> DL = BaseClass.tBE.Dolgnosti.ToList();
        Specialists specialists;
        public WindowPerson(Specialists specialists)
        {
            InitializeComponent();
            this.specialists = specialists;
            textboxName.Text = specialists.Name;
            textboxSurname.Text = specialists.Surname;
            textboxPatronymic.Text = specialists.Patronymic;
            dpBirthday.Text = specialists.Date_of_birth.ToString();

            cbPol.ItemsSource = BaseClass.tBE.Genders.ToList();
            cbPol.SelectedValuePath = "Kod_gendera";
            cbPol.DisplayMemberPath = "Gender";
            cbPol.SelectedValue = specialists.Kod_pola;

           

        }
       
        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (textboxName.Text != "" && textboxSurname.Text != "" && textboxPatronymic.Text != ""  && cbPol.SelectedItem != null && dpBirthday.SelectedDate != null)
            {
                specialists.Name = textboxName.Text;
                specialists.Surname = textboxSurname.Text;
                specialists.Patronymic = textboxPatronymic.Text;
                specialists.Date_of_birth = Convert.ToDateTime(dpBirthday.SelectedDate);
                specialists.Kod_pola = (int)cbPol.SelectedValue;
                specialists.Kod_dolgnosti = specialists.Kod_dolgnosti;

                BaseClass.tBE.SaveChanges();
                MessageBox.Show("Данные изменены"); // сообщение об успешном изменение данных
                this.Close();// закрываем это окно
            }
            else
            {
                MessageBox.Show($"Не все поля заполнены");
            }
        }
        // ввод первой заглавной буквы
        private void textboxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (textboxName.Text.Length == 1)
                {
                    textboxName.Text = textboxName.Text.ToUpper();
                    textboxName.Select(textboxName.Text.Length, 0);
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так с авто заглавными буквами отчества");
            }
        }
        // ввод первой заглавной буквы
        private void textboxSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (textboxSurname.Text.Length == 1)
                {
                    textboxSurname.Text = textboxSurname.Text.ToUpper();
                    textboxSurname.Select(textboxSurname.Text.Length, 0);
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так с авто заглавными буквами отчества");
            }
        }
        // ввод первой заглавной буквы
        private void textboxPatronymic_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (textboxPatronymic.Text.Length == 1)
                {
                    textboxPatronymic.Text = textboxPatronymic.Text.ToUpper();
                    textboxPatronymic.Select(textboxPatronymic.Text.Length, 0);
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так с авто заглавными буквами отчества");
            }
        }
        // запрет ввода чисел
        private void textboxPatronymic_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
        private void textboxSurname_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
        private void textboxName_PreviewTextInput(object sender, TextCompositionEventArgs e)
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


    }
}
