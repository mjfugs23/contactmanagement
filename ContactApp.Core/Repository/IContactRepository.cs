using System.Collections.Generic;
using ContactApp.Core.Models;

namespace ContactApp.Core.Repository
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetAll();
        Contact? GetById(int id);
        IEnumerable<Contact> Search(string? query, string? company);
        Contact Add(Contact contact);
        Contact Update(Contact contact);
        void Delete(int id);
        void EnsureCreated();
    }
}
