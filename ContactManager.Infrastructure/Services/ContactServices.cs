using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManager.Core.Entities;
using ContactManager.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Infrastructure.Services
{
    public class ContactServices : IContactServices
    {
        private readonly ContactManagerDbContext _context;

        public ContactServices(ContactManagerDbContext context)
        {
            _context = context;
        }

        public async Task<Contact> AddContact(Contact contact)
        {
            //check if already exists
            if (_context.Contacts.Find(contact.Id) != null)
                throw new ArgumentException("Le contact existe déjà");

            if (contact.IsFreelancer && string.IsNullOrEmpty(contact.VatNumber))
                throw new ArgumentException("Le VAT est obligatoire");

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<Contact> GetContactById(Guid id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                throw new NullReferenceException("Contact introuvable");
            }

            return contact;
        }

        public async Task<ICollection<Contact>> ListContacts()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact> RemoveContact(Guid Id)
        {
            var contact = _context.Contacts.Find(Id);
            if (contact == null)
                throw new ArgumentNullException("L'entreprise n'existe pas ");

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<bool> UpdateContact(Guid id, Contact contact)
        {

            if (id != contact.Id)
            {
                throw new ArgumentException("Le contact ne correspond pas");
            }

            _context.Entry(contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (_context.Contacts.Find(id) == null)
                {
                    throw new ArgumentNullException("Le contact n'existe pas");
                }
                else
                {
                    throw new Exception("Erreur inattendue", ex);
                }
            }

            return true;

        }

        public async Task<ICollection<Contact>> GetContactsByCompany(Guid id)
        {
            var company = _context.Companies.Include(d => d.ContactCompanies).FirstOrDefault(d => d.Id == id);

            if (company == null)
                throw new NullReferenceException("Entreprise introuvable");

            var listContactsId = company.ContactCompanies.Select(d => d.IdContact).ToList();

            return await _context.Contacts.Where(d => listContactsId.Contains(d.Id)).ToListAsync();
        }
    }
}

