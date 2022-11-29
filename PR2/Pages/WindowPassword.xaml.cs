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

            //int p = tbPassword.Password.GetHashCode();
          
            int p = specialists.Password.GetHashCode();
            textPassword.Text = Convert.ToString(p);
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (textLogin.Text != "" && textPassword.Text != "")
            {
                specialists.Login = textLogin.Text;

                specialists.Password = textPassword.Text.GetHashCode();
                BaseClass.tBE.SaveChanges();
                this.Close();// закрываем это окно
                MessageBox.Show("Данные изменены"); // сообщение об успешном изменение данных
            }
            else
            {
                MessageBox.Show($"Не все поля заполнены");
            }
        }
    }
}
