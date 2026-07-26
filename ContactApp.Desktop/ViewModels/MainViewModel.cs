using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using ContactApp.Core.Models;
using ContactApp.Core.Repository;

namespace ContactApp.Desktop.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IContactRepository _repo;

        public ObservableCollection<Contact> Contacts { get; } = new ObservableCollection<Contact>();

        private Contact? _selected;
        public Contact? Selected
        {
            get => _selected;
            set { _selected = value; OnPropertyChanged(nameof(Selected)); }
        }

        private string _search = string.Empty;
        public string Search
        {
            get => _search;
            set { _search = value; OnPropertyChanged(nameof(Search)); }
        }

        private string _companyFilter = string.Empty;
        public string CompanyFilter
        {
            get => _companyFilter;
            set { _companyFilter = value; OnPropertyChanged(nameof(CompanyFilter)); }
        }

        public ICommand CmdRefresh { get; }
        public ICommand CmdAdd { get; }
        public ICommand CmdEdit { get; }
        public ICommand CmdDelete { get; }
        public ICommand CmdExportCsv { get; }

        public MainViewModel()
        {
            _repo = new ContactRepository();
            CmdRefresh = new RelayCommand(_ => Refresh());
            CmdAdd = new RelayCommand(_ => Add());
            CmdEdit = new RelayCommand(_ => Edit(), _ => Selected != null);
            CmdDelete = new RelayCommand(_ => Delete(), _ => Selected != null);
            CmdExportCsv = new RelayCommand(_ => ExportCsv());

            Refresh();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public void Refresh()
        {
            Contacts.Clear();
            foreach (var c in _repo.Search(Search, string.IsNullOrWhiteSpace(CompanyFilter) ? null : CompanyFilter))
                Contacts.Add(c);
        }

        private void Add()
        {
            var win = new AddEditContactWindow();
            if (win.ShowDialog() == true)
            {
                var newContact = win.Contact;
                _repo.Add(newContact);
                Refresh();
            }
        }

        private void Edit()
        {
            if (Selected == null) return;
            var win = new AddEditContactWindow(Selected);
            if (win.ShowDialog() == true)
            {
                _repo.Update(win.Contact);
                Refresh();
            }
        }

        private void Delete()
        {
            if (Selected == null) return;
            if (MessageBox.Show($"Delete {Selected.FirstName} {Selected.LastName}?","Confirm",MessageBoxButton.YesNo,MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                _repo.Delete(Selected.Id);
                Refresh();
            }
        }

        private void ExportCsv()
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "contacts_export.csv");
                var sb = new StringBuilder();
                sb.AppendLine("Id,FirstName,LastName,Email,Phone,Company,Notes,CreatedAt,ModifiedAt");
                foreach (var c in Contacts)
                {
                    sb.AppendLine($"{c.Id},\"{c.FirstName}\",\"{c.LastName}\",\"{c.Email}\",\"{c.Phone}\",\"{c.Company}\",\"{(c.Notes ?? string.Empty).Replace("\"","\"\"")}\",{c.CreatedAt:o},{c.ModifiedAt:o}");
                }
                File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
                MessageBox.Show($"Exported {Contacts.Count} contacts to {path}", "Export", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Export failed: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
