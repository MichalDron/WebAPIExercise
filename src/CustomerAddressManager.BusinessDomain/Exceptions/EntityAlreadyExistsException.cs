using System;

namespace CustomerAddressManager.BusinessDomain.Exceptions
{
    public class EntityAlreadyExistsException<T> : Exception
    {
        public override string Message => $"{typeof(T).Name} with provided key already exists";
    }
}
