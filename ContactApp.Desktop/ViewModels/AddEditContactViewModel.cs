using System.ComponentModel;
using ContactApp.Core.Models;

namespace ContactApp.Desktop.ViewModels
{
    public class AddEditContactViewModel : INotifyPropertyChanged
    {
        private Contact _contact;

        public AddEditContactViewModel(Contact? contact = null)
        {
            _contact = contact != null ? new Contact
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                Phone = contact.Phone,
                Company = contact.Company,
                Notes = contact.Notes,
                CreatedAt = contact.CreatedAt,
                ModifiedAt = contact.ModifiedAt
            } : new Contact();
        }

        public Contact Contact => _contact;

        public string FirstName { get => _contact.FirstName; set { _contact.FirstName = value; OnPropertyChanged(nameof(FirstName)); } }
        public string LastName { get => _contact.LastName; set { _contact.LastName = value; OnPropertyChanged(nameof(LastName)); } }
        public string? Email { get => _contact.Email; set { _contact.Email = value; OnPropertyChanged(nameof(Email)); } }
        public string? Phone { get => _contact.Phone; set { _contact.Phone = value; OnPropertyChanged(nameof(Phone)); } }
        public string? Company { get => _contact.Company; set { _contact.Company = value; OnPropertyChanged(nameof(Company)); } }
        public string? Notes { get => _contact.Notes; set { _contact.Notes = value; OnPropertyChanged(nameof(Notes)); } }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
