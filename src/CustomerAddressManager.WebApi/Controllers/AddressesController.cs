using CustomerAddressManager.BusinessDomain.Services;
using CustomerAddressManager.Dal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CustomerAddressManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService addressService;

        public AddressesController(IAddressService addressService)
        {
            this.addressService = addressService;
        }

        [HttpPost]
        public ActionResult Create([FromBody] Address value)
        {
            this.addressService.CreateAddress(value);
            return CreatedAtAction(nameof(AddressesController.GetSingle), new { customerId = value.CustomerId, addressType = value.AddressType }, value);
        }

        [HttpPut]
        public ActionResult Update([FromBody] Address value)
        {
            this.addressService.Update(value);
            return CreatedAtAction(nameof(AddressesController.GetSingle), new { customerId = value.CustomerId, addressType = value.AddressType }, value);
        }

        [HttpDelete("{customerId}/addressTypes/{addressType}")]
        public ActionResult Delete(string customerId, string addressType)
        {
            this.addressService.Delete(customerId, addressType);
            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Address>> Get()
        {
            var addresses = this.addressService.GetAll();
            return Ok(addresses);
        }

        [HttpGet("{customerId}/addressTypes/{addressType}")]
        public ActionResult<Customer> GetSingle(string customerId, string addressType)
        {
            var addresses = this.addressService.GetSingle(customerId, addressType);
            return Ok(addresses);
        }
    }
}
