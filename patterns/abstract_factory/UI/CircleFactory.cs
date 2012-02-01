using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace UI
{
    public class CircleFactory : ICreateUIElements
    {
        public FrameworkElement Create()
        {
            return new Ellipse{ Fill = Brushes.Black, Height = 10, Width = 10 };
        }
    }
}