using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace UI
{
    public class SquareFactory : ICreateUIElements
    {
        public FrameworkElement Create()
        {
            return new Rectangle{ Fill = Brushes.Black, Width = 15, Height = 15 };
        }
    }
}