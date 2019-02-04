using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySuper.Api.DatabaseContext;
using MySuper.Api.Entities;
using MySuper.Api.Repositories;

namespace MySuper.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CutomerRepository _cutomerRepository;

        public CustomersController(CutomerRepository cutomerRepository)
        {
            _cutomerRepository = cutomerRepository;
        }

        // GET: api/Customers
        [HttpGet]
        public ActionResult GetCustomers()
        {
            try
            {
                var customers = _cutomerRepository.GetCustomers();
                return Ok(customers);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public ActionResult GetCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer =_cutomerRepository.GetCustomerByID(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public ActionResult PutCustomer([FromRoute] int id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.Id)
            {
                return BadRequest();
            }

            _cutomerRepository.UpdateCustomer(customer);
            _cutomerRepository.Save();

            return NoContent();
        }

        // POST: api/Customers
        [HttpPost]
        public ActionResult PostCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _cutomerRepository.InsertCustomer(customer);
            _cutomerRepository.Save();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public ActionResult DeleteCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = _cutomerRepository.GetCustomerByID(id);
            if (customer == null)
            {
                return NotFound();
            }

            _cutomerRepository.DeleteCustomer(customer);
            _cutomerRepository.Save();

            return Ok(customer);
        }

       
    }
}