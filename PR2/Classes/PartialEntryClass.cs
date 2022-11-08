using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;



namespace PR2
{
    
    public partial class Entry
    {
        DateTime d = DateTime.Now;
        public string Birthday // отображение даты
        {
            get
            {
                return Date.ToString("dd MMMM yyyy года");
            }
        }

        public string Surn//фамилия
        {
            get
            {
                return "Фамилия: " + Clients.Surname;
            }
        }


        public SolidColorBrush DateColor // если дата записи уже прошла то красим в прозрачный + небольшой цвет из StackPanel, иначе красим так же в более темный - значит дата записи актуальна и ещё не прошла
        {
            get
            {
                if (Date < d)
                {
                    return Brushes.Transparent;
                }
                else
                {
                    
                    return Brushes.Gray;
                }

            }
        }

    }
}
