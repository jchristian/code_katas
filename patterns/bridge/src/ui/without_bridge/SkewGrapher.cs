using System.Windows;

namespace ui.without_bridge
{
    public class SkewGrapher : IGraph
    {
        Point skew;
        IGraph base_grapher;

        public SkewGrapher(IGraph base_grapher, Point skew)
        {
            this.base_grapher = base_grapher;
            this.skew = skew;
        }

        public void graph_line(Point start, Point end)
        {
            base_grapher.graph_line(start.skew(skew), end.skew(skew));
        }
    }
}