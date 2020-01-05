using System;
using System.Collections.Generic;

namespace ContactManager.Core.Entities
{
    public class Company
    {
        public Company()
        {
            Addresses = new HashSet<Address>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public string VatNumber { get; set; }
        public ICollection<ContactCompany> ContactCompanies { get; set; }
    }
}