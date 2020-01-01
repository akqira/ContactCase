using ContactManager.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Infrastructure
{
    public class ContactManagerDbContext : DbContext
    {
        public ContactManagerDbContext(DbContextOptions<ContactManagerDbContext> options)
                   : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Contact>()
            .HasMany(d => d.ContactCompanies)
            .WithOne(d => d.Contact)
            .HasForeignKey(d => d.IdContact);

            builder.Entity<Company>()
            .HasMany(d => d.ContactCompanies)
            .WithOne(d => d.Company)
            .HasForeignKey(d => d.IdCompany);

            builder.Entity<Company>()
            .HasMany(d => d.Addresses)
            .WithOne(d => d.Company)
            .HasForeignKey(d => d.IdCompany);

            builder.Entity<ContactCompany>()
            .HasKey(d => new { d.IdCompany, d.IdContact });

        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ContactCompany> ContactCompanies { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}