using System;

namespace CustomerAddressManager.BusinessDomain.Exceptions
{
    public class EntityNotFoundException<T> : Exception
    {
        public override string Message => $"{typeof(T)} not found";
    }
}
