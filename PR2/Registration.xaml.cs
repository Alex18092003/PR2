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

        public void dolgnosti()
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
            
            try { 
                    int g=0;
            if (rbGen.IsChecked == true)
                g = 1;
            if (rbMyg.IsChecked == true)
                g = 2;
            
   
            Specialists specialists = new Specialists()
            {
                Name = tbName.Text,
                Surname = tbFamil.Text,
                Patronymic = tbPatr.Text,
                Kod_pola = g,
                Kod_dolgnosti = cbDolgn.SelectedIndex+1,
                Login = tbLogin.Text,
                Password = tbPassword.Password.GetHashCode(),
                Date_of_birth = Convert.ToDateTime(dpBirthday.SelectedDate)
            };
            BaseClass.tBE.Specialists.Add(specialists);
            BaseClass.tBE.SaveChanges();

         
                MessageBox.Show("Пользователь добавлен успешно");
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
    }
}
