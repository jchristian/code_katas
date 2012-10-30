using System.Collections.Generic;
using System.Windows;
using ui.with_bridge;
using ui.without_bridge;

namespace ui
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var graphers = new List<IGraph>();

            graphers.Add(new SkewGrapher(new CartesianGuiGrapher(without_cartesian), new Point(1, -1)));
            graphers.Add(new SkewGrapher(new PolarGuiGrapher(without_polar), new Point(1, -1)));
            graphers.Add(new CartesianGrapher(new SkewPlotter(new GuiPlotter(with_cartesian), new Point(1, -1))));
            graphers.Add(new PolarGrapher(new SkewPlotter(new GuiPlotter(with_polar), new Point(1, -1))));

            graphers.ForEach(x => x.graph_line(new Point(10, 10), new Point(30, 20)));
        }
    }
}