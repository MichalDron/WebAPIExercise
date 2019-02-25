using System.Collections.Generic;
using CustomerAddressManager.Dal.Models;

namespace CustomerAddressManager.BusinessDomain.Services
{
    public interface IAddressService
    {
        void CreateAddress(Address commandEntity);

        void Delete(string customerId);

        void Delete(string customerId, string addressType);

        IEnumerable<Address> GetAll();

        IEnumerable<Address> GetByCustomerId(string customerId);

        Address GetSingle(string customerId, string addressType);

        void Update(Address commandEntity);
    }
}
