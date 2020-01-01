using System;

namespace ContactManager.Core.Entities
{
    public class ContactCompany
    {
        public Guid IdCompany { get; set; }
        public Guid IdContact { get; set; }
        public Contact Contact { get; set; }
        public Company Company { get; set; }
    }
}