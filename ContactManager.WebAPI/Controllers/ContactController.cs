using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactManager.Core.Entities;
using ContactManager.Infrastructure;
using ContactManager.Infrastructure.Services.Interfaces;

namespace ContactManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        public readonly IContactServices _contactServices;

        public ContactController(IContactServices contactServices)
        {
            _contactServices = contactServices;
        }
        [HttpGet]
        public async Task<ActionResult<ICollection<Contact>>> GetContacts()
        {
            return Ok(await _contactServices.ListContacts());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(Guid id)
        {
            return Ok(await _contactServices.GetContactById(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(Guid id, Contact contact)
        {
            return Ok(await _contactServices.UpdateContact(id, contact));
        }

        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(Contact contact)
        {

            var response = await _contactServices.AddContact(contact);
            return CreatedAtAction("GetContact", new { id = contact.Id }, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Contact>> DeleteContact(Guid id)
        {
            var contact = await _contactServices.RemoveContact(id);
            return contact;
        }

        [HttpGet("api/[controller]/CompanyContacts/{id}")]
        public async Task<ActionResult<ContactCompany>> GetContactsByCompany(Guid id)
        {
            return Ok(await _contactServices.GetContactsByCompany(id));
        }

    }
}
