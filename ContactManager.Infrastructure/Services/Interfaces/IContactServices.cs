using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactManager.Core.Entities;

namespace ContactManager.Infrastructure.Services.Interfaces
{
    public interface IContactServices
    {
        Task<Contact> AddContact(Contact contact);

        Task<Contact> RemoveContact(Guid Id);

        Task<bool> UpdateContact(Guid id, Contact contact);

        Task<ICollection<Contact>> ListContacts();
        Task<ICollection<Contact>> GetContactsByCompany(Guid id);
        Task<Contact> GetContactById(Guid id);
    }
}