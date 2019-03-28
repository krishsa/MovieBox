using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieBox.Models;
using MovieBox.ViewModels;
using MovieBox.Data;
using Microsoft.EntityFrameworkCore;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieBox.Controllers
{
    public class CustomersController : Controller
    {

        private ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext _appContext)
        {
            _context = _appContext;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);

        }

        public IActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        public IActionResult New()
        {
            var memberShipType = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = memberShipType
            };
            return View("CustomerForm",viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Customer customer)
        {
            if(!(ModelState.IsValid))
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        public IActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }


        //IEnumerable - using this will give colletion features in View like Index(), Append()..
        //private IEnumerable<Customer> GetCustomers()
        //{
        //    return new List<Customer>
        //    {
        //        new Customer { Id =1, Name = "John Smith"},
        //        new Customer { Id = 2, Name = "Mary Williams"}
        //    };
        //}
    }
}
