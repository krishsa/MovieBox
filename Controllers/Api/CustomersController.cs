using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieBox.Data;
using MovieBox.Models;
using MovieBox.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieBox.Controllers.Api
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CustomersController(ApplicationDbContext _appContext, IMapper mapper)
        {
            _context = _appContext;
            _mapper = mapper;
        }

        // GET: api/customers
        [HttpGet]
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _context.Customers.ToList().Select(_mapper.Map<Customer, CustomerDto>);

        }

        // GET api/customers/1
        [HttpGet("{id}")]
        public CustomerDto GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                throw new ArgumentNullException();

            return _mapper.Map<Customer, CustomerDto>(customer); 
        }

        // POST api/customers
        [HttpPost]
        public IActionResult CreateCustomer([FromBody]CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = _mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            var requestUrl = new Uri(HttpContext.Request.Path); 

            return Created(requestUrl + "/" + customerDto.Id, customerDto);
        }

        // PUT api/customers/1
        [HttpPut("{id}")]
        public void UpdateCustomer(int id, [FromBody]CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new Exception();

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new Exception();

            _mapper.Map<CustomerDto, Customer>(customerDto, customerInDb);
            //customerInDb.Name = customer.Name;
            //customerInDb.BirthDate = customer.BirthDate;
            //customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            //customerInDb.MembershipTypeId = customer.MembershipTypeId;

            _context.SaveChanges();

        }

        // DELETE api/customers/1
        [HttpDelete("{id}")]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id ==id);

            if (customerInDb == null)
                throw new Exception();

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges(); 
        }
    }
}
