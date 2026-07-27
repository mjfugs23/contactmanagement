using System.Windows;
using ContactApp.Core.Models;
using ContactApp.Desktop.ViewModels;

namespace ContactApp.Desktop
{
    public partial class AddEditContactWindow : Window
    {
        private readonly AddEditContactViewModel _vm;
        public AddEditContactWindow(Contact? contact = null)
        {
            InitializeComponent();
            _vm = new AddEditContactViewModel(contact);
            DataContext = _vm;
        }

        public Contact Contact => _vm.Contact;

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
