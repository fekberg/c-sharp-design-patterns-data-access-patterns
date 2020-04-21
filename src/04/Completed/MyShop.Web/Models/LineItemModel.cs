using System;

namespace MyShop.Web.Models
{
    public class LineItemModel
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
