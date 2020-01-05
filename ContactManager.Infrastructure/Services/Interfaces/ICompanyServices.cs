using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactManager.Core.Entities;

namespace ContactManager.Infrastructure.Services.Interfaces
{
    public interface ICompanyServices
    {
        Task<Company> GetById(Guid Id);
        Task<Company> AddCompany(Company company);
        Task<Company> RemoveCompany(Guid Id);
        Task<bool> UpdateCompany(Guid id, Company company);
        Task<ICollection<Company>> ListCompanies();
        Task<Company> UpdateCompanyAddress(Guid id, Address address);
        Task<Company> AddCompanyAddress(Guid id, Address address);
    }
}