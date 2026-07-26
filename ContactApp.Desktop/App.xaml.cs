using System;
using System.Windows;
using ContactApp.Core.Repository;

namespace ContactApp.Desktop
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            try
            {
                // initialize DB on startup
                var repo = new ContactRepository();
                repo.EnsureCreated();
            }
            catch (Exception)
            {
                // ignore DB errors here; LoginWindow will handle runtime errors
            }

            var login = new LoginWindow();
            login.Show();
        }
    }
}
