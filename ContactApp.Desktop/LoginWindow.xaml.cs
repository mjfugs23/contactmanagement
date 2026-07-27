using System;
using System.Security.Principal;
using System.Windows;

namespace ContactApp.Desktop
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnWindows_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var id = WindowsIdentity.GetCurrent();
                string user = id?.Name ?? "Unknown";
                OpenMainWindow(user);
            }
            catch (Exception ex)
            {
                StatusText.Text = "Windows Integrated login failed: " + ex.Message;
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string user = UsernameTextBox.Text?.Trim() ?? string.Empty;
            string pass = PasswordBox.Password ?? string.Empty;

            // Simple demo credential check. Replace with API-backed auth for real deployments.
            if (string.Equals(user, "admin", StringComparison.OrdinalIgnoreCase) && pass == "password")
            {
                OpenMainWindow(user);
            }
            else
            {
                StatusText.Text = "Invalid username or password.";
            }
        }

        private void OpenMainWindow(string username)
        {
            // Ensure database is created before showing main UI
            var repo = new ContactApp.Core.Repository.ContactRepository();
            repo.EnsureCreated();

            var main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
