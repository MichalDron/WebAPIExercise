using CustomerAddressManager.BusinessDomain.Exceptions;
using CustomerAddressManager.Dal.Models;
using CustomerAddressManager.Dal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerAddressManager.BusinessDomain.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> customerRepository;
        private readonly IAddressService addressService;

        public CustomerService(IRepository<Customer> customerRepository, IAddressService addressService)
        {
            this.customerRepository = customerRepository;
            this.addressService = addressService;
        }

        public IEnumerable<Customer> GetAll()
        {
            return this.customerRepository
                        .GetAll()
                        .ToList();
        }

        public Customer GetSingle(string customerId, string name)
        {
            var entity = this.GetCustomerByIdAndName(customerId, name);

            if (entity == null)
            {
                throw new EntityNotFoundException<Customer>();
            }

            return entity;
        }

        public IEnumerable<Customer> GetByCustomerId(string customerId)
        {
            return this.customerRepository
                        .Get(x => x.CustomerId == customerId)
                        .ToList();
        }

        public void CreateCustomer(Customer commandEntity)
        {
            var entity = this.GetCustomerByIdAndName(commandEntity.CustomerId, commandEntity.Name);

            if (entity != null)
            {
                throw new EntityAlreadyExistsException<Customer>();
            }

            this.customerRepository.Create(commandEntity);
            this.customerRepository.Save();
        }

        public void Update(Customer commandEntity)
        {
            var entity = this.GetCustomerByIdAndName(commandEntity.CustomerId, commandEntity.Name);

            if (entity == null)
            {
                throw new EntityNotFoundException<Customer>();
            }

            this.customerRepository.Update(commandEntity);
            this.customerRepository.Save();
        }

        public void Delete(string customerId, string name)
        {
            var entities = this.GetByCustomerId(customerId);

            if (!entities.Any())
            {
                throw new EntityNotFoundException<Customer>();
            }

            var entity = entities.Where(x => x.CustomerId == customerId && x.Name == name).SingleOrDefault();

            if (entity == null)
            {
                throw new EntityNotFoundException<Customer>();
            }

            if (entities.Count() == 1)
            {
                this.addressService.Delete(customerId);
            }

            this.customerRepository.Delete(entity);
            this.customerRepository.Save();
        }

        private Customer GetCustomerByIdAndName(string customerId, string name)
        {
            return this.customerRepository.GetSingle(x => x.CustomerId == customerId && x.Name == name);
        }
    }
}
