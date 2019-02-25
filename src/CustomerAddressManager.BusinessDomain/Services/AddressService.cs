using CustomerAddressManager.BusinessDomain.Exceptions;
using CustomerAddressManager.Dal.Models;
using CustomerAddressManager.Dal.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace CustomerAddressManager.BusinessDomain.Services
{
    public class AddressService : IAddressService
    {
        private readonly IRepository<Address> addressRepository;

        public AddressService(IRepository<Address> addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        public IEnumerable<Address> GetAll()
        {
            return this.addressRepository
                        .GetAll()
                        .ToList();
        }

        public Address GetSingle(string customerId, string addressType)
        {
            var entity = this.GetAddressByCustomerIdAndAddressType(customerId, addressType);

            if (entity == null)
            {
                throw new EntityNotFoundException<Address>();
            }

            return entity;
        }

        public IEnumerable<Address> GetByCustomerId(string customerId)
        {
            return this.addressRepository
                        .Get(x => x.CustomerId == customerId)
                        .ToList();
        }

        public void CreateAddress(Address commandEntity)
        {
            var entity = this.GetAddressByCustomerIdAndAddressType(commandEntity.CustomerId, commandEntity.AddressType);

            if (entity != null)
            {
                throw new EntityAlreadyExistsException<Address>();
            }

            this.addressRepository.Create(commandEntity);
            this.addressRepository.Save();
        }

        public void Update(Address commandEntity)
        {
            var entity = this.GetAddressByCustomerIdAndAddressType(commandEntity.CustomerId, commandEntity.AddressType);

            if (entity == null)
            {
                throw new EntityNotFoundException<Address>();
            }

            this.addressRepository.Update(commandEntity);
            this.addressRepository.Save();
        }

        public void Delete(string customerId)
        {
            var entities = this.GetByCustomerId(customerId);

            if (!entities.Any())
            {
                return;
            }

            foreach (Address entity in entities)
            {
                this.addressRepository.Delete(entity);
            }

            this.addressRepository.Save();
        }

        public void Delete(string customerId, string addressType)
        {
            var entity = this.GetSingle(customerId, addressType);

            if (entity == null)
            {
                throw new EntityNotFoundException<Address>();
            }

            this.addressRepository.Delete(entity);
            this.addressRepository.Save();
        }

        private Address GetAddressByCustomerIdAndAddressType(string customerId, string addressType)
        {
            return this.addressRepository
                        .GetSingle(x => x.CustomerId == customerId && x.AddressType == addressType);
        }
    }
}
