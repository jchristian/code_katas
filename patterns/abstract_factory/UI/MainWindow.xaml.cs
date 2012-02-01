using System.Windows;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FactoryRegistry _factoryRegistry;

        public MainWindow()
        {
            InitializeComponent();

            _factoryRegistry = new FactoryRegistry();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var shape = _factoryRegistry.Find(ShapeComboBox.SelectionBoxItem.ToString()).Create();
            shape.Margin = new Thickness(5);
            ShapePanel.Children.Add(shape);
        }
    }
}
