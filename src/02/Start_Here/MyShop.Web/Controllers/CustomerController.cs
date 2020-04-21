using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyShop.Infrastructure;

namespace MyShop.Web.Controllers
{
    public class CustomerController : Controller
    {
        private ShoppingContext context;

        public CustomerController()
        {
            context = new ShoppingContext();
        }

        public IActionResult Index(Guid? id)
        {
            if (id == null)
            {
                var customers = context.Products.ToList();

                return View(customers);
            }
            else
            {
                var customer = context.Products.Find(id.Value);

                return View(new[] { customer });
            }
        }
    }
}
