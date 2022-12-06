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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PR2.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAdvertising.xaml
    /// </summary>
    public partial class PageAdvertising : Page
    {
        public PageAdvertising()
        {
            InitializeComponent();
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 10;
            doubleAnimation.To = 300; // конечное значение свойства
            doubleAnimation.Duration = TimeSpan.FromSeconds(5); // продолжительность анимации (в секундах)
            doubleAnimation.RepeatBehavior = RepeatBehavior.Forever; // бесконечность анимации
            doubleAnimation.AutoReverse = true; // воспроизведение временной шкалы в обратном порядке
            buttonZapis.BeginAnimation(WidthProperty, doubleAnimation); // «навешивание» анимации на свойство ширины кнопки

            DoubleAnimation doubleAnimationHeight = new DoubleAnimation();
            doubleAnimationHeight.From = 2;
            doubleAnimationHeight.To = 40;
            doubleAnimationHeight.Duration = TimeSpan.FromSeconds(2);
            doubleAnimationHeight.RepeatBehavior = RepeatBehavior.Forever;
            doubleAnimationHeight.AutoReverse = true;
            buttonZapis.BeginAnimation(HeightProperty, doubleAnimationHeight);

            ThicknessAnimation MA = new ThicknessAnimation(); // анимация границ
            MA.From = new Thickness(0, 0, 0, 0);
            MA.To = new Thickness(50, 50, 50, 50);
            MA.Duration = TimeSpan.FromSeconds(2);
            MA.AutoReverse = true;
            buttonZapis.BeginAnimation(MarginProperty, MA);

            ColorAnimation BA = new ColorAnimation();
            Color Cstart = Color.FromRgb(191, 15, 116); // присваивание начального цвета эл-ту
            buttonZapis.Background = new SolidColorBrush(Cstart);
            BA.From = Cstart;
            BA.To = Color.FromRgb(197, 9, 14);
            BA.Duration = TimeSpan.FromSeconds(5);
            BA.RepeatBehavior = RepeatBehavior.Forever;
            BA.AutoReverse = true;
            buttonZapis.Background.BeginAnimation(SolidColorBrush.ColorProperty, BA);


            //DoubleAnimation text = new DoubleAnimation();
            //text.From = 10;
            //text.To = 200; // конечное значение свойства
            //text.Duration = TimeSpan.FromSeconds(5); // продолжительность анимации (в секундах)
            //text.RepeatBehavior = RepeatBehavior.Forever; // бесконечность анимации
            //text.AutoReverse = true; // воспроизведение временной шкалы в обратном порядке
            //stock.BeginAnimation(WidthProperty, text);


            DoubleAnimation text2 = new DoubleAnimation();
            text2.From = 14;
            text2.To = 30;
            text2.Duration = TimeSpan.FromSeconds(2);
            text2.RepeatBehavior = RepeatBehavior.Forever;
            text2.AutoReverse = true;
            stock.BeginAnimation(FontSizeProperty, text2);

            DoubleAnimation doubleAnimationImage = new DoubleAnimation();
            doubleAnimationImage.From = 50;
            doubleAnimationImage.To = 170; // конечное значение свойства
            doubleAnimationImage.Duration = TimeSpan.FromSeconds(3); // продолжительность анимации (в секундах)
            doubleAnimationImage.RepeatBehavior = RepeatBehavior.Forever; // бесконечность анимации
            doubleAnimationImage.AutoReverse = true; // воспроизведение временной шкалы в обратном порядке
            img.BeginAnimation(WidthProperty, doubleAnimationImage); // «навешивание» анимации на свойство ширины кнопки

            DoubleAnimation doubleAnimationHeightImage = new DoubleAnimation();
            doubleAnimationHeightImage.From = 100;
            doubleAnimationHeightImage.To = 220;
            doubleAnimationHeightImage.Duration = TimeSpan.FromSeconds(3);
            doubleAnimationHeightImage.RepeatBehavior = RepeatBehavior.Forever;
            doubleAnimationHeightImage.AutoReverse = true;
            img.BeginAnimation(HeightProperty, doubleAnimationHeightImage);


            DoubleAnimation da = new DoubleAnimation() { From = 0, To = 360, Duration = TimeSpan.FromSeconds(3) };
            RotateTransform rt = new RotateTransform() { CenterX = 0, CenterY = 0 };
            this.sneg.RenderTransform = rt;
            da.RepeatBehavior = RepeatBehavior.Forever;
            rt.BeginAnimation(RotateTransform.AngleProperty, da);

            DoubleAnimation d = new DoubleAnimation() { From = 360, To = 0, Duration = TimeSpan.FromSeconds(3) };
            RotateTransform r = new RotateTransform() { CenterX = 0, CenterY = 0 };
            this.sneg2.RenderTransform = r;
            d.RepeatBehavior = RepeatBehavior.Forever;
            r.BeginAnimation(RotateTransform.AngleProperty, d);

            DoubleAnimation dd = new DoubleAnimation() { From = 360, To = 0, Duration = TimeSpan.FromSeconds(3) };
            RotateTransform rr = new RotateTransform() { CenterX = 50, CenterY = 50 };
            this.sneg3.RenderTransform = rr;
            dd.RepeatBehavior = RepeatBehavior.Forever;
            rr.BeginAnimation(RotateTransform.AngleProperty, dd);

            DoubleAnimation ddd = new DoubleAnimation() { From = 0, To = 360, Duration = TimeSpan.FromSeconds(3) };
            RotateTransform rrr = new RotateTransform() { CenterX = 50, CenterY = 50 };
            this.sneg4.RenderTransform = rrr;
            ddd.RepeatBehavior = RepeatBehavior.Forever;
            rrr.BeginAnimation(RotateTransform.AngleProperty, ddd);

            DoubleAnimation dddd = new DoubleAnimation() { From = 0, To = 360, Duration = TimeSpan.FromSeconds(3) };
            RotateTransform rrrr = new RotateTransform() { CenterX = 50, CenterY = 50 };
            this.sneg5.RenderTransform = rrrr;
            dddd.RepeatBehavior = RepeatBehavior.Forever;
            rrrr.BeginAnimation(RotateTransform.AngleProperty, dddd);

            DoubleAnimation ddddd = new DoubleAnimation() { From =360, To = 0, Duration = TimeSpan.FromSeconds(3) };
            RotateTransform rrrrr = new RotateTransform() { CenterX = 50, CenterY = 50 };
            this.sneg6.RenderTransform = rrrrr;
            ddddd.RepeatBehavior = RepeatBehavior.Forever;
            rrrrr.BeginAnimation(RotateTransform.AngleProperty, ddddd);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Framec.MainFrame.Navigate(new Authorizat());
        }

        private void buttonZapis_Click(object sender, RoutedEventArgs e)
        {
            Framec.MainFrame.Navigate(new Authorizat());
        }
    }
}
