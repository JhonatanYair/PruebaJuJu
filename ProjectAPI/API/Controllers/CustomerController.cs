using API.Common;
using API.Service;
using Business;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using CustomerEntity = DataAccess.Data.Customer;

namespace API.Controllers.Customer
{
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService) 
        {
            _customerService = customerService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _customerService.GetAllCustomersAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customerFind = await _customerService.GetByIdCustomerAsync(id);

            if (customerFind.CodeHttp == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound(customerFind);
            }
            else if(customerFind.CodeHttp == System.Net.HttpStatusCode.OK)
            {
                return Ok(customerFind);
            }
            else
            {
                return BadRequest(customerFind);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerEntity entity)
        {
            var customerCreated = await _customerService.CreateCustomerAsync(entity);
            if (customerCreated.CodeHttp == System.Net.HttpStatusCode.Created)
            {
                return Ok(customerCreated);
            }
            else
            {
                return BadRequest(customerCreated);
            }          
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromBody]CustomerEntity entity)
        {
            var customerUpdated = await _customerService.UpdateCustomerAsync(id,entity);
            if (customerUpdated.CodeHttp == System.Net.HttpStatusCode.OK)
            {
                return Ok(customerUpdated);
            }
            else
            {
                return BadRequest(customerUpdated);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customerDelete = await _customerService.DeleteCustomerAsync(id);

            if (customerDelete.CodeHttp == System.Net.HttpStatusCode.OK)
            {
                return Ok(customerDelete);
            }
            else
            {
               return BadRequest(customerDelete);
            }
        }
    }
}
