using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using ui.Annotations;
using Point = System.Windows.Point;

namespace ui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ImageSpriteFactory sprite_factory;

        public IList<ImageSprite> Model
        {
            get { return (IList<ImageSprite>)DataContext; }
            set { DataContext = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
            Model = new ObservableCollection<ImageSprite>();
            sprite_factory = new ImageSpriteFactory();
        }

        void AddImage(object sender, MouseButtonEventArgs e)
        {
            var mouse_position = Mouse.GetPosition((IInputElement)sender);

            if (e.LeftButton == MouseButtonState.Pressed)
                Model.Add(sprite_factory.create_circle(mouse_position));
            if(e.RightButton == MouseButtonState.Pressed)
                Model.Add(sprite_factory.create_square(mouse_position));
        }
    }

    public class ImageSpriteFactory
    {
        public ImageSprite create_circle(Point position)
        {
            return new ImageSprite(position, Images.Circle);
        }

        public ImageSprite create_square(Point position)
        {
            return new ImageSprite(position, Images.Square);
        }
    }

    public static class Images
    {
        public static string Circle { get { return "pack://application:,,,/images/circle.png"; } }
        public static string Square { get { return "pack://application:,,,/images/square.png"; } }
    }

    public class ImageSprite : INotifyPropertyChanged {
        int _x;
        public int X
        {
            get { return _x; }
            protected set
            {
                _x = value;
                OnPropertyChanged("X");
            }
        }

        int _y;
        public int Y
        {
            get { return _y; }
            protected set
            {
                _y = value;
                OnPropertyChanged("Y");
            }
        }

        string _source;
        public string Source
        {
            get { return _source; }
            protected set
            {
                _source = value;
                OnPropertyChanged("Source");
            }
        }

        public ImageSprite(Point position, string source)
        {
            X = (int)position.X;
            Y = (int)position.Y;
            Source = source;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string property_name)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(property_name));
        }
    }
}
