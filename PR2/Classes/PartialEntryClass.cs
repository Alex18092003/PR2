using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PR2
{
    DateTime d = DateTime.Now;
    public partial class Entry
    {

        public string Birthday
        {
            get
            {
                return Date.ToString("dd MMMM yyyy года");
            }
        }

        public SolidColorBrush DateColor
        {
            get
            {


            }
        }

    }
}
