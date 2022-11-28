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
    /// Логика взаимодействия для WindowPerson.xaml
    /// </summary>
    public partial class WindowPerson : Window
    {
        Specialists specialists;
        public WindowPerson(Specialists specialists)
        {
            InitializeComponent();
            this.specialists = specialists;
            textboxName.Text = specialists.Name;
            textboxSurname.Text = specialists.Surname;
            textboxPatronymic.Text = specialists.Patronymic;
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            specialists.Name = textboxName.Text;
            specialists.Surname = textboxSurname.Text;
            specialists.Patronymic = textboxPatronymic.Text;
            BaseClass.tBE.SaveChanges();
            this.Close();// закрываем это окно
        }
    }
}
