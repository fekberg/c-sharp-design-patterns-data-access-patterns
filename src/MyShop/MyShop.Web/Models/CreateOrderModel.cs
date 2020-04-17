using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Web.Models
{
    public class CreateOrderModel
    {
        public IEnumerable<LineItemModel> LineItems { get; set; }

        public CustomerModel Customer { get; set; }
    }

    public class CustomerModel
    {
        public string Name { get; set; }
        public string ShippingAddress { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }

    public class LineItemModel
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
