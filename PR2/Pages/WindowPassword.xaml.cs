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
    /// Логика взаимодействия для WindowPassword.xaml
    /// </summary>
    public partial class WindowPassword : Window
    {
        Specialists specialists;
        public WindowPassword(Specialists specialists)
        {
            InitializeComponent();
            this.specialists = specialists;
            textLogin.Text = specialists.Login;

        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (textLogin.Text != "")
            {
                int p = textPassword.Password.GetHashCode();
                Specialists sp = BaseClass.tBE.Specialists.FirstOrDefault(x => x.Login == specialists.Login && x.Password == p);
                if(sp != null)
                {
                    if (textPasswordNew.Password.ToString() != "")
                    {
                        Regex r1 = new Regex("(?=.*[A-Z])");
                        Regex r2 = new Regex("[a-z].*[a-z].*[a-z]");
                        Regex r3 = new Regex("\\d.*\\d");
                        Regex r4 = new Regex("[!@#№?$%^&*()_+=]");
                        Regex r5 = new Regex(".{8,}");
                        if (r1.IsMatch(textPasswordNew.Password.ToString()) == false)
                        {
                            MessageBox.Show("Новый пароль должен содержать не менее 1 заглавного латинского символа");
                            return;
                        }
                        if (r2.IsMatch(textPasswordNew.Password.ToString()) == false)
                        {
                            MessageBox.Show("Новый пароль должен содержать не менее 3 строчных латинских символов");
                            return;
                        }
                        if (r3.IsMatch(textPasswordNew.Password.ToString()) == false)
                        {
                            MessageBox.Show("Новый пароль должен содержать не менее 2 цифр");
                            return;
                        }
                        if (r4.IsMatch(textPasswordNew.Password.ToString()) == false)
                        {
                            MessageBox.Show("Новый пароль должен содержать не менее 1 спец. символа");
                            return;
                        }
                        if (r5.IsMatch(textPasswordNew.Password.ToString()) == false)
                        {
                            MessageBox.Show("Общая длина нового пароля должна быть не менее 8 символов");
                            return;
                        }
                        if (textPasswordNew.Password.ToString() != textPasswordNew2.Password.ToString())
                        {
                            MessageBox.Show("Новый пароль не совпадает");
                            return;
                        }
                        specialists.Password = textPasswordNew.Password.GetHashCode();
                    }
                    specialists.Login = textLogin.Text;
                    BaseClass.tBE.SaveChanges();
                    this.Close();
                    MessageBox.Show("Данные изменены");
                } 
            }
            else
            {
                MessageBox.Show($"Не все поля заполнены");
            }
        }

    }
}
