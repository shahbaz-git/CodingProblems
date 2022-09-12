using CustomerApi.Entities;
using CustomerApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Get All Customers.
        /// </summary>
        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(IEnumerable<Customer>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _customerService.GetCustomers();
            return Ok(response);
        }

        /// <summary>
        /// Create customer.
        /// </summary>
        [HttpPost("[action]", Name = nameof(PostAsync))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostAsync(Customer[] customerToAdd)
        {
            var result = await _customerService.AddCustomers(customerToAdd);

            return result == 1 ? Ok("Customer(s) created successfully!")
                : BadRequest("Failed to insert one or more customers due to duplicate payload!");
        }
    }
}
