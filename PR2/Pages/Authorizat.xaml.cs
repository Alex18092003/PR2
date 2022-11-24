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
    /// Логика взаимодействия для Authorizat.xaml
    /// </summary>
    public partial class Authorizat : Page
    {
        public Authorizat()
        {
            InitializeComponent();
        }

        
        //админ = логин - admin, пароль - admin
        // пользователи = логин - ssss , пароль - Sasha_420
        private void btnAvtoriz_Click(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text != "" && tbPassword.Password != "")
            {
                int p = tbPassword.Password.GetHashCode();
                Specialists specialists = BaseClass.tBE.Specialists.FirstOrDefault(x => x.Login == tbLogin.Text && x.Password == p);
                Specialists adm = BaseClass.tBE.Specialists.FirstOrDefault(x => x.Kod_dolgnosti == 1);

                if (specialists != null)
                {
                    if (specialists.Kod_dolgnosti == 1)
                    {
                        Framec.MainFrame.Navigate(new Menu_admin());
                    }
                    else
                    {
                        Framec.MainFrame.Navigate(new Menu_polzovatel(specialists));
                    }

                }
                else
                {

                    MessageBox.Show("Данный пользователь не зарегистрирован\nВведите верный логин и пароль");
                    tbLogin.Text = "";
                    tbPassword.Password = "";
                }
            }
            else
            {
                MessageBox.Show($"Не все поля заполнены");
            }
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            Framec.MainFrame.Navigate(new Registration());
        }
    }
}
