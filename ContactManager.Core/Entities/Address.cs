using System;

namespace ContactManager.Core.Entities
{
    public class Address
    {
        public Guid Id { get; set; }
        public bool IsPrimary { get; set; }
        public string Street { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public Guid IdCompany { get; set; }
        public Company Company { get; set; }
    }
}