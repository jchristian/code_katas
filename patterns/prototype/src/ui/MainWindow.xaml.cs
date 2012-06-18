using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xaml;

namespace ui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IDrawOnTheCanvas current_tool = new EmptyTool();
        IContainTheDrawingTools tools;

        public MainWindow()
        {
            InitializeComponent();

            tools = new DrawingTools();
            tool_bar_panel.Children.Add(tools.tool_bar);

            canvas.MouseDown += start_draw;
            canvas.MouseMove += continue_draw;
            canvas.MouseUp += end_draw;

            tools.add(new GenericToolFactory(() => new Pen(), "Pen"));
            copy_button.Click += (s, e) => tools.add(new Copier().copy(canvas.Children.Cast<UIElement>()));
        }

        void start_draw(object sender, MouseButtonEventArgs e)
        {
            current_tool = tools.get_current();
            current_tool.start_at(e.GetPosition(canvas));
            canvas.Children.Add(current_tool.shape);
        }

        void continue_draw(object sender, MouseEventArgs e)
        {
            current_tool.continue_at(e.GetPosition(canvas));
        }

        void end_draw(object sender, MouseButtonEventArgs e)
        {
            current_tool.end_at(e.GetPosition(canvas));
            current_tool = new EmptyTool();
        }
    }

    public interface ICreateATool
    {
        object label { get; }

        IDrawOnTheCanvas create();
    }

    public interface IContainTheDrawingTools
    {
        StackPanel tool_bar { get; }

        void add(ICreateATool tool_factory);
        IDrawOnTheCanvas get_current();
    }

    public class DrawingTools : IContainTheDrawingTools
    {
        ICreateATool current_tool_factory;
        public StackPanel tool_bar { get; private set; }

        public DrawingTools()
        {
            tool_bar = new StackPanel { Orientation = Orientation.Horizontal };
        }

        public void add(ICreateATool tool_factory)
        {
            var button = new Button { Content = tool_factory.label };
            button.Click += (s, e) => current_tool_factory = tool_factory;

            tool_bar.Children.Add(button);
        }

        public IDrawOnTheCanvas get_current()
        {
            return current_tool_factory.create();
        }
    }

    public class GenericToolFactory : ICreateATool
    {
        readonly Func<IDrawOnTheCanvas> factory; 
        public object label { get; private set; }

        public GenericToolFactory(Func<IDrawOnTheCanvas> factory, object label)
        {
            this.factory = factory;
            this.label = label;
        }

        public IDrawOnTheCanvas create()
        {
            return factory();
        }
    }

    public class Copier
    {
        public ICreateATool copy(IEnumerable<UIElement> copied_elements)
        {
            Func<IDrawOnTheCanvas> factory = () => new CompositeElement(copied_elements);
            var label = new Viewbox { Child = factory().shape, Width = 25, Height = 25 };

            return new GenericToolFactory(factory, label);
        }
    }

    public class CompositeElement : IDrawOnTheCanvas
    {
        UIElement representation;
        Point position;

        public CompositeElement(IEnumerable<UIElement> child_elements)
        {
            var canvas = new Canvas();
            foreach (var child in child_elements)
                canvas.Children.Add((UIElement)XamlServices.Parse(XamlServices.Save(child)));

            representation = canvas;
        }

        public void start_at(Point position)
        {
            this.position = position;
        }

        public void continue_at(Point position)
        {
            this.position = position;
        }

        public void end_at(Point position)
        {
            this.position = position;
        }

        public UIElement shape
        {
            get
            {
                representation.SetValue(Canvas.LeftProperty, position.X);
                representation.SetValue(Canvas.TopProperty, position.Y);
                return representation;
            }
        }
    }

    public class Pen : IDrawOnTheCanvas
    {
        PathGeometry path_geometry;
        Path path;
        Point last_position;

        public UIElement shape
        {
            get { return path; }
        }

        public Pen()
        {
            path_geometry = new PathGeometry();
            path = new Path { Data = path_geometry, StrokeThickness = 2, Stroke = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)) };
        }

        public void start_at(Point position)
        {
            last_position = position;
        }

        public void continue_at(Point position)
        {
            path_geometry.AddGeometry(new LineGeometry(last_position, position));
            last_position = position;
        }

        public void end_at(Point position)
        {
            path_geometry.AddGeometry(new LineGeometry(last_position, position));
        }
    }

    public interface IDrawOnTheCanvas
    {
        void start_at(Point position);
        void continue_at(Point position);
        void end_at(Point position);

        UIElement shape { get; }
    }

    public class EmptyTool : IDrawOnTheCanvas
    {
        public void start_at(Point position) { }
        public void continue_at(Point position) { }
        public void end_at(Point position) { }

        public UIElement shape
        {
            get { return new Line { StrokeThickness = 0 }; }
        }
    }
}
