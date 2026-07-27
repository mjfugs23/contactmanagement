using System.Windows;

namespace ContactApp.Desktop
{
    public partial class MainWindow : Window
    {
        public MainWindow(string username)
        {
            InitializeComponent();
        }

        public MainWindow() : this(string.Empty)
        {
        }
    }
}
