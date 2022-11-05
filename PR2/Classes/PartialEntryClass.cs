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
        public string Birthday
        {
            get
            {
                return Date.ToString("dd MMMM yyyy года");
            }
        }


        public SolidColorBrush DateColor // если дата записи уже прошла красим
        {
            get
            {
                if (Date < d)
                {
                    return Brushes.Red;
                }
                else
                {
                    return Brushes.LightPink;
                }

            }
        }

    }
}
