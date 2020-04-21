using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyShop.Domain.Models;
using MyShop.Infrastructure.Repositories;

namespace MyShop.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IRepository<Customer> repository;

        public CustomerController(ILogger<CustomerController> logger,
            IRepository<Customer> repository)
        {
            _logger = logger;
            this.repository = repository;
        }

        public IActionResult Index(Guid? id)
        {
            if (id == null)
            {
                var customers = repository.All();

                return View(customers);
            }
            else
            {
                var customer = repository.Get(id.Value);

                return View(new[] { customer });
            }
        }
    }
}
