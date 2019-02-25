using CustomerAddressManager.BusinessDomain.Services;
using CustomerAddressManager.Dal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CustomerAddressManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;
        private readonly IAddressService addressService;

        public CustomersController(ICustomerService customerService, IAddressService addressService)
        {
            this.customerService = customerService;
            this.addressService = addressService;
        }

        [HttpPost]
        public ActionResult Create([FromBody] Customer value)
        {
            this.customerService.CreateCustomer(value);
            return CreatedAtAction(nameof(CustomersController.GetSingle), new { customerId = value.CustomerId, name = value.Name }, value);
        }

        [HttpPut]
        public ActionResult Update([FromBody] Customer value)
        {
            this.customerService.Update(value);
            return Ok();
        }

        [HttpDelete("{customerId}/names/{name}")]
        public ActionResult Delete(string customerId, string name)
        {
            this.customerService.Delete(customerId, name);
            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            var customers = this.customerService.GetAll();
            return Ok(customers);
        }

        [HttpGet("{customerId}")]
        public ActionResult<IEnumerable<Customer>> GetByCustomerId(string customerId)
        {
            var customers = this.customerService.GetByCustomerId(customerId);
            return Ok(customers);
        }

        [HttpGet("{customerId}/names/{name}")]
        public ActionResult<Customer> GetSingle(string customerId, string name)
        {
            var customers = this.customerService.GetSingle(customerId, name);
            return Ok(customers);
        }

        [HttpGet("{customerId}/addresses")]
        public ActionResult<IEnumerable<Address>> GetAddresses(string customerId, string name)
        {
            var addresses = this.addressService.GetByCustomerId(customerId);
            return Ok(addresses);
        }
    }
}
