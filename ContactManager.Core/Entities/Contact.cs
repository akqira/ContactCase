using System;
using System.Collections.Generic;

namespace ContactManager.Core.Entities
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool IsFreelancer { get; set; } = false;
        public string VatNumber { get; set; }
        public ICollection<ContactCompany> ContactCompanies { get; set; }
    }
}