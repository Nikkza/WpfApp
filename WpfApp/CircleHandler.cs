using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp
{
    public class CircleHandler
    {
        Ellipse ellipse = new Ellipse();
        public void CircleAdd(Point s, MouseButtonEventArgs e,Canvas c,Brush stroke,Brush fill)
        {
            s = e.MouseDevice.GetPosition(c);

            ellipse.Stroke = stroke;
            ellipse.StrokeThickness = 3;
            ellipse.Fill = fill;
            c.Children.Add(ellipse);
        }
    }
}
