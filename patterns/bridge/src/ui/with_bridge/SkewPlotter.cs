using System.Windows;
using ui.without_bridge;

namespace ui.with_bridge
{
    public class SkewPlotter : IPlot
    {
        Point skew;
        IPlot base_plotter;

        public SkewPlotter(IPlot base_plotter, Point skew)
        {
            this.base_plotter = base_plotter;
            this.skew = skew;
        }

        public void plot_axes()
        {
            base_plotter.plot_axes();
        }

        public void plot(Point start, Point end)
        {
            base_plotter.plot(start.skew(skew), end.skew(skew));
        }

        public void start_path()
        {
            base_plotter.start_path();
        }

        public void end_path()
        {
            base_plotter.end_path();
        }
    }
}