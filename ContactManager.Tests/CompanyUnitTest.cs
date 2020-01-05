using ContactManager.Core.Entities;
using ContactManager.Infrastructure;
using ContactManager.Infrastructure.Exceptions;
using ContactManager.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace ContactManager.Tests
{
    public class CompanyUnitTest
    {
        public ContactManagerDbContext Context { get; set; }

        public CompanyUnitTest()
        {
            Initialize();
        }

        void Initialize()
        {
            var builder = new DbContextOptionsBuilder<ContactManagerDbContext>().UseSqlServer("Server=localhost;Database=contactDB;Persist Security Info=False;User ID=SA;Password=P@ssword1;MultipleActiveResultSets=False;");

            Context = new ContactManagerDbContext(builder.Options);
        }

        [Fact]
        public void AddCompany_CompanyCannotBeNull()
        {
            var _companyServices = new Mock<ICompanyServices>();

            Assert.ThrowsAsync<NullCompanyException>(async () =>
            {
                await _companyServices.Object.AddCompany(null);
            });
        }


        [Fact]
        public void AddCompany_CompanyMustHaveVatNumber()
        {
            var _companyServices = new Mock<ICompanyServices>();

            Assert.ThrowsAsync<NullVatNumberException>(async () =>
            {
                await _companyServices.Object.AddCompany(new Company
                {
                    Name = "Test company",
                    VatNumber = string.Empty
                });
            });
        }


        [Fact]
        public void AddCompany_CompanyMustHavePrimaryAddress()
        {
            var _companyServices = new Mock<ICompanyServices>();

            var nonPrimaryAddress = new Address
            {
                City = "Belgium",
                Street = "Bd Leopold 2",
                Country = "Belgium",
                IsPrimary = false
            };

            var company = new Company
            {
                Name = "Test company",
                VatNumber = "00000"
            };
            company.Addresses.Add(nonPrimaryAddress);

            Assert.ThrowsAsync<NullPrimaryAddressException>(async () =>
            {
                await _companyServices.Object.AddCompany(company);
            });
        }
    }
}
