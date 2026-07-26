using System.IO;
using Microsoft.EntityFrameworkCore;
using ContactApp.Core.Models;

namespace ContactApp.Core.Data
{
    public class ContactDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; } = null!;

        public ContactDbContext()
        {
        }

        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Place the SQLite DB in the app folder next to the executable by default
                var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "contacts.db");
                optionsBuilder.UseSqlite($"Data Source={dbPath}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Contact>().HasIndex(c => new { c.LastName, c.FirstName });
        }
    }
}
