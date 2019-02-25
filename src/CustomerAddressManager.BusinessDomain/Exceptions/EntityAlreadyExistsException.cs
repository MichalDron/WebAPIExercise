using System;

namespace CustomerAddressManager.BusinessDomain.Exceptions
{
    public class EntityAlreadyExistsException<T> : Exception
    {
        public override string Message => $"{typeof(T)} with provided key already exists";
    }
}
