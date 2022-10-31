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
using System.Text.RegularExpressions;

namespace PR2
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        List<Dolgnosti> DL = BaseClass.tBE.Dolgnosti.ToList();
        public Registration()
        {
            InitializeComponent();
            dolgnosti();
           
        }

        private void dolgnosti()
        {
            string dol;
            for (int i = 1; i < DL.Count; i++)
            {
                dol = DL[i].Dolgnost;
                cbDolgn.Items.Add(dol);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Framec.MainFrame.Navigate(new Authorizat());
        }

        //админ = логин - admin, пароль - admin
        private void btnZareg_Click(object sender, RoutedEventArgs e)
        {
            int g = 0;
            if (rbGen.IsChecked == true)
                g = 1;
            if (rbMyg.IsChecked == true)
                g = 2;

            Regex r1 = new Regex("(?=.*[A-Z])");
            Regex r2 = new Regex("[a-z].*[a-z].*[a-z]");
            Regex r3 = new Regex("\\d.*\\d");
            Regex r4 = new Regex("[!@#№?$%^&*()_+=]");

            if (tbName.Text != "" && cbDolgn.SelectedItem != null && tbFamil.Text != "" && tbPatr.Text != "" && (rbGen.IsChecked != false || rbMyg.IsChecked != false) && tbLogin.Text != "" && tbPassword.Password != "" && dpBirthday.SelectedDate != null)
            {
                Specialists specialists1 = BaseClass.tBE.Specialists.FirstOrDefault(x=> x.Login == tbLogin.Text);
                if (specialists1 == null)
                {
                    if (r1.IsMatch(tbPassword.Password) == true)
                    {
                        if (r2.IsMatch(tbPassword.Password) == true)
                        {
                            if (r3.IsMatch(tbPassword.Password) == true)
                            {
                                if (r4.IsMatch(tbPassword.Password) == true)
                                {
                                    if (tbPassword.Password.Length >= 8)
                                    {
                                        try
                                        {

                                            Specialists specialists = new Specialists()
                                            {
                                                Name = tbName.Text,
                                                Surname = tbFamil.Text,
                                                Patronymic = tbPatr.Text,
                                                Kod_pola = g,
                                                Kod_dolgnosti = cbDolgn.SelectedIndex + 2,
                                                Login = tbLogin.Text,
                                                Password = tbPassword.Password.GetHashCode(),
                                                Date_of_birth = Convert.ToDateTime(dpBirthday.SelectedDate)
                                            };
                                            BaseClass.tBE.Specialists.Add(specialists);
                                            BaseClass.tBE.SaveChanges();


                                            MessageBox.Show("Успешная регистрация");
                                            tbName.Text = "";
                                            tbFamil.Text = "";
                                            tbPatr.Text = "";
                                            rbGen.IsChecked = false;
                                            rbMyg.IsChecked = false;
                                            tbLogin.Text = "";
                                            cbDolgn.Items.Clear();
                                            dolgnosti();
                                            tbPassword.Password = "";
                                            dpBirthday.SelectedDate = null;
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show($"Введены некорректные данные");
                                        }
                                    }

                                    else
                                    {
                                        MessageBox.Show($"Пароль должен быть не менее 8 символов");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show($"Пароль должен содержать не менее 1 специального символа");
                                }
                            }
                            else
                            {
                                MessageBox.Show($"Пароль должен содержать не менее 2 цифры");
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Пароль должен содержать не менее 3 строчных латинских символов");
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Пароль должен содержать не менее 1 заглавного латинского символа");
                    }
                }
                else
                {
                    MessageBox.Show($"Данный логин уже занят");
                }
            }
        else 
        {
                MessageBox.Show($"Не все данные заполнены");
        }
        }

        private void tbName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex r1 = new Regex("^[0-9]+");
            e.Handled = r1.IsMatch(e.Text);
        }

        private void tbFamil_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex r1 = new Regex("^[0-9]+");
            e.Handled = r1.IsMatch(e.Text);
        }

        private void tbPatr_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex r1 = new Regex("^[0-9]+");
            e.Handled = r1.IsMatch(e.Text);
        }

        private void tbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbName.Text.Length == 1)
            {
                tbName.Text = tbName.Text.ToUpper();
                tbName.Select(tbName.Text.Length, 0);
            
            }
        }

        private void tbFamil_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbFamil.Text.Length == 1)
            {
                tbFamil.Text = tbFamil.Text.ToUpper();
                tbFamil.Select(tbFamil.Text.Length, 0);

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
    }
}
