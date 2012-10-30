using System.Windows;

namespace ui.with_bridge
{
    public class CartesianGrapher : IGraph
    {
        IPlot plotter;

        public CartesianGrapher(IPlot plotter)
        {
            this.plotter = plotter;
            plotter.plot_axes();
        }

        public void graph_line(Point start, Point end)
        {
            plotter.start_path();
            plotter.plot(start, end);
            plotter.end_path();
        }
    }
}