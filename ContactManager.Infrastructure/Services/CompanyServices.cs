using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManager.Core.Entities;
using ContactManager.Infrastructure.Exceptions;
using ContactManager.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Infrastructure.Services
{
    public class CompanyServices : ICompanyServices
    {

        private readonly ContactManagerDbContext _context;

        public CompanyServices(ContactManagerDbContext context)
        {
            _context = context;
        }
        public async Task<Company> AddCompany(Company company)
        {
            if (company == null)
                throw new NullCompanyException();

            //check if already exists
            if (_context.Companies.Find(company.Id) != null)
                throw new ArgumentException("L'entreprise existe déjà");

            if (string.IsNullOrEmpty(company.VatNumber))
                throw new NullVatNumberException();

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return company;
        }

        public async Task<Company> AddCompanyAddress(Guid id, Address address)
        {
            // check if already exists
            if (address.Id != Guid.Empty && _context.Addresses.FindAsync(address.Id) != null)
                throw new ArgumentNullException("Addresse déjà utilisée pour cette entreprise");

            // if parimary address is added, then switch existing primary address to false
            if (address.IsPrimary)
            {
                var existingPrimaryAddress = await _context.Addresses.Where(d => d.IdCompany == address.IdCompany && d.IsPrimary).FirstOrDefaultAsync();
                if (existingPrimaryAddress != null)
                    existingPrimaryAddress.IsPrimary = false;

                _context.Addresses.Update(existingPrimaryAddress);
            };

            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
            return _context.Companies.Find(id);
        }

        public async Task<Company> GetById(Guid Id)
        {
            var company = await _context.Companies.Include(d => d.Addresses).Include(d => d.ContactCompanies).ThenInclude(d => d.Contact).FirstOrDefaultAsync(d => d.Id == Id);

            if (company == null)
            {
                throw new ArgumentException("L'Entreprise n'existe pas ");
            }

            return company;
        }

        public async Task<ICollection<Company>> ListCompanies()
        {
            return await _context.Companies.Include(d => d.Addresses).Include(d => d.ContactCompanies).ToListAsync();
        }

        public async Task<Company> RemoveCompany(Guid Id)
        {
            var company = _context.Companies.Find(Id);
            if (company == null)
                throw new ArgumentNullException("L'entreprise n'existe pas ");

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return company;
        }

        public async Task<bool> UpdateCompany(Guid id, Company company)
        {
            if (id != company.Id)
            {
                throw new ArgumentException("L'Entreprise ne correspond pas");
            }

            _context.Entry(company).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (_context.Companies.Find(id) == null)
                {
                    throw new ArgumentNullException("L'entreprise n'existe pas");
                }
                else
                {
                    throw new Exception("Erreur inattendue", ex);
                }
            }

            return true;
        }

        public async Task<Company> UpdateCompanyAddress(Guid id, Address address)
        {
            var oldAddress = _context.Addresses.FirstOrDefault(d => d.Id == address.Id && d.IdCompany == id);

            if (oldAddress == null)
                throw new ArgumentNullException("L'adresse à mettre à jour n'existe pas pour cette entreprise");

            oldAddress.City = address.City;
            oldAddress.Country = address.Country;
            oldAddress.IsPrimary = address.IsPrimary;
            oldAddress.PostalCode = address.PostalCode;
            oldAddress.Street = address.Street;

            _context.Entry(oldAddress).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _context.Companies.Find(id);
        }
    }

}