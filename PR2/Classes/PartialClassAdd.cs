using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR2
{
    public partial class Clients
    {
        public string FIO //вывод Фамилии + Имя + Отчество в ComboBox
        {
            get
            {
                return Surname + " " + Name + " " + Patronymic + " ";
            }
        }

        public string Phone //вывод Фамилии + Имя + Отчество в ComboBox
        {
            get
            {
                return Telephone;
            }
        }
    }
}
