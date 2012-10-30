using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ui.with_bridge
{
    public class GuiPlotter : IPlot
    {
        Canvas canvas;
        Point center;
        Path current_path;

        public GuiPlotter(Canvas canvas)
        {
            this.canvas = canvas;
            center = new Point(canvas.Width / 2, canvas.Height / 2);
        }

        public void plot_axes()
        {
            start_path();
            plot_line_absolute(new Point(center.X, 0), new Point(center.X, canvas.Height));
            end_path();

            start_path();
            plot_line_absolute(new Point(0, center.Y), new Point(canvas.Width, center.Y));
            end_path();
        }

        public void plot(Point start, Point end)
        {
            plot_line_absolute(start.translate(center), end.translate(center));
        }

        public void start_path()
        {
            current_path = new Path();
            current_path.Data = new GeometryGroup();

            current_path.Stroke = Brushes.Black;
            current_path.StrokeThickness = 1;
            current_path.SnapsToDevicePixels = true;
            current_path.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
        }

        public void end_path()
        {
            canvas.Children.Add(current_path);
        }

        void plot_line_absolute(Point start, Point end)
        {
            ((GeometryGroup)current_path.Data).Children.Add(new LineGeometry(start, end));
        }
    }
}