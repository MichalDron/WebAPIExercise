using System.Collections.Generic;
using CustomerAddressManager.Dal.Models;

namespace CustomerAddressManager.BusinessDomain.Services
{
    public interface ICustomerService
    {
        void CreateCustomer(Customer commandEntity);
        void Delete(string customerId, string name);
        IEnumerable<Customer> GetAll();
        IEnumerable<Customer> GetByCustomerId(string customerId);
        Customer GetSingle(string customerId, string name);
        void Update(Customer commandEntity);
    }
}