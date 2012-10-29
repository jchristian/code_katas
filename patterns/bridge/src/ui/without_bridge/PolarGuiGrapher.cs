using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ui.without_bridge
{
    public class PolarGuiGrapher : GuiGrapher
    {
        public PolarGuiGrapher(Canvas canvas) : base(canvas) { }

        public override void graph_line(Point start, Point end)
        {
            var path = new Path();
            var resolution = 2000;
            var x_increment = (end.X - start.X) / resolution;
            var y_increment = (end.Y - start.Y) / resolution;

            var group = new GeometryGroup();
            path.Data = group;
            foreach (var interval in Enumerable.Range(0, resolution))
            {
                var segment_start = new Point(start.X + x_increment * interval, start.Y + y_increment * interval).convert_to_cartesian().translate(center);
                var segment_end = new Point(start.X + x_increment * (interval + 1), start.Y + y_increment * (interval + 1)).convert_to_cartesian().translate(center);

                var segment = new LineGeometry(segment_start, segment_end);
                group.Children.Add(segment);
            }

            path.Stroke = Brushes.Black;
            path.StrokeThickness = 1;
            path.SnapsToDevicePixels = true;
            path.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);

            canvas.Children.Add(path);
        }
    }
}