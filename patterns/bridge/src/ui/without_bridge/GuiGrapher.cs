using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ui.without_bridge
{
    public abstract class GuiGrapher : IGraph
    {
        protected Canvas canvas;
        protected Point center;

        public GuiGrapher(Canvas canvas)
        {
            this.canvas = canvas;
            center = new Point(canvas.Width/2, canvas.Height/2);

            draw_axes();
        }

        void draw_axes()
        {
            draw_axis(new Point(center.X, 0), new Point(center.X, canvas.Height));
            draw_axis(new Point(0, center.Y), new Point(canvas.Width, center.Y));
        }

        void draw_axis(Point start, Point end)
        {
            var line = new Line();

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

        public abstract void graph_line(Point start, Point end);
    }
}