using MyShop.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyShop.Business.Models
{
    public class Order
    {
        public Guid OrderId { get; private set; }

        public IEnumerable<LineItem> LineItems { get; set; }

        public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }

        // SQLite doesn't support DateTimeOffset :(
        public DateTime OrderDate { get; set; }

        public decimal OrderTotal => LineItems.Sum(item => item.Product.Price * item.Quantity);

        public Order()
        {
            OrderId = Guid.NewGuid();

            OrderDate = DateTime.UtcNow;
        }
    }
}
