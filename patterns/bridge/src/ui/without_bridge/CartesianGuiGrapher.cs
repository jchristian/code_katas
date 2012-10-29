using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ui.without_bridge
{
    public class CartesianGuiGrapher : GuiGrapher
    {
        public CartesianGuiGrapher(Canvas canvas) : base(canvas) { }

        public override void graph_line(Point start, Point end)
        {
            var line = new Line();
            start = start.translate(center);
            end = end.translate(center);

            line.X1 = start.X;
            line.Y1 = start.Y;
            line.X2 = end.X;
            line.Y2 = end.Y;

            line.Stroke = Brushes.Black;
            line.StrokeThickness = 1;
            line.SnapsToDevicePixels = true;
            line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);

            canvas.Children.Add(line);
        }
    }
}