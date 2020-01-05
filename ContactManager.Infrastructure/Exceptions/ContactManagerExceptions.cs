using System;

namespace ContactManager.Infrastructure.Exceptions
{
    public class NullCompanyException : Exception
    {
        public NullCompanyException() : base()
        {
        }
    }

    public class NullVatNumberException : Exception
    {
        public NullVatNumberException() : base()
        {
        }
    }

    public class NullPrimaryAddressException : Exception
    {
        public NullPrimaryAddressException()
        {

        }
    }
}