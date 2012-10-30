using System.Windows;

namespace ui.with_bridge
{
    public interface IPlot
    {
        void plot_axes();
        void plot(Point start, Point end);

        void start_path();
        void end_path();
    }
}