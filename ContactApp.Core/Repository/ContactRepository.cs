using System;
using System.Collections.Generic;
using System.Linq;
using ContactApp.Core.Data;
using ContactApp.Core.Models;

namespace ContactApp.Core.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactDbContext _db;

        public ContactRepository(ContactDbContext? context = null)
        {
            _db = context ?? new ContactDbContext();
            EnsureCreated();
        }

        public IEnumerable<Contact> GetAll()
        {
            return _db.Contacts.OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ToList();
        }

        public Contact? GetById(int id)
        {
            return _db.Contacts.Find(id);
        }

        public IEnumerable<Contact> Search(string? query, string? company)
        {
            var q = _db.Contacts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                string term = query.Trim();
                q = q.Where(c => c.FirstName.Contains(term) || c.LastName.Contains(term) || (c.Email ?? string.Empty).Contains(term) || (c.Phone ?? string.Empty).Contains(term));
            }

            if (!string.IsNullOrWhiteSpace(company))
            {
                q = q.Where(c => (c.Company ?? string.Empty) == company);
            }

            return q.OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ToList();
        }

        public Contact Add(Contact contact)
        {
            contact.CreatedAt = DateTime.UtcNow;
            _db.Contacts.Add(contact);
            _db.SaveChanges();
            return contact;
        }

        public Contact Update(Contact contact)
        {
            contact.ModifiedAt = DateTime.UtcNow;
            _db.Contacts.Update(contact);
            _db.SaveChanges();
            return contact;
        }

        public void Delete(int id)
        {
            var c = _db.Contacts.Find(id);
            if (c != null)
            {
                _db.Contacts.Remove(c);
                _db.SaveChanges();
            }
        }

        public void EnsureCreated()
        {
            _db.Database.EnsureCreated();
        }
    }
}
