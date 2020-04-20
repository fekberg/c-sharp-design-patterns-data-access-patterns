using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyShop.Business;
using MyShop.Business.Models;
using MyShop.Data;
using MyShop.Web.Models;

namespace MyShop.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IRepository<Order> orderRepository;
        private readonly IRepository<Product> productRepository;
        private readonly IRepository<Customer> customerRepository;
        private readonly UnitOfWork unitOfWork;

        public OrderController(ILogger<OrderController> logger,
             IRepository<Order> orderRepository,
             IRepository<Product> productRepository,
             IRepository<Customer> customerRepository,
             UnitOfWork unitOfWork)
        {
            _logger = logger;
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
            this.customerRepository = customerRepository;
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var orders = orderRepository.Find(order => order.OrderDate > DateTime.UtcNow.AddDays(-1));

            return View(orders);
        }

        public IActionResult Create()
        {
            var products = productRepository.All();

            return View(products);
        }

        [HttpPost]
        public IActionResult Create(CreateOrderModel model)
        {
            if (!model.LineItems.Any()) return BadRequest("Please submit line items");

            if (string.IsNullOrWhiteSpace(model.Customer.Name)) return BadRequest("Customer needs a name");

            //var order = new Order
            //{
            //    LineItems = model.LineItems
            //        .Select(line => new LineItem { ProductId = line.ProductId, Quantity = line.Quantity })
            //        .ToList(),

            //    Customer = new Customer
            //    {
            //        Name = model.Customer.Name,
            //        ShippingAddress = model.Customer.ShippingAddress,
            //        PostalCode = model.Customer.PostalCode,
            //        Country = model.Customer.Country
            //    }
            //};

            //orderRepository.Add(order);

            //orderRepository.SaveChanges();

            var customer = //customerRepository
                unitOfWork.CustomerRepository
                .Find(c => c.Name == model.Customer.Name)
                .FirstOrDefault();

            if(customer != null)
            {
                customer.ShippingAddress = model.Customer.ShippingAddress;
                customer.PostalCode = model.Customer.PostalCode;
                customer.Country = model.Customer.Country;

                unitOfWork.CustomerRepository.Update(customer);
            }
            else
            {
                customer = new Customer
                {
                    Name = model.Customer.Name,
                    ShippingAddress = model.Customer.ShippingAddress,
                    PostalCode = model.Customer.PostalCode,
                    Country = model.Customer.Country
                };
            }

            var order = new Order
            {
                LineItems = model.LineItems
                    .Select(line => new LineItem { ProductId = line.ProductId, Quantity = line.Quantity })
                    .ToList(),

                Customer = customer
            };

            //orderRepository.Add(order);

            //orderRepository.SaveChanges();

            unitOfWork.OrderRepository.Add(order);

            unitOfWork.SaveChanges();

            return Ok("Order Created");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
