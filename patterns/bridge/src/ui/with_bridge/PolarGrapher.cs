using System.Linq;
using System.Windows;
using ui.without_bridge;

namespace ui.with_bridge
{
    public class PolarGrapher : IGraph
    {
        IPlot plotter;

        public PolarGrapher(IPlot plotter)
        {
            this.plotter = plotter;
            plotter.plot_axes();
        }

        public void graph_line(Point start, Point end)
        {
            var resolution = 2000;
            var x_increment = (end.X - start.X) / resolution;
            var y_increment = (end.Y - start.Y) / resolution;

            plotter.start_path();
            foreach (var interval in Enumerable.Range(0, resolution))
            {
                var segment_start = new Point(start.X + x_increment * interval, start.Y + y_increment * interval).convert_to_cartesian();
                var segment_end = new Point(start.X + x_increment * (interval + 1), start.Y + y_increment * (interval + 1)).convert_to_cartesian();

                plotter.plot(segment_start, segment_end);
            }
            plotter.end_path();
        }
    }
}